using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketBehaviour : AbstractTower
{
    [Space(20)]
    [SerializeField] private float rotationSpeed;
    [SerializeField] private Transform horizontalRotationPart;
    [SerializeField] private Transform verticalRotationPart;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletSpawner;

    private void Start()
    {
        base.Start();
    }

    private void Update()
    {
        base.Update();

        if (targetEnemy != null)
        {
            RotateToTarget();
            if (isReloaded())
                Attack();
        }
    }

    protected override void Attack()
    {  
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawner.position, Quaternion.LookRotation(targetEnemy.transform.position - bulletSpawner.position, Vector3.up));
        bullet.GetComponent<Bullet>().target = this.targetEnemy;
        reloadTimeLeft = reloadTime;
    }

    private void RotateToTarget()
    {
        Quaternion lookRotation = Quaternion.LookRotation(targetEnemy.transform.position - verticalRotationPart.position);
        Vector3 horizontalRotation = Quaternion.Lerp(horizontalRotationPart.rotation, lookRotation, rotationSpeed * Time.deltaTime).eulerAngles;
        Vector3 verticalRotation = Quaternion.Lerp(verticalRotationPart.rotation, lookRotation, rotationSpeed * Time.deltaTime).eulerAngles;
        horizontalRotationPart.rotation = Quaternion.Euler(0f, horizontalRotation.y, 0f);
        verticalRotationPart.localRotation = Quaternion.Euler(verticalRotation.x, 0, 0);
    }
}
