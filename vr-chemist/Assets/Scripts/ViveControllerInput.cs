using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViveControllerInput : MonoBehaviour
{
    private SteamVR_TrackedObject mTrackedObject;
    private SteamVR_Controller.Device mController
    {
       get
        {
            return SteamVR_Controller.Input((int)mTrackedObject.index);
        }
    }

    // Wake up the controllers
    void Awake()
    {
        mTrackedObject = GetComponent<SteamVR_TrackedObject>();
    }

    // Update is called once per frame
    void Update () {
        // 1
        if (mController.GetAxis() != Vector2.zero)
        {
            Debug.Log(gameObject.name + mController.GetAxis());
        }

        // 2
        if (mController.GetHairTriggerDown())
        {
            Debug.Log(gameObject.name + " Trigger Press");
        }

        // 3
        if (mController.GetHairTriggerUp())
        {
            Debug.Log(gameObject.name + " Trigger Release");
        }

        // 4
        if (mController.GetPressDown(SteamVR_Controller.ButtonMask.Grip))
        {
            Debug.Log(gameObject.name + " Grip Press");
        }

        // 5
        if (mController.GetPressUp(SteamVR_Controller.ButtonMask.Grip))
        {
            Debug.Log(gameObject.name + " Grip Release");
        }
    }
}
