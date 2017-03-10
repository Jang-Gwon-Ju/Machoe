using UnityEngine;
using System.Collections;

public class buttonManager : MonoBehaviour {

    public GameObject gm;

    public void buttons()
    {
        gm.GetComponent<Manager>().ab.x += 1;
        gm.GetComponent<Manager>().matchmonster = false;
    }
}
