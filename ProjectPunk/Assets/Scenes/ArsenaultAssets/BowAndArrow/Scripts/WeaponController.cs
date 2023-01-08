using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponController : MonoBehaviour
{
    [SerializeField]
    private Text firePowerText;

    [SerializeField]
    private Weapon weapon;

    [SerializeField]
    private string enemyTag;

    [SerializeField]
    private float maxFrePower;

    [SerializeField]
    private float firePowerSpeed;

    private float firePower;

    [SerializeField]
    private float rotateSpeed;

    [SerializeField]
    private float minRotation;

    [SerializeField]
    private float maxRotation;

    private float mouseY;

    private bool fire;

    void Start()
    {
        weapon.SetEnemyTag(enemyTag);
        Debug.Log("1st reload");
        weapon.reload();
    }

    void Update()
    {
        mouseY += Input.GetAxis("Mouse Y") * rotateSpeed;
        mouseY = Mathf.Clamp(mouseY, minRotation, maxRotation);
        weapon.transform.localRotation = Quaternion.Euler(mouseY, weapon.transform.localEulerAngles.y, weapon.transform.localEulerAngles.z);

        if(Input.GetMouseButtonDown(0))
        {
            fire = true;
        }

        if(fire && firePower < maxFrePower)
        {
            firePower += Time.deltaTime * firePowerSpeed;
        }

        if(fire && Input.GetMouseButtonUp(0))
        {
            weapon.Fire(firePower);
            firePower = 0;
            fire = false;
        }

        if(fire)
        {
            firePowerText.text = firePower.ToString();
        }
    }


}
