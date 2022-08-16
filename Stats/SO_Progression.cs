using UnityEngine;
using System.Collections.Generic;
using System;

namespace RPG.Stats
{
    [CreateAssetMenu(fileName = "Progression", menuName = "Stats/SO_Progession", order = 0)]
    public class SO_Progression : ScriptableObject
    {
        [SerializeField] ProgressionCharacterClass[] progressionCharacterClasses = null;


        Dictionary<CharacterClass, Dictionary<Stat, float[] >> lookupTable = null;


        public float GetStats(Stat stat, CharacterClass characterClass, int level)
        {
            BuildLookup();

            float[] levels = lookupTable[characterClass][stat];

            if(levels.Length < level) return 0;

            return levels[level-1];;
        }

        public int GetNumberOfLevels(Stat stat, CharacterClass characterClass)
        {
            BuildLookup();

            float[] levels = lookupTable[characterClass][stat];
            return levels.Length;
        }

        private void BuildLookup()
        {
            if(lookupTable != null) return;

            lookupTable = new Dictionary<CharacterClass/*enum*/, Dictionary<Stat/*enum*/, float[]/*level*/>>();

            foreach(ProgressionCharacterClass progressionCharacterClass in progressionCharacterClasses)
            {
                var statLooupTable = new Dictionary<Stat, float[]>();
                foreach (ProgressionStat progressionStat in progressionCharacterClass.stats)
                {
                    statLooupTable[progressionStat.stat] = progressionStat.levels;   
                    /* here the key where we want to keep the levels values, because of [], and the values we put in */
                }

                lookupTable[progressionCharacterClass.characterClass] = statLooupTable;
            }
        }

        [System.Serializable]
        class ProgressionCharacterClass
        {
            public CharacterClass characterClass; //enum
            public ProgressionStat[] stats;
        }

        
        [System.Serializable]
        class ProgressionStat
        {
            public Stat stat; //enum
            public float[] levels;
        }
    }
}
