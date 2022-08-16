using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Stats
{
    public class BaseStats : MonoBehaviour
    {

        [Range(1, 99)]
        int StartingLevel = 1;
        int currentLevel = 0;
        
        [SerializeField] CharacterClass characterClass;
        [SerializeField] SO_Progression progression = null;

        [SerializeField] GameObject levelUpParticleEffect = null;


        public event Action onLevelUp;

        

        public void Start()
        {
            currentLevel = CalculateLevel();
            Experience experience = GetComponent<Experience>();
            if(experience != null)
            {
                experience.onExperienceGained += UpdateLevel;
            }
        }

        public void UpdateLevel()
        {
            int newLevel = CalculateLevel();
            if(newLevel > currentLevel)
            {
                currentLevel = newLevel;
                LevelUpEffect();
                onLevelUp();
            }
        }

        private void LevelUpEffect()
        {
            Instantiate(levelUpParticleEffect, transform); 
        }

        public float GetStats(Stat stat)
        {
            return progression.GetStats(stat, characterClass, GetLevel()) + GetAdditiveModifier(stat);
        }

        
        public int GetLevel()
        {
            if(currentLevel<1) 
            {
                currentLevel = CalculateLevel();
            }
            return currentLevel;
        }

        private float GetAdditiveModifier(Stat stat)
        {
            float total = 0;
            foreach (IModifierProvider provider in GetComponents<IModifierProvider>())
            {
                foreach (float modifiers in provider.GetAdditiveModifier(stat))
                {
                    total += modifiers;
                }
            }
            return total;
        }

        private int CalculateLevel()
        {
            Experience experience = GetComponent<Experience>();
            if(experience == null) return StartingLevel;

            float currentXP = experience.GetExperience(); 
            int penultimateLevel = progression.GetNumberOfLevels(Stat.ExperienceToLevelUp, characterClass);
            for(int level = 1; level <= penultimateLevel; level++)
            {
                float xPToLevelUp = progression.GetStats(Stat.ExperienceToLevelUp, characterClass, level);
                if(xPToLevelUp > currentXP)
                {
                    return level;
                }
            }
            return penultimateLevel + 1;
        }
    }
}