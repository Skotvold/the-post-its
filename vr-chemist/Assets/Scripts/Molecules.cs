using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Binding
{
    public GameObject reference = null;
    public int numberOfConnections = 0;

    public Binding(GameObject obj)
    {
        reference = obj;
    }


    public GameObject getReference()
    {
        return reference;
    }

    public int getConnections()
    {
        Debug.Log(numberOfConnections);
        return numberOfConnections;
    }

    public void setConnections(int connections)
    {
        numberOfConnections = connections;
        Debug.Log(numberOfConnections);
    }

}

    public class Atom
{

    public GameObject reference = null;

    public Atom(GameObject obj)
    {
        reference = obj;
    }


    public GameObject getReference()
    {
        return reference;
    }
}





public class Molecules : MonoBehaviour {
    Dictionary<int, Atom> atomList = new Dictionary<int, Atom>();
    Dictionary<int, Binding> bindingList = new Dictionary<int, Binding>();

    public void Awake()
    {
        foreach (GameObject i in GameObject.FindGameObjectsWithTag("StaticVR"))
        {
            Atom atom = new Atom(i);
            atomList.Add(i.GetInstanceID(), atom);
        }

        foreach (GameObject i in GameObject.FindGameObjectsWithTag("Binding"))
        {
            Binding binding = new Binding(i);
            bindingList.Add(i.GetInstanceID(), binding);
        }
    }
    public void print()
    {

    }

	public void stopMovement()
    {
        foreach (Atom i in atomList.Values)
        {
            i.getReference().GetComponent<Rigidbody>().velocity = Vector3.zero;
            i.getReference().GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        }
        foreach (Binding i in bindingList.Values)
        {
            i.getReference().GetComponent<Rigidbody>().velocity = Vector3.zero;
            i.getReference().GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        }
    }

    public int GetBindingConnections(int ID)
    {
        Binding binding = bindingList[ID];
        return binding.getConnections();
    }

    public void SetBindingConnections(int connections, int ID)
    {
        Binding binding = bindingList[ID];
        binding.setConnections(connections);
    }

 
    // Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
  
	}
}
