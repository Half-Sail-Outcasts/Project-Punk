using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadomNumberScript : MonoBehaviour
{
    // Start is called before the first frame update

    public int spawn;
    public GameObject object1;
    public GameObject object2;
    public GameObject object3;
    public GameObject object4;
    public GameObject object5;
    public GameObject object6;




    public int rariety;
    public int item;
    public int value;


    void Start()
    {
        // 1: Common(White) 2.Uncommon(Green) 3.Rare(Blue) 4.Legendary(Purple) 5.Exotic(Yellow) 6.Artifact(Red)
        rariety = Random.Range(1, 6);
        item = Random.Range(1, 6);
        value = Random.Range(1, 6);
        Debug.Log("Rare" + rariety);
        Debug.Log("item" + item);
        Debug.Log("value" + value);



        //Adding Game objects to List and creating a new list

        Spawn();


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)){
            Spawn();
        }
        //Spawn();
    }

    public void Spawn()
    {
        switch (rariety)
        {
            case 1:
                Common();
                break;
            case 2:
                UnCommon();
                break;
            case 3:
                Rare();
                break;
            case 4:
                Legendary();
                break;
            case 5:
                Exotic();
                break;
            case 6:
                Artifact();
                break;
            default:
                Debug.Log("No Number was Generated");
                break;
        }

        //    GameObject newObject = Instantiate(object1);
        //    return;
    }

    public void Common()
    {
        switch (item)
        {
            case 1:
                Debug.Log("Common first item Spawned");
                value = 50;
                break;
            case 2:
                Debug.Log("Common Second item Spawned");
                break;
            case 3:
                Debug.Log("Common Third item Spawned");
                break;
            case 4:
                Debug.Log("Common Fourth item Spawned");
                break;
            case 5:
                Debug.Log("Common Fifth item Spawned");
                break;
            case 6:
                Debug.Log("Common Six item Spawned");
                break;
            default:
                Debug.Log("No Number was Generated");
                break;
        }
    }
    public void UnCommon()
    {
        switch (item)
        {
            case 1:
                Debug.Log("UnCommon first item Spawned");
                break;
            case 2:
                Debug.Log("UnCommon Second item Spawned");
                break;
            case 3:
                Debug.Log("UnCommon Third item Spawned");
                break;
            case 4:
                Debug.Log("UnCommon Fourth item Spawned");
                break;
            case 5:
                Debug.Log("UnCommon Fifth item Spawned");
                break;
            case 6:
                Debug.Log("UnCommon Six item Spawned");
                break;
            default:
                Debug.Log("No Number was Generated");
                break;
        }
    }
    public void Rare()
    {
        switch (item)
        {
            case 1:
                Debug.Log("Rare first item Spawned");
                break;
            case 2:
                Debug.Log("Rare Second item Spawned");
                break;
            case 3:
                Debug.Log("Rare Third item Spawned");
                break;
            case 4:
                Debug.Log("Rare Fourth item Spawned");
                break;
            case 5:
                Debug.Log("Rare Fifth item Spawned");
                break;
            case 6:
                Debug.Log("Rare Six item Spawned");
                break;
            default:
                Debug.Log("No Number was Generated");
                break;
        }
    }
    public void Legendary()
    {
        switch (item)
        {
            case 1:
                Debug.Log("Legendary first item Spawned");
                break;
            case 2:
                Debug.Log("Legendary Second item Spawned");
                break;
            case 3:
                Debug.Log("Legendary Third item Spawned");
                break;
            case 4:
                Debug.Log("Legendary Fourth item Spawned");
                break;
            case 5:
                Debug.Log("Legendary Fifth item Spawned");
                break;
            case 6:
                Debug.Log("Legendary Six item Spawned");
                break;
            default:
                Debug.Log("No Number was Generated");
                break;
        }
    }
    public void Exotic()
    {
        switch (item)
        {
            case 1:
                Debug.Log("Exotic first item Spawned");
                break;
            case 2:
                Debug.Log("Exotic Second item Spawned");
                break;
            case 3:
                Debug.Log("Exotic Third item Spawned");
                break;
            case 4:
                Debug.Log("Exotic Fourth item Spawned");
                break;
            case 5:
                Debug.Log("Exotic Fifth item Spawned");
                break;
            case 6:
                Debug.Log("Exotic Six item Spawned");
                break;
            default:
                Debug.Log("No Number was Generated");
                break;
        }
    }

    public void Artifact()
    {
        switch (item)
        {
            case 1:
                Debug.Log("Artifact first item Spawned");
                break;
            case 2:
                Debug.Log("Artifact Second item Spawned");
                break;
            case 3:
                Debug.Log("Artifact Third item Spawned");
                break;
            case 4:
                Debug.Log("Artifact Fourth item Spawned");
                break;
            case 5:
                Debug.Log("Artifact Fifth item Spawned");
                break;
            case 6:
                Debug.Log("Artifact Six item Spawned");
                break;
            default:
                Debug.Log("No Number was Generated");
                break;
        }
    }
}
