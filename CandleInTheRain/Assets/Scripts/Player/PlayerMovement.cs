﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bool movementEnabled = true;
    public Rigidbody rb;
    public Transform body;
    public Animator animator;

    private Transform mainCam;
    private Vector3 initialPosition;

    private void Awake() 
    {
        mainCam = Game.inst.cams.mainCam.transform;
        initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(movementEnabled)
            Move();
    }

    private void Move() 
    {
        Vector3 movement = new Vector3(Input.GetAxis(InputStrings.HorizontalMovement), 0f, Input.GetAxis(InputStrings.VerticalMovement));
        movement = mainCam.TransformDirection(movement);
        movement.y = 0f;
        rb.velocity = movement.normalized * Game.inst.config.moveSpeed;

        if (movement != Vector3.zero) 
            body.rotation = Quaternion.Slerp(body.rotation, Quaternion.LookRotation(movement), Game.inst.config.rotationSpeed);

        animator.SetBool("walking", movement != Vector3.zero);
    }

    public void MoveTowardsPosition(Transform target, Transform lookTarget)
    {
        Vector3 targetPosition = target.position;
        targetPosition.y = transform.position.y;
        transform.position = targetPosition;

        Vector3 lookPosition = lookTarget.position;
        lookPosition.y = body.position.y;
        body.LookAt(lookPosition, Vector3.up);
    }

    public void ResetPosition()
    {
        transform.position = initialPosition;
    }
}
