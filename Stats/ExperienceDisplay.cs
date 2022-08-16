using UnityEngine;
using TMPro;
    
namespace RPG.Stats
{
    public class ExperienceDisplay : MonoBehaviour
    { 
        Experience experience;

        private void Awake()
        {
            experience = GameObject.FindWithTag("Player").GetComponent<Experience>();
        }

        private void Update()
        {
            GetComponent<TextMeshProUGUI>().SetText("{0}", experience.GetExperience());
        }
    }
}
        