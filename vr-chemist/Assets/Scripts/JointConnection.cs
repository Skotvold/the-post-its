using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JointConnection : MonoBehaviour {

    private const int MaxConnections = 1;

    private void FixedUpdate()
    {
        //Fixes position of binding endings to binding parent
        if (name == "Binding_EndR")
        {
            transform.localPosition = new Vector3(0.5f, 0, 0);
        }
        else
        {
            transform.localPosition = new Vector3(-0.5f, 0, 0);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject go = GameObject.Find("SingletonController");
        Molecules other = (Molecules)go.GetComponent(typeof(Molecules));
        int connections = other.GetBindingConnections(gameObject.GetInstanceID());

        if (collision.gameObject.tag == "Atom" && connections < MaxConnections)
        {
            AddFixedJoint(collision.gameObject, connections);
            other.SetBindingConnections(++connections, gameObject.GetInstanceID());
        }
    }

    private void AddFixedJoint(GameObject connectedObject, int connections)
    {
        FixedJoint fx = gameObject.AddComponent<FixedJoint>();
        fx.breakForce = 2000;
        fx.breakTorque = 2000;
        connectedObject.transform.position = transform.position;
        fx.autoConfigureConnectedAnchor = true;
        fx.enableCollision = false;
        fx.connectedBody = connectedObject.GetComponent<Rigidbody>();
    }

    void OnJointBreak(float breakForce)
    {
        GameObject go = GameObject.Find("SingletonController");
        Molecules other = (Molecules)go.GetComponent(typeof(Molecules));
        int connections = other.GetBindingConnections(gameObject.GetInstanceID());
        other.SetBindingConnections(--connections, gameObject.GetInstanceID());
        gameObject.GetComponent<FixedJoint>().enableCollision = true;
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

////////////////////////////OLD
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