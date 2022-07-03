using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Combat;
using RPG.Core;
using RPG.Movement;

public class AIController : MonoBehaviour
{
    
    [SerializeField] float chaseDistance = 5f;
    [SerializeField] Fighter fighter;
    GameObject player;
    Health health;
    Mover mover;


    Vector3 guardPosition;

    private void Start()
    {
        fighter = GetComponent<Fighter>();
        player = GameObject.FindGameObjectWithTag("Player");   
        health = GetComponent<Health>();
        mover = GetComponent<Mover>();
        guardPosition = transform.position;

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
            //call also CancelAction
            mover.StartMoveAction(guardPosition); 
        }
 
    }

    private bool InAttackRangeOfPlayer()
    {
        float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
        return distanceToPlayer < chaseDistance;
    }

    // called by Unity
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, chaseDistance);
    }
}
