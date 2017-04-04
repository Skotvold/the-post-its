using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Binding : MonoBehaviour
{
    public GameObject reference = null;
    public int id = 0;
    public int numberOfConnections = 0;

    public Binding(GameObject obj, int p_id)
    {
        reference = obj;
        id = p_id;
    }

    //Dictionary attempt
    //public Binding(GameObject obj)
    //{
    //    reference = obj;
    //}


    public GameObject getReference()
    {
        return reference;
    }

    public int getID()
    {
        return id;
    }

    public int getConnections()
    {
        return numberOfConnections;
        Debug.Log(numberOfConnections);
    }

    public void setConnections(int connections)
    {
        numberOfConnections = connections;
        Debug.Log(numberOfConnections);
    }

}

    public class Atom : MonoBehaviour
{

    public GameObject reference = null;
    public int id = 0;

    public Atom(GameObject obj, int p_id)
    {
        reference = obj;
        id = p_id;
    }

    //Dictionary attempt
    //public Atom(GameObject obj)
    //{
    //    reference = obj;
    //}


    public GameObject getReference()
    {
        return reference;
    }
}





public class Molecules : MonoBehaviour {
    List<Atom> atomList = new List<Atom>();
    List<Binding> bindingList = new List<Binding>();
    int objectID = 0;

    //Dictionary attempt
    //Dictionary<int, Atom> atomList = new Dictionary<int, Atom>();
    //Dictionary<int, Binding> bindingList = new Dictionary<int, Binding>();

    public void Awake()
    {

        foreach (GameObject i in GameObject.FindGameObjectsWithTag("StaticVR"))
        {
            Atom atom = null;
            atom = new Atom(i, objectID++);
            atomList.Add(atom);
        }

        foreach (GameObject i in GameObject.FindGameObjectsWithTag("Binding"))
        {
            Binding binding = null;
            binding = new Binding(i, objectID++);
            bindingList.Add(binding);
        }

        ////Dictionary attempt
        //foreach (GameObject i in GameObject.FindGameObjectsWithTag("StaticVR"))
        //{
        //    Atom atom = new Atom(i);
        //    atomList.Add(i.GetInstanceID(), atom);
        //}

        //foreach (GameObject i in GameObject.FindGameObjectsWithTag("Binding"))
        //{
        //    Binding binding = new Binding(i);
        //    bindingList.Add(i.GetInstanceID(), binding);
        //}
    }
    public void print()
    {

    }

	public void stopMovement()
    {


        for(int i = 0; i < atomList.Count; i++)
        {
            atomList[i].getReference().GetComponent<Rigidbody>().velocity = Vector3.zero;
            atomList[i].getReference().GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        }

        for (int i = 0; i < bindingList.Count; i++)
        {
            bindingList[i].getReference().GetComponent<Rigidbody>().velocity = Vector3.zero;
            bindingList[i].getReference().GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        }

        //Dictionary attempt
        //foreach (Atom i in atomList.Values)
        //{
        //    i.getReference().GetComponent<Rigidbody>().velocity = Vector3.zero;
        //    i.getReference().GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        //}
        //foreach (Binding i in bindingList.Values)
        //{
        //    i.getReference().GetComponent<Rigidbody>().velocity = Vector3.zero;
        //    i.getReference().GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        //}
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
