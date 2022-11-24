using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClamberCheck : MonoBehaviour
{
    public bool isClamber;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ground")
        {
            isClamber = true;
            print("Clamber Up");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "ground")
        {
            isClamber = false;
            print("Nothing to grab onto");
        }
    }
}
