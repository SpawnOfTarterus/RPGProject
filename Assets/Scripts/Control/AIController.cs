using RPG.Movement;
using RPG.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseRange = 5f;

        Vector3 targetPos;

        GameObject player;
        Move move;
        Fighter fighter;

        private void Start()
        {
            move = GetComponent<Move>();
            fighter = GetComponent<Fighter>();
            player = GameObject.FindWithTag("Player");
        }

        private void Update()
        {
            if(GetComponent<Health>().IsDead()) { return; }
            CheckIfPlayerInRange();
        }

        private void CheckIfPlayerInRange()
        {
            if(player == null) { return; }
            if (Vector3.Distance(player.transform.position, transform.position) <= chaseRange)
            {
                fighter.Attack(player);
                targetPos = player.transform.position;
                return;
            }
            move.BaseMove(targetPos);
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, chaseRange);
        }
    }
}
