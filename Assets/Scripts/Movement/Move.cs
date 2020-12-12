using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RPG.Core;

namespace RPG.Movement
{
    public class Move : MonoBehaviour, IAction
    {
        [SerializeField] float maxSpeed = 10f;

        float speedRatio;
        
        NavMeshAgent myMeshAgent;
        Animator myAnimator;
        ActionScheduler myActionScheduler;
        Health health;

        public void SetMoveSpeed(float newSpeed)
        {
            myMeshAgent.speed = newSpeed;
        }

        void Start()
        {
            myMeshAgent = GetComponent<NavMeshAgent>();
            myAnimator = GetComponent<Animator>();
            myActionScheduler = GetComponent<ActionScheduler>();
            health = GetComponent<Health>();
            myMeshAgent.speed = maxSpeed;
        }

        void Update()
        {
            myMeshAgent.enabled = !health.IsDead();
            UpdateAnimation();
        }

        public void BaseMove(Vector3 targetPosition)
        {
            myActionScheduler.StartAction(this);
            MoveToDestination(targetPosition);
        }

        public void MoveToDestination(Vector3 destination)
        {
            myMeshAgent.destination = destination;
            myMeshAgent.isStopped = false;
        }

        public void Cancel()
        {
            myMeshAgent.isStopped = true;
        }

        private void UpdateAnimation()
        {
            speedRatio = myMeshAgent.velocity.magnitude / maxSpeed;
            myAnimator.SetFloat("Blend", speedRatio);
        }
    }
}
