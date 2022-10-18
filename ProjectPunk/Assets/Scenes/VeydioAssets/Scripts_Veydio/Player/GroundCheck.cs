using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public bool isGround;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ground")
        {
            isGround = true;
            print("Grounded");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "ground")
        {
            isGround = false;
            print("where is the floor?");
        }
    }
}
