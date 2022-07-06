using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Combat;
using RPG.Core;
using RPG.Movement;

public class AIController : MonoBehaviour
{
    
    [SerializeField] float chaseDistance = 5f;
    [SerializeField] float suspicionTime = 3f;
    [SerializeField] Fighter fighter;
    GameObject player;
    Health health;
    Mover mover;


    Vector3 guardPosition;
    float timeSinceLastSawPlayer = Mathf.Infinity;

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
            timeSinceLastSawPlayer = 0f;
            AttackBehaviour();
        }
        else if (timeSinceLastSawPlayer < suspicionTime)
        {
            SuspicionBehaviour();
        }
        else
        {
            GuardBehaviour();
        }

        timeSinceLastSawPlayer += Time.deltaTime;
    }

    private void GuardBehaviour()
    {   
          //call also CancelAction
        mover.StartMoveAction(guardPosition);
    }

    private void SuspicionBehaviour()
    {
        GetComponent<ActionsScheduler>().CancelCurrentAction();
    }

    private void AttackBehaviour()
    {
        fighter.Attack(player);
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
