using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Transform mainCam;

    private void OnEnable()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>();
    }

    private void LateUpdate()
    {
        transform.LookAt(new Vector3(transform.position.x + mainCam.forward.x, transform.position.y , transform.position.z + mainCam.forward.z));
    }
}
