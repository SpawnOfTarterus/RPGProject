using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RPG.Core;

namespace RPG.Movement
{
    public class Move : MonoBehaviour, IAction
    {
        [SerializeField] float moveSpeed = 1f;

        float speedRatio;
        
        NavMeshAgent myMeshAgent;
        Animator myAnimator;
        ActionScheduler myActionScheduler;

        void Start()
        {
            myMeshAgent = GetComponent<NavMeshAgent>();
            myAnimator = GetComponent<Animator>();
            myActionScheduler = GetComponent<ActionScheduler>();
            myMeshAgent.speed = moveSpeed;
        }

        void Update()
        {
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
            speedRatio = myMeshAgent.velocity.magnitude / myMeshAgent.speed;
            myAnimator.SetFloat("Blend", speedRatio);
        }
    }
}
