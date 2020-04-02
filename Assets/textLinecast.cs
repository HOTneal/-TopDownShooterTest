using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class textLinecast : MonoBehaviour
{
    public Transform point1;

    public RaycastHit hit;
    // Update is called once per frame
    void Update()
    {
        if (Physics.Linecast(transform.position, point1.position, out hit))
            print(hit.point);
    }
}