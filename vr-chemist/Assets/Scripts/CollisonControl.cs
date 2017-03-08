using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisonControl : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionExit(Collision collision)
    {
        this.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

    
}
