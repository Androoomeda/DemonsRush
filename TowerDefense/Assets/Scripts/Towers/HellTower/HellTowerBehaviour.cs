using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HellTowerBehaviour : AbstractTower
{
    [Space(20)]
    [SerializeField] private Transform hellSphere;
    [SerializeField] private int minDamage, maxDamage;
    [SerializeField] Vector3 sphereRotation;

    public int currentDamage;
    private LineRenderer lineRenderer;

    private void Start()
    {
        base.Start();
        currentDamage = minDamage;
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.SetPosition(0, hellSphere.position);
    }

    private void Update()
    {
        base.Update();

        RotateHellSphere();
        UpdateLaser();

        if (targetEnemy != null && isReloaded())
            Attack();
        else if (targetEnemy == null)
            currentDamage = minDamage;
    }

    private void RotateHellSphere()
    {
        hellSphere.transform.Rotate(sphereRotation * Time.deltaTime);
    }

    protected override void Attack()
    {
        EnemyHealth enemyHealth = targetEnemy.GetComponent<EnemyHealth>();
        enemyHealth.TakeDamage(currentDamage);
        reloadTimeLeft = reloadTime;
        if (currentDamage < maxDamage)
            currentDamage++;
    }

    private void UpdateLaser()
    {
        if(targetEnemy == null)
            lineRenderer.positionCount = 1;
        else
        {
            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(1, targetEnemy.transform.position);
        }
    }
}
