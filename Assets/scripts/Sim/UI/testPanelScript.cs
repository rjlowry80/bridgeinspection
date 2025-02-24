﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class testPanelScript : MonoBehaviour {

    public Text ROSLinearVelocityOut;
    public Text dronePanelVelocityOut;
    public Text dronePanelAngVelocity;
    public Text droneAltitude;
    public GameObject Drone;
    public GameObject UBD;


    private Transform droneTransform;
    private Rigidbody droneRigidbody;
    private Vector3 drn_velocityVector;
    private Vector3 drn_angularVelocityVector;
    private ROSManager rosManager;

    // Use this for initialization
    void Start () {
        droneRigidbody = Drone.GetComponent<Rigidbody>();
        droneTransform = Drone.GetComponent<Transform>();

        drn_angularVelocityVector = droneTransform.eulerAngles;
        drn_velocityVector = droneTransform.position;

        rosManager = ROSManager.getInstance();
        
        

		
	}
	
	// Update is called once per frame
	void Update () {

        getDroneVelocity();
        getDroneAngularVelocity();

        getROSLinearVelocity();
		
	}

    private void getDroneVelocity()
    {
        float movementPerFram = Vector3.Distance(drn_velocityVector, droneTransform.position);
        float velocityTemp = (movementPerFram / Time.deltaTime);
        drn_velocityVector = droneTransform.position;

        dronePanelVelocityOut.text = velocityTemp.ToString();
    }

    private void getDroneAngularVelocity()
    {
        Vector3 angMovementPerFrame = drn_angularVelocityVector - droneTransform.eulerAngles;
        float angVelocityTemp = (angMovementPerFrame.y / Time.deltaTime);
        drn_angularVelocityVector = droneTransform.eulerAngles;

        dronePanelAngVelocity.text = angVelocityTemp.ToString();

       
    }
    
    private void getROSLinearVelocity()
    {
        if (ROSLinearVelocityOut != null)
        {
            ROSLinearVelocityOut.text = ROSManager.getInstance().getLinear().ToString();
        }
        
    }
    
}
