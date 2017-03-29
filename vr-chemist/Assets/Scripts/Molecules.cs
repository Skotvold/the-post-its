using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Atom : MonoBehaviour
{

    public GameObject reference;
    public int id;

    Atom()
    {

    }

}

//{
//    Dictionary<GameObject, Atom>

    //public Atom(int i)
    //{
    //    numberOfConnections = 1;
    //    next = null;
    //    updateID();
    //    personalID = i;
    //}

    

    //public void setMaxConnections(int num)
    //{
    //    numberOfConnections = num;
    //}

    //public void updateConnections(Atom atom)
    //{
    //    if (next.Count < numberOfConnections)
    //    {
    //        next.Add(atom);
    //    }
    //}

   

    //public int getID()
    //{
    //    return personalID;
    //}



    //public void setReference(GameObject obj)
    //{
    //    reference = obj;
        
    //}

    //public GameObject getReference()
    //{
    //    return reference;
    //}




    //void OnDestroy()
    //{
    //    decrementID();
    //    next.Clear();
    //    next = null;
    //}





    //private void updateID()
    //{
    //    ID++;
    //}

    //private void decrementID()
    //{
    //    ID--;
    //}

//}


public class Molecules : MonoBehaviour {
    public Dictionary<int, GameObject> molecules = new Dictionary<int, GameObject>();
    public int id;
    //spublic List<GameObject> molecules;
   

    public void Awake()
    {
        id = 0;
        foreach (GameObject i in GameObject.FindGameObjectsWithTag("StaticVR"))
        {

            molecules[id++] = i;
        }

        foreach (GameObject i in GameObject.FindGameObjectsWithTag("Binding"))
        {
            molecules[id++] = i;
        }
    }
    public void print()
    {
     
    }

	public void stopMovement()
    {

        for (int i = 0; i < molecules.Count; i++)
        {
            molecules[i].GetComponent<Rigidbody>().velocity = Vector3.zero;
            molecules[i].GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        }

        //foreach (GameObject i in GameObject.FindGameObjectsWithTag("StaticVR"))
        //{
        //    i.GetComponent<Rigidbody>().velocity = Vector3.zero;
        //    i.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        //}


        //foreach (GameObject i in GameObject.FindGameObjectsWithTag("Binding"))
        //{
        //    i.GetComponent<Rigidbody>().velocity = Vector3.zero;
        //    i.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        //}
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
