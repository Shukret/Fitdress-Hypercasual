using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Game;
using Player;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class PlayerMovement : MonoBehaviour
{
    public event Action onStart;
    public event Action onComplete;

    public Transform moveTarget;
    public Transform startTarget;
    public Transform head;

    public float smoothTime = 1f;
    public float speed = 1f;
    public bool lookAt;
    public float rotationSpeed = 1f;
    
    private float _move;
    private Vector3 _velocity;
    
    private int distMoveSpeed = 0;
    private void Update()
    {
        if (moveTarget)
        {
            if (moveTarget.position.z - head.position.z < -1f)
            {
                moveTarget = startTarget;
            }
            
            MoveForward();
            
            var pos = head.position;
            pos.y = 0;
        
            var targetPos = moveTarget.position;
            targetPos.y = 0;

            if (Vector3.Distance(pos, targetPos) < 0.5f)
            {
                if (distMoveSpeed != 0)
                {
                    onComplete?.Invoke();
                }
                distMoveSpeed = 0;
            }
            else
            {
                if (distMoveSpeed != 1)
                {
                    onStart?.Invoke();
                }
                distMoveSpeed = 1;
            }
            
            if (lookAt)
                LookAtTarget(moveTarget);
            
            MoveToTarget(moveTarget);
        }
    }

    private void MoveForward()
    {
        transform.position += Vector3.forward * speed * _move * distMoveSpeed * Time.deltaTime;
    }
    
    private void LookAtTarget(Transform target)
    {
        Quaternion lookOnLook =
            Quaternion.LookRotation(target.position - head.position);
        head.rotation = Quaternion.Slerp(head.rotation, lookOnLook, Time.deltaTime * rotationSpeed * _move);
    }
    
    private void MoveToTarget(Transform target)
    {
        var targetPos = target.position;
        targetPos.z = 0;
        var newPos = Vector3.SmoothDamp(head.localPosition, targetPos, ref _velocity, smoothTime, _move * 1000, Time.deltaTime);
        newPos.z = 0;
        head.localPosition = newPos;
    }
    
    public void StartMove()
    {
        _move = 1;
    }

    public void StopMove()
    {
        _move = 0;
    }
}