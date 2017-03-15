using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Atom : MonoBehaviour
{
    public int numberOfConnections;
    public int hasConnected;
    public List<Atom> next;
    public static int ID = 0;
    private int personalID;
    GameObject reference;

    public Atom()
    {
        numberOfConnections = 1;
        next = null;
        updateID();
        personalID = ID;
    }

    

    public void setMaxConnections(int num)
    {
        numberOfConnections = num;
    }

    public void updateConnections(Atom atom)
    {
        if (next.Count < numberOfConnections)
        {
            next.Add(atom);
        }
    }

    public void setReference(GameObject obj)
    {
        reference = obj;
    }

    public int getID()
    {
        return personalID;
    }

    void OnDestroy()
    {
        decrementID();
        next.Clear();
        next = null;
    }

    private void updateID()
    {
        ID++;
    }

    private void decrementID()
    {
        ID--;
    }
}

public class Molecules : MonoBehaviour {

    public List<Atom> molecules;
   

    public void Awake()
    {
        foreach (GameObject i in GameObject.FindGameObjectsWithTag("StaticVR"))
        {
            Atom temp = null;
            temp = new Atom();
            temp.setReference(i);
            molecules.Add(temp);
        }
    }
    public void print()
    {
        foreach (Atom i in molecules)
        {
            Debug.Log("Atom: " + i.getID());
        }
    }

	
    // Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        print();
	}
}
