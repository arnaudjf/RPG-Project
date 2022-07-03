using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Combat;
using RPG.Core;

public class AIController : MonoBehaviour
{
    
    [SerializeField] float chaseDistance = 5f;
    [SerializeField] Fighter fighter;
    GameObject player;
    Health health;


    private void Start()
    {
        fighter = GetComponent<Fighter>();
        player = GameObject.FindGameObjectWithTag("Player");   
        health = GetComponent<Health>();
    }

    private void Update()
    { 
        if(health.IsDead()) return;
        if(InAttackRangeOfPlayer() && fighter.CanAttack(player))
        {
            fighter.Attack(player);
        }
        else
        {
            fighter.Cancel();
        }
 
    }

    private bool InAttackRangeOfPlayer()
    {
        float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
        return distanceToPlayer < chaseDistance;
    }
}
