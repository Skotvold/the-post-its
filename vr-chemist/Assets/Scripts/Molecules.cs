using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//////////////////////////////////////////////////////////////////////////////////////////
public class Binding
{
    public GameObject reference = null;
    public int numberOfConnections = 0;

    public Binding(GameObject obj)
    {
        reference = obj;
    }

    public GameObject getReference
    {
        get { return reference; }
    }

    public int Connections
    {
        get { return numberOfConnections; }
        set { numberOfConnections = value; }
    }
}

//////////////////////////////////////////////////////////////////////////////////////////
    public class Atom
{
    public enum atomType { Hydrogen, Helium, Oxygen, Carbon };
    private atomType thisAtom;
    public GameObject reference = null;

    public Atom(GameObject obj, string type)
    {
        reference = obj;
        switch (type)
        {
            case "Hydrogen": thisAtom = atomType.Hydrogen; break;
            case "Oxygen": thisAtom = atomType.Oxygen; break;
            case "Carbon": thisAtom = atomType.Carbon; break;
            default: thisAtom = atomType.Hydrogen; break;
        }
    }

    public atomType ThisAtom
    {
        get { return thisAtom; }
    }

    public GameObject Reference
    {
        get { return reference; }
    }
}


////////////////////////////////////////////////////////////////////////////////////////
public class Molecules : MonoBehaviour {
    Dictionary<int, Atom> atomList = new Dictionary<int, Atom>();
    Dictionary<int, Binding> bindingList = new Dictionary<int, Binding>();

    public void Awake()
    {
        foreach (GameObject i in GameObject.FindGameObjectsWithTag("Atom"))
        {
            Atom atom = new Atom(i, i.transform.GetChild(0).tag);
            atomList.Add(i.GetInstanceID(), atom);
        }

        foreach (GameObject i in GameObject.FindGameObjectsWithTag("Binding"))
        {
            Binding binding = new Binding(i);
            bindingList.Add(i.GetInstanceID(), binding);
        }
    }

	public void stopMovement()
    {
        foreach (Atom i in atomList.Values)
        {
            i.Reference.GetComponent<Rigidbody>().velocity = Vector3.zero;
            i.Reference.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        }
        foreach (Binding i in bindingList.Values)
        {
            i.getReference.GetComponent<Rigidbody>().velocity = Vector3.zero;
            i.getReference.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        }
    }

    public int GetBindingConnections(int ID)
    {
        Binding binding = bindingList[ID];
        return binding.Connections;
    }

    public void SetBindingConnections(int connections, int ID)
    {
        Binding binding = bindingList[ID];
        binding.Connections = connections;
    }
}
