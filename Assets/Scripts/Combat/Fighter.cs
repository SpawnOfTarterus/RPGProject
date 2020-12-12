using RPG.Movement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] float damage = 1f;
        [SerializeField] float weaponRange = 1f;
        [SerializeField] float timeBetweenAttacks = 1f;

        Health target;
        float timeSinceLastAttack = Mathf.Infinity;

        Move move;
        ActionScheduler actionScheduler;

        public float GetWeaponRange()
        {
            return weaponRange;
        }

        private void Start()
        {
            move = GetComponent<Move>();
            actionScheduler = GetComponent<ActionScheduler>();
        }

        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;
            if (target == null) { return; }
            transform.LookAt(target.transform);
            if (target.IsDead()) { Cancel(); return; }
            if (Vector3.Distance(transform.position, target.transform.position) <= weaponRange)
            {
                move.Cancel();
                AttackEvent();
                return;
            }
            move.MoveToDestination(target.transform.position);
        }

        private void AttackEvent()
        {
            if(timeSinceLastAttack >= timeBetweenAttacks)
            {
                GetComponent<Animator>().ResetTrigger("StopAttack");
                timeSinceLastAttack = 0;
                GetComponent<Animator>().SetTrigger("Attack");
            }
        }

        public void Cancel()
        {
            target = null;
            move.Cancel();
            GetComponent<Animator>().ResetTrigger("Attack");
            GetComponent<Animator>().SetTrigger("StopAttack");
        }

        public void Attack(GameObject target)
        {
            actionScheduler.StartAction(this);
            this.target = target.GetComponent<Health>();
        }

        //Animation Event
        void Hit()
        {
            if(!target) { return; }
            target.TakeDamage(damage);
        }
    }
}
