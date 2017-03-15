using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JointConnection : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "StaticVR")
        {
            AddFixedJoint(collision.gameObject);
        }
    }

    private void AddFixedJoint(GameObject gameObject)
    {
        FixedJoint fx = this.gameObject.AddComponent<FixedJoint>();
        fx.breakForce = 2000;
        fx.breakTorque = 2000;
        fx.connectedBody = gameObject.GetComponent<Rigidbody>();
        fx.enableCollision = true;
    }

    public void StopVelocity ()
    {
        StopMovement();
    }

    private void OnCollisionExit(Collision collision)
    {
        StopMovement();
    }

    private void StopMovement()
    {
        this.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }


}
