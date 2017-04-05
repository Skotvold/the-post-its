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
        // Comment out this and "connections" to test without Dictionary
        GameObject go = GameObject.Find("SingletonController");
        Molecules other = (Molecules)go.GetComponent(typeof(Molecules));
        int connections = other.GetBindingConnections(gameObject.GetInstanceID());

        if (collision.gameObject.tag == "StaticVR" && connections < 2)
        {
            AddFixedJoint(collision.gameObject, connections);
            other.SetBindingConnections(++connections, gameObject.GetInstanceID());
        }


    }

    private void AddFixedJoint(GameObject connectedObject, int connections)
    {
        FixedJoint fx = this.gameObject.AddComponent<FixedJoint>();
        fx.breakForce = 2000;
        fx.breakTorque = 2000;
        
        fx.connectedBody = connectedObject.GetComponent<Rigidbody>();
        fx.enableCollision = true;

    }

    void OnJointBreak(float breakForce)
    {
        GameObject go = GameObject.Find("SingletonController");
        Molecules other = (Molecules)go.GetComponent(typeof(Molecules));
        int connections = other.GetBindingConnections(gameObject.GetInstanceID());
        other.SetBindingConnections(--connections, gameObject.GetInstanceID());
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
