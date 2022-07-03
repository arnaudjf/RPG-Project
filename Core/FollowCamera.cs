using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RPG.Core
{

    public class FollowCamera : MonoBehaviour
    {


        [SerializeField] private Transform target;


        // Start is called before the first frame update
        void Start()
        {
            target = GameObject.FindWithTag("Player").GetComponent<Transform>();
        }

        // Update is called once per frame
        void LateUpdate()
        {
            GetComponent<Transform>().position = target.position;
        }
    }


}
