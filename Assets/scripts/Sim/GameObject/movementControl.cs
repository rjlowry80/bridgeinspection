﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movementControl : MonoBehaviour {

    public float modifySpeed = 1.0f;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

        this.handleKeyboardControl();

    }

    private void handleKeyboardControl()
    {
        if(this.gameObject.name == "UBD")
        {
            ubdMovement();
        }
        else if(this.gameObject.name == "myRobot2")
        {
            robotMovement();
        }
        else
        {
            flyingMovement();
        }
    }

    private void robotMovement()
    {
        //float modifySpeed = 0.15f;

        //Angular movement
        //get axis to rotate around
        Vector3 leftaxis = transform.TransformDirection(Vector3.up);
        //rotate
        this.transform.RotateAround(transform.position, leftaxis, Input.GetAxis("Horizontal"));

        //Linear movement
        //Vector3.left is used because UBD transform is off 
        this.transform.Translate(Input.GetAxis("Vertical") * Vector3.forward * modifySpeed * Time.deltaTime);

        //constructing ROS teleop message
        //float _dx = Input.GetAxis("Horizontal");
        //float _dy = Input.GetAxis("Vertical");
        //float linear = _dy * 0.6f;
        //float angular = -_dx * 1.6f;
        //ROSManager.getInstance().RemoteControl(new Vector3(linear, 0.0f, 0.0f), new Vector3(0.0f, 0.0f, angular));

    }


    private void ubdMovement()
    {
        //float modifySpeed = 0.15f;
        
        //Angular movement
        //get axis to rotate around
        Vector3 leftaxis = transform.TransformDirection(Vector3.up);
        //rotate
        this.transform.RotateAround(transform.position, leftaxis, Input.GetAxis("Horizontal"));

        //Linear movement
        //Vector3.left is used because UBD transform is off 
        this.transform.Translate(Input.GetAxis("Vertical") * Vector3.left * modifySpeed * Time.deltaTime);
        
        //constructing ROS teleop message
        float _dx = Input.GetAxis("Horizontal");
        float _dy = Input.GetAxis("Vertical");
        float linear = _dy * 0.6f;
        float angular = -_dx * 1.6f;
        ROSManager.getInstance().RemoteControl(new Vector3(linear, 0.0f, 0.0f), new Vector3(0.0f, 0.0f, angular));

    }

    private void flyingMovement()
    {
        int increaseSpeed = 1;

        //increase the speed of the free cam
        if (this.gameObject.name == "FreeCamObject")
        {
            increaseSpeed = 4;
        }
        
        //Angular movement
        //get axis to rotate around
        Vector3 leftaxis = transform.TransformDirection(Vector3.up);
        //rotate
        this.transform.RotateAround(transform.position, leftaxis, Input.GetAxis("Horizontal"));

        //Linear movement forward and back
        this.transform.Translate(Input.GetAxis("Vertical") * Vector3.forward * increaseSpeed * Time.deltaTime);

        //Linear movement up and down
        this.transform.Translate(Input.GetAxis("Jump") * Vector3.up * Time.deltaTime * increaseSpeed );
    }
}
