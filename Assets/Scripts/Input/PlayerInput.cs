﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public PlayerController controller;
    public Orbit cam;
    
	void Update ()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        cam.Look(mouseX, mouseY);
        float inputH = Input.GetAxis("Horizontal");
        float inputV = Input.GetAxis("Vertical");
        bool isJumping = Input.GetButton("Jump");
        controller.Move(inputH, inputV, isJumping);
    }
}
