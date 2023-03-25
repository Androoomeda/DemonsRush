using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractTower : MonoBehaviour
{
    public GameObject targetEnemy { get; private set; }
    public int cost { get => _cost; }

    [Header("Base")]
    [SerializeField] LayerMask targetLayer;
    [SerializeField] protected float reloadTime;
    [SerializeField] private int _cost;
    [SerializeField] private float range;

    protected float reloadTimeLeft;

    private Collider[] touchedColliders = new Collider[20];

    protected void Start()
    {
        StartCoroutine(CheckTarget());
    }

    protected void Update()
    {
        if (!isReloaded())
            Reload();
    }

    protected abstract void Attack();


    protected IEnumerator CheckTarget()
    {
        if (targetEnemy == null)
            TryFindEnemy();
        else if (Vector3.Distance(transform.position, targetEnemy.transform.position) > range)
            targetEnemy = null;
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(CheckTarget());
    }

    protected void TryFindEnemy()
    {
        float maxPosX = float.MinValue;

        Physics.OverlapSphereNonAlloc(transform.position, range, touchedColliders, targetLayer);

        foreach (var collider in touchedColliders)
        {
            if (collider == null)
                return;

            GameObject enemy = collider.gameObject;
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

            if (distanceToEnemy < range && enemy.transform.position.x > maxPosX)
            {
                maxPosX = enemy.transform.position.x;
                targetEnemy = enemy;
            }
        }
    }

    protected bool isReloaded()
    {
        if (reloadTimeLeft > 0)
            return false;
        else
            return true;
    }

    protected void Reload()
    {
        reloadTimeLeft -= Time.deltaTime;
    }

    protected void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
