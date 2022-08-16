using UnityEngine;
using TMPro;
    
namespace RPG.Stats
{
    public class LevelDisplay : MonoBehaviour
    { 
        BaseStats baseStats;

        private void Awake()
        {
            baseStats = GameObject.FindWithTag("Player").GetComponent<BaseStats>();
        }

        private void Update()
        {
            GetComponent<TextMeshProUGUI>().SetText("{0}", baseStats.GetLevel());
        }
    }
}
        