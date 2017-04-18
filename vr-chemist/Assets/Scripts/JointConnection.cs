using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// We are adding a fixced join everytime right?
// so in theory each fixed joint should know what object it is attached to?
// to figure out which joint breaks: https://forum.unity3d.com/threads/solved-onjointbreak.40171/

public class JointConnection : MonoBehaviour {

    private const int MaxConnections = 2;           // Changed this to 2, not sure if necessary, with the bools?
    private bool hasConnectionRight = false;
    private bool hasConnectionLeft  = false;

    // These lines was used for the check of controlling which atom that got removed,
    // Not sure if needed, if somethign crash, take away this check. And see if it works then
    // This code is created in my head, so when testing remember that. For finding out if things work correctly, this may need to be removed.

    private FixedJoint fixedJoint_right = null;
    private FixedJoint fixedJoint_left = null;


    private void OnCollisionEnter(Collision collision)
    {



        GameObject go = GameObject.Find("SingletonController");
        Molecules other = (Molecules)go.GetComponent(typeof(Molecules));
        int connections = other.GetBindingConnections(gameObject.GetInstanceID());


        //------------------------------------------------------------------------------------------------------
                // SUPER SAFETY CHECK DELUXE, IGNORE FOR NOW
        if (fixedJoint_right == null)
            hasConnectionRight = false;

        if (fixedJoint_left == null)
            hasConnectionLeft = false;
        //----------------------------------------------------------------------------------------- 





        if (collision.gameObject.tag == "Atom" && connections < MaxConnections)
        {
            // Calculate right/left intersection of the collision
            var relativePosition = transform.InverseTransformPoint(collision.contacts[0].point);

            // When checking this, check the model that is being used, it could be another axis that is the
            // Models "side". need to check, my test program used the Z-direction. Seems the interBinding uses X-AXIS it seems. 
            // Also check the relative position numbers, could also be different, they will be printed in the debug. 

            // Collision is on the "right of the object"
            if (relativePosition.x >= 0.35 && relativePosition.x <= 0.5)
            {
                // REMEMBER: left and right depends on which side of the object you stand.
                // it would be smarter to say: negative side, and positive side. The important part 
                // is that they are at different side, and we can always find this side.  

                Debug.Log("The object is to the right"); // handy for testing
                Debug.Log(relativePosition);             // handy for testing

                // If the right  connection is available.
                if (hasConnectionRight == false)
                {

                    fixedJoint_right = AddFixedJoint(collision.gameObject, connections);
                    // need the id, when we remove this one. Must happen in the molecule script i believe. 
                    // the bools should probably be there and we use the molecule script for controll, reason:
                    // we need to know which molecule that is getting taken off. I am trying with teh fixedjoint reference as a work around
                    hasConnectionRight = true;

                    other.SetBindingConnections(++connections, gameObject.GetInstanceID());
                }

            }

            // Collision is on the "left of the object"
            else if (relativePosition.x <= -0.3 && relativePosition.x >= -0.5)
            {

                Debug.Log("The object is to the left"); // handy for testing
                Debug.Log(relativePosition);            // handy for testing

                if (hasConnectionLeft == false)
                {
                    fixedJoint_left = AddFixedJoint(collision.gameObject, connections);
                    hasConnectionLeft = true;
                    other.SetBindingConnections(++connections, gameObject.GetInstanceID());
                }
            }


            // else we cancel out immediatly, may be removed?
            else
            {
                return;
            }
        
        }
        
    }

    private FixedJoint AddFixedJoint(GameObject connectedObject, int connections)
    {
        FixedJoint fx = gameObject.AddComponent<FixedJoint>();
        fx.breakForce = 2000;
        fx.breakTorque = 2000;

        //----- her ------
        // is this needed?
        //connectedObject.transform.position = transform.position;


        fx.autoConfigureConnectedAnchor = true;
        fx.enableCollision = false;
        fx.connectedBody = connectedObject.GetComponent<Rigidbody>();

        return fx;
    }

    // IF THE ABOVE REFUSE TO WORK AT ALL: OLD CODE
    //private void AddFixedJoint(GameObject connectedObject, int connections)
    //{
    //    FixedJoint fx = gameObject.AddComponent<FixedJoint>();
    //    fx.breakForce = 2000;
    //    fx.breakTorque = 2000;

    //    //----- her ------
    //    // is this needed?
    //    //connectedObject.transform.position = transform.position;


    //    fx.autoConfigureConnectedAnchor = true;
    //    fx.enableCollision = false;
    //    fx.connectedBody = connectedObject.GetComponent<Rigidbody>();
    //}

    void OnJointBreak(float breakForce)
    {
        GameObject go = GameObject.Find("SingletonController");
        Molecules other = (Molecules)go.GetComponent(typeof(Molecules));
        int connections = other.GetBindingConnections(gameObject.GetInstanceID());
        other.SetBindingConnections(--connections, gameObject.GetInstanceID());
        gameObject.GetComponent<FixedJoint>().enableCollision = true;

        // This is why we need to check which atom or joint that is broken. quick fix is done here atm(maybe)
        // This maybe even need to be in update function or something,
        // the join may not be broken her yet... they will be null when broken
        if (fixedJoint_right == null)
            hasConnectionRight = false;

        if (fixedJoint_left == null)
            hasConnectionLeft = false;

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