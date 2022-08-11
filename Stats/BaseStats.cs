using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Stats
{
    
    public class BaseStats : MonoBehaviour
    {

        [Range(1, 99)]
        [SerializeField] int StartingLevel = 1;
        [SerializeField] CharacterClass characterClass;
        [SerializeField] SO_Progression progression = null;

        public float GetHealth()
        {
            return progression.GetHealth(characterClass, StartingLevel);
        }

    }

}