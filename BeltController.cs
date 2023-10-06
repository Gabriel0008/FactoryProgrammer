using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeltController : MonoBehaviour
{
    private PlacedObject_Done placedObject;

    
    void Start()
    {
        placedObject = this.gameObject.GetComponent<PlacedObject_Done>();
    }

    // Update is called once per frame
    void Update()
    {
    }
}
