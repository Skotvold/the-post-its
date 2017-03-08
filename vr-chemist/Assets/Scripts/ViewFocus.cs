using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewFocus : MonoBehaviour
{
    private SteamVR_TrackedObject trackedObj;
    public GameObject ViewTriggerPrefab;
    private GameObject viewObject;
    private Transform laserTransform;
    private Vector3 hitPoint;
    public LayerMask teleportMask;
    public Vector3 teleportReticleOffset;


    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }

    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    private void ShowViewTrigger(RaycastHit hit)
    {

        viewObject.SetActive(true);
        laserTransform.position = Vector3.Lerp(trackedObj.transform.position, hitPoint, .5f);
        laserTransform.LookAt(hitPoint);
       
    }



    void Start()
    {
        // for the laser
        viewObject = Instantiate(ViewTriggerPrefab);
        laserTransform = viewObject.transform;

    }

    // Update is called once per frame
    void Update()
    {
        
        if (!Controller.GetPress(SteamVR_Controller.ButtonMask.Touchpad))
        {
            viewObject.SetActive(false);
            
        }

        RaycastHit hit;
        Physics.Raycast(trackedObj.transform.position, transform.forward, out hit, 100, teleportMask);
        hitPoint = hit.point;
        ShowViewTrigger(hit);

        laserTransform.position = hitPoint + teleportReticleOffset;

    }
}
