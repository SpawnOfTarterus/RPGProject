using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Control
{
    public class PatrolPath : MonoBehaviour
    {
        [SerializeField] Transform[] waypoints = null;
        [SerializeField] float waypointGizmoSize = 1f;

        private void OnDrawGizmos()
        {
            for(int i = 0; i < waypoints.Length; i++)
            {         
                Gizmos.DrawSphere(GetWaypoint(i), waypointGizmoSize);
                if(waypoints.Length <= 1) { return; }
                int j = GetNextIndex(i);
                Gizmos.DrawLine(GetWaypoint(i), GetWaypoint(j));
            }
        }

        public int GetNumberOfWaypoints()
        {
            return waypoints.Length;
        }

        public Vector3 GetWaypoint(int i)
        {
            return waypoints[i].position;
        }

        public int GetNextIndex(int i)
        {
            if(i + 1 == waypoints.Length)
            {
                return 0;
            }
            return i + 1;
        }


    }
}
