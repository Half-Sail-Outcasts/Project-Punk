using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadomNumberScript : MonoBehaviour
{
    // Start is called before the first frame update

    public int spawn;
    public GameObject object1;
    public GameObject object2;

    public int rariety;
    public int item;
    public int value;

    void Start()
    {

        rariety = Random.Range(1, 9);
        item = Random.Range(1, 9);
        value = Random.Range(1, 9);
        Debug.Log("Rare" + rariety);
        Debug.Log("item" + item);
        Debug.Log("value" + value);
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
