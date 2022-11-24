using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadomNumberScript : MonoBehaviour
{
    // Start is called before the first frame update

    public int spawn;
    public GameObject object1;
    public GameObject object2;

    void Start()
    {
        spawn = Random.Range(1, 1000);
        Debug.Log(spawn);
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
        if(spawn <= 500)
        {
            GameObject newObject = Instantiate(object1);
            return;
        }
        else
        {
            GameObject newObject = Instantiate(object2);
            return;
        }
    }
}
