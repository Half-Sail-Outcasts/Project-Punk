using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VaultingCheck : MonoBehaviour
{
    public bool canVault;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ground")
        {
            canVault = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "ground")
        {
            canVault = false;
            print("Can vault here");
        }
    }
}
