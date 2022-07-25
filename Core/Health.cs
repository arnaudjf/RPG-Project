using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Saving;


namespace RPG.Core
{
    public class Health : MonoBehaviour, ISaveable
    {
        [SerializeField] float healthPoints = 100f;
        [SerializeField] bool isDead = false;


        public bool IsDead()
        {
            return isDead;
        }

     
        public void TakeDomage(float damage)
        {
            healthPoints = Mathf.Max(healthPoints - damage, 0);
            
            if(healthPoints == 0)
            {
                Die();
            }
        }

        private void Die()
        {
            if (isDead) return;
            
            GetComponent<Animator>().SetTrigger("die");
            GetComponent<ActionsScheduler>().CancelCurrentAction();
            isDead = true;
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
