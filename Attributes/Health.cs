using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Saving;
using RPG.Stats;
using RPG.Core;
using System;

namespace RPG.Attributes
{
    public class Health : MonoBehaviour, ISaveable
    {
        float healthPoints = -1f;
        [SerializeField] bool isDead = false;
        [SerializeField] float RegenerationPercentage = 70;

        
        private void Start()
        {
            BaseStats baseStats = GetComponent<BaseStats>();

            if(healthPoints < 0)
            {
                healthPoints = baseStats.GetStats(Stat.Health);
            }
            baseStats.onLevelUp += RegenerateHealth;

        }


        public bool IsDead()
        {
            return isDead;
        }

     
        public void TakeDamage(GameObject instigator, float damage)
        {
            print(gameObject.name + " took damage: " + damage);
            
            healthPoints = Mathf.Max(healthPoints - damage, 0);
            
            if(healthPoints == 0)
            {
                Die();
                AwardExperience(instigator);
            }
        }

        public float GetHealthPoint()
        {
            return healthPoints;
        }

        public float GetMaxHealthPoints()
        {
            return GetComponent<BaseStats>().GetStats(Stat.Health);
        }

        public float GetPercentage()
        {
            return 100 * (healthPoints / GetComponent<BaseStats>().GetStats(Stat.Health));
        }

        private void Die()
        {
            if (isDead) return;
            
            GetComponent<Animator>().SetTrigger("die");
            GetComponent<ActionsScheduler>().CancelCurrentAction();
            isDead = true;
        }
        
        private void AwardExperience(GameObject instigator)
        {
            Experience experience = instigator.GetComponent<Experience>();
            if(experience == null) return;

            experience.GainExperience(GetComponent<BaseStats>().GetStats(Stat.ExperienceReward));
        }

        public void RegenerateHealth()
        {
            float regenHelthPoint = GetComponent<BaseStats>().GetStats(Stat.Health) 
                                    *
                                    (RegenerationPercentage / 100) ;
            healthPoints = Mathf.Max(healthPoints, regenHelthPoint);
            
        }


        /* Saving System */
        public object CaptureState()
        {
            return healthPoints;
        }

        public void RestoreState(object state)
        {
            healthPoints = (float)state;
            if(healthPoints == 0)
            {
                Die();
            }
        }
        /*******************/
    }

}
