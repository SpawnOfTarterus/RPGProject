using RPG.Movement;
using RPG.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;
using System;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseRange = 5f;
        [SerializeField] float suspicionTime = 2f;
        [SerializeField] float chaseSpeed = 1f;
        [SerializeField] float patrolSpeed = 1f;
        [SerializeField] PatrolPath patrolPath = null;
        [SerializeField] float stopAndLookTime = 3f;

        float timeSinceLastSawPlayer = Mathf.Infinity; 
            
        GameObject player;
        Move move;
        Fighter fighter;
        int targetPos = 0;
        float timer = 0;

        private void Start()
        {
            move = GetComponent<Move>();
            fighter = GetComponent<Fighter>();
            player = GameObject.FindWithTag("Player");
        }

        private void Update()
        {
            if(GetComponent<Health>().IsDead()) { return; }
            if (CheckIfPlayerInRange() && !player.GetComponent<Health>().IsDead())
            {
                AttackBehavior();
                timeSinceLastSawPlayer = 0f;
            }
            else if(timeSinceLastSawPlayer <= suspicionTime)
            {
                SuspicionBehavior();
            }
            else
            {
                PatrolBehavior();
            }
        }

        private void PatrolBehavior()
        {
            move.SetMoveSpeed(patrolSpeed);
            Vector3 nextCheckpoint = patrolPath.GetWaypoint(targetPos);
            if(HasReachedNextCheckpoint(nextCheckpoint) && patrolPath.GetNumberOfWaypoints() > 1)
            {
                StopAndLook();
            }
            move.BaseMove(nextCheckpoint);
        }

        private void StopAndLook()
        {
            timer += Time.deltaTime;
            if (timer >= stopAndLookTime)
            {
                UpdateTargetPos(targetPos);
            }
        }

        private void UpdateTargetPos(int currentIndex)
        {
            timer = 0;
            targetPos = patrolPath.GetNextIndex(currentIndex);
        }

        private bool HasReachedNextCheckpoint(Vector3 checkpoint)
        {
            float closeEnough = 0.5f;
            return Vector3.Distance(checkpoint, transform.position) <= closeEnough;
        }

        private void SuspicionBehavior()
        {
            timeSinceLastSawPlayer += Time.deltaTime;
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        private void AttackBehavior()
        {
            move.SetMoveSpeed(chaseSpeed);
            fighter.Attack(player);
        }

        private bool CheckIfPlayerInRange()
        {
            if(player == null) { return false; }
            if (Vector3.Distance(player.transform.position, transform.position) <= chaseRange)
            { 
                return true;
            }
            return false;
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, chaseRange);
        }
    }
}
