using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkPlayerInput : NetworkBehaviour
{
    public PlayerController controller;
    public Orbit cam;

	// Use this for initialization
	void Start ()
    {
	    if(!isLocalPlayer)
        {
            cam.gameObject.SetActive(false);
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if(isLocalPlayer)
        {
            float inputH = Input.GetAxis("Horizontal");
            float inputV = Input.GetAxis("Vertical");
            bool isJumping = Input.GetButton("Jump");
            controller.Move(inputH, inputV, isJumping);
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");
            cam.Look(mouseX, mouseY);

            // Check if Attack input
            // Call controller.Attack()
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                controller.Attack();
            }
            if (Input.GetAxis("Mouse ScrollWheel") > 0f) // forward
            {
                controller.x++;
            }
            else if (Input.GetAxis("Mouse ScrollWheel") < 0f) // backwards
            {
                controller.x--;
            }
        }
	}
}
