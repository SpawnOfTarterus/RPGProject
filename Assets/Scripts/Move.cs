using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Move : MonoBehaviour
{
    Vector3 target;
    NavMeshAgent myMeshAgent;
    Camera mainCamera;
    Animator myAnimator;
    float speedRatio;
    
    void Start()
    {
        myMeshAgent = GetComponent<NavMeshAgent>();
        mainCamera = Camera.main;
        myAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0) || Input.GetMouseButton(0))
        {
            MoveToCursor();
        }
        UpdateAnimation();
    }

    private void MoveToCursor()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
        {
            target = hit.point;
            myMeshAgent.destination = target;
        }
    }

    private void UpdateAnimation()
    {
        speedRatio = myMeshAgent.velocity.magnitude / myMeshAgent.speed;
        myAnimator.SetFloat("Blend", speedRatio);
    }
}
