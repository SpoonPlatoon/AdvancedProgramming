using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : MonoBehaviour
{
    public Camera cam;

    public GameObject weapon;
    public GameObject Gun;
    public GameObject GunFire;
    public GameObject SelectedWeapon;

    public int x = 1;

    public bool isJumping;
    public float speed = 6.0F;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;
    private Vector3 moveDirection = Vector3.zero;
    public CharacterController controller;
    
    public void Move(float inputH, float inputV, bool isJumping)
    {
        if (controller.isGrounded)
        {
            Vector3 euler = cam.transform.eulerAngles;
            transform.rotation = Quaternion.AngleAxis(euler.y, Vector3.up);

            moveDirection = new Vector3(inputH, 0, inputV);
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;
            if (Input.GetButton("Jump"))
                moveDirection.y = jumpSpeed;

        }
    }

    public void Update()
    {
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
        WeaponSwap();
    }

    public void Attack()
    {
        //check if leftClick is pressed
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if(SelectedWeapon == Gun)
            {
                Debug.Log("BANG");
                GunFire.SetActive(true);

            }

            if (SelectedWeapon == weapon)
            {
                SelectedWeapon.SetActive(true);
                Debug.Log("SCHWING");
            }
        }
    }

    public void WeaponSwap()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0f) // forward
        {
            x++;
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f) // backwards
        {
            x--;
        }

        if(x == 1)
        {
            SelectedWeapon = Gun;
            Gun.SetActive(true);
        }

        if (x == 0)
        {
            SelectedWeapon = weapon;
            Gun.SetActive(false);
        }

        if (x > 1)
        {
            x = 0;
        }

        if(x < 0)
        {
            x = 1;
        }
    }
}
