using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    #region Var

    [SerializeField]
    private float damage;

    [SerializeField]
    private float torgue;

    [SerializeField]
    private Rigidbody rb;

    private string enemyTag;

    private bool didHit;

    #endregion

    public void SetEnemyTag(string enemyTag)
    {
        this.enemyTag = enemyTag;
    }

    public void Fly(Vector3 force)
    {
        rb.isKinematic = false;
        rb.AddForce(force, ForceMode.Impulse);
        rb.AddTorque(transform.right * torgue);
        transform.SetParent(null);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (didHit) return;
        didHit = true;

        if(collider.CompareTag(enemyTag))
        {
            //var health = collider.GetComponent<HealthController>();
            //health.ApplyDamage(damage);
        }

        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.isKinematic = true;
        transform.SetParent(collider.transform);
    }
}
