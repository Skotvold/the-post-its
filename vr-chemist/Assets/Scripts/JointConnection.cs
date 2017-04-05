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
        fx.autoConfigureConnectedAnchor = true;



        //connectedObject.transform.position = new Vector3(xRight,this.transform.position.y,this.transform.position.z);
        //connectedObject.transform.position = newPos;
        fx.connectedBody = connectedObject.GetComponent<Rigidbody>();
       


        // fx.connectedAnchor = Vector3.zero;

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


//float xRight =  transform.TransformPoint(this.GetComponent<BoxCollider>().center).x + transform.TransformVector(this.GetComponent<BoxCollider>().size).x/2;
//Debug.Log(this.transform.position.x + " ,  " + transform.TransformPoint(this.GetComponent<BoxCollider>().center).x + " , " + transform.TransformVector(this.GetComponent<BoxCollider>().size).x);
//Debug.Log(xRight);

//float xLeft = transform.TransformPoint(this.GetComponent<BoxCollider>().center).x - transform.TransformVector(this.GetComponent<BoxCollider>().size).x / 2;
//Debug.Log(this.transform.position.x + " ,  " + transform.TransformPoint(this.GetComponent<BoxCollider>().center).x + " , " + transform.TransformVector(this.GetComponent<BoxCollider>().size).x/2);
//Debug.Log(xLeft);



//if (xLeft > xRight)
//{
//    float center = (((xLeft - xRight) / 2) + xRight);
//    if (connectedObject.gameObject.transform.position.x > center)
//    {
//        connectedObject.transform.position = new Vector3(xLeft, this.transform.position.y, this.transform.position.z);
//    }
//    else
//    {
//        connectedObject.transform.position = new Vector3(xRight, this.transform.position.y, this.transform.position.z);
//    }
//}
//else
//{
//    float center = (((xRight - xLeft) / 2) + xLeft);
//    if (connectedObject.gameObject.transform.position.x < center)
//    {
//        connectedObject.transform.position = new Vector3(xLeft, this.transform.position.y, this.transform.position.z);
//    }
//    else
//    {
//        connectedObject.transform.position = new Vector3(xRight, this.transform.position.y, this.transform.position.z);
//    }
//}