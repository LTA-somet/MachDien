using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetWiresIndivisual : MonoBehaviour
{
    public GameObject[] wires;
    void Start()
    {
        foreach (GameObject gameObject in wires)
        {
            gameObject.SetActive(false);
        }
    }

    
}
