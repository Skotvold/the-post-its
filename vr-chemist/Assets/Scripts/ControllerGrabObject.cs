using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerGrabObject : MonoBehaviour {

	private SteamVR_TrackedObject mTrackedObj;
    private GameObject collidingObject;
    private GameObject objectInHand;

    private SteamVR_Controller.Device mController
    {
        get { return SteamVR_Controller.Input((int)mTrackedObj.index); }
    }

    void Awake()
    {
        mTrackedObj = GetComponent<SteamVR_TrackedObject>();

    }

    private void SetCollidingObject(Collider col)
    {
       
        if (collidingObject || !col.GetComponent<Rigidbody>())
        {
            return;
        }
     
        collidingObject = col.gameObject;
    }



    public void OnTriggerEnter(Collider other)
    {
        SetCollidingObject(other);
    }

    public void OnTriggerStay(Collider other)
    {
        SetCollidingObject(other);
    }

    public void OnTriggerExit(Collider other)
    {
        if (!collidingObject)
        {
            return;
        }

        collidingObject = null;
    }



    private void GrabObject()
    {
        objectInHand = collidingObject;
        collidingObject = null;
        var joint = AddFixedJoint();
        joint.connectedBody = objectInHand.GetComponent<Rigidbody>();
    }

    private FixedJoint AddFixedJoint()
    {
        FixedJoint fx = gameObject.AddComponent<FixedJoint>();
        fx.breakForce = 20000;
        fx.breakTorque = 20000;
        return fx;
    }

    private void ReleaseObject()
    {
        if (GetComponent<FixedJoint>())
        {
            GetComponent<FixedJoint>().connectedBody = null;
            Destroy(GetComponent<FixedJoint>());
        }

        objectInHand.GetComponent<Rigidbody>().velocity = Vector3.zero;
        objectInHand.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

        GameObject go = GameObject.Find("SingletonController");
        Molecules other = (Molecules)go.GetComponent(typeof(Molecules));
        other.stopMovement();
        objectInHand = null;
    }

    // Update is called once per frame
    void Update()
    {
        // If the trigger is down, grab an object.
        if (mController.GetHairTriggerDown())
        {
           
            if (collidingObject)
            {
                GrabObject();
            }
        }

        // If the trigger is released, release the object.
        if (mController.GetHairTriggerUp())
        {
            if (objectInHand)
            {
                ReleaseObject();
            }
        }
    }
}
