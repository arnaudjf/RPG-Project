using UnityEngine;
using TMPro;
using RPG.Attributes;
    
namespace RPG.Combat
{
    public class EnemyHealthDisplay : MonoBehaviour
    {
        private Fighter fighter;
    
        private void Awake()
        {
            fighter = GameObject.FindWithTag("Player").GetComponent<Fighter>();
        }
    
        private void Update()
        {
            if(fighter.GetTargetHealth() == null)
            {
                GetComponent<TextMeshProUGUI>().SetText("N/A");
                return;
            }
            Health health = fighter.GetTargetHealth();
            GetComponent<TextMeshProUGUI>().SetText("{0:0}/{1:0}", health.GetHealthPoint(), health.GetMaxHealthPoints());
        }
    }
}