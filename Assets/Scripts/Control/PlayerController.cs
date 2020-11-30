using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Combat;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        Move move;
        Fighter fighter;

        private void Start()
        {
            move = GetComponent<Move>();
            fighter = GetComponent<Fighter>();
        }

        private void Update()
        {
            if (InteractWithCombat()) { return; }
            if (InteractWithMovement()) { return; }
        }

        private bool InteractWithMovement()
        {
            if (Physics.Raycast(GetMouseRay(), out RaycastHit hit, Mathf.Infinity))
            {
                SetDestination(hit.point);
                return true;
            }
            return false;
        }

        private void SetDestination(Vector3 destination)
        {
            if (Input.GetMouseButton(0))
            {
                move.BaseMove(destination);
            }
        }

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }

        private bool InteractWithCombat()
        {
            return GetTarget();
        }

        private bool GetTarget()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            foreach (RaycastHit hit in hits)
            {
                if (!hit.transform.GetComponent<CombatTarget>()) { continue; }
                if(hit.transform.GetComponent<Health>().IsDead()) { continue; }
                if(Input.GetMouseButtonUp(0))
                {
                    fighter.Attack(hit.transform.gameObject);
                }
                return true;
            }
            return false;
        }
    }
}
