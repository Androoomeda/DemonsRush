using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage;
    [HideInInspector] public GameObject target;

    [SerializeField] private ParticleSystem bulletDestroyEffect;
    [SerializeField] private float moveSpeed;

    private void FixedUpdate()
    {
        if (target != null)
        {
            transform.LookAt(target.transform);
            transform.position += transform.forward * moveSpeed * Time.deltaTime;
        }
        else
            Destroy(gameObject);
    }

    private void OnTriggerStay(Collider collider)
    {
        EnemyHealth enemyHealth;
        if (collider.gameObject == target)
        {
            if(collider.gameObject.TryGetComponent<EnemyHealth>(out enemyHealth))
            {
                enemyHealth.TakeDamage(damage);
                Destroy(gameObject);
            }
        }
    }

    private void OnDestroy()
    {
        Instantiate(bulletDestroyEffect, transform.position, Quaternion.identity);
    }
}
