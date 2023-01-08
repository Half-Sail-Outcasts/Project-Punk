using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    private float reloadTime;

    [SerializeField]
    private Arrow arrowPrefab;

    [SerializeField]
    private Transform spawnPoint;

    private Arrow currentArrow;

    private string enemyTag;

    private bool isReloading;

    public void SetEnemyTag(string enemyTag)
    {
        this.enemyTag = enemyTag;
    }

    public void reload()
    {
        Debug.Log("Enter Reload");
        if (isReloading || currentArrow != null) return;
        Debug.Log("Pass If statement");
        isReloading = true;
        StartCoroutine(ReloadsAfterTime());
    }

    private IEnumerator ReloadsAfterTime()
    {
        Debug.Log("I am working");
        yield return new WaitForSeconds(reloadTime);
        currentArrow = Instantiate(arrowPrefab, spawnPoint);
        currentArrow.transform.localPosition = Vector3.zero;
        currentArrow.SetEnemyTag(enemyTag);
        isReloading = false;
    }

    public void Fire(float firePower)
    {
        if (isReloading || currentArrow == null) return;
        var force = spawnPoint.TransformDirection(Vector3.forward * firePower);
        currentArrow.Fly(force);
        currentArrow = null;
        reload();
    }

    public bool IsReady()
    {
        return (!isReloading && currentArrow != null);
    }
}
