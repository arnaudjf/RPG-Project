using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    
    [SerializeField] float chaseDistance = 5f;
 

    private void Update()
    {
        
        if(DistanceToPlayer() < chaseDistance)
        {
            print(gameObject.name + "Should chase");
        }
        
    }

    private float DistanceToPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        return Vector3.Distance(player.transform.position, transform.position);
    }
}
