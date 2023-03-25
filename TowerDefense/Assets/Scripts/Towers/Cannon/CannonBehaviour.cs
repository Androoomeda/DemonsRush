using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBehaviour : AbstractTower
{
    [Space(20)]
    [SerializeField] private float rotationSpeed;
    [SerializeField] private Transform partToRotate;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletSpawner;
    [SerializeField] private ParticleSystem shootEffect;


    private void Start()
    {
        base.Start();
    }

    private void Update()
    {
        base.Update();

        if(targetEnemy != null)
        {
            RotateToTarget();
            if (isReloaded())
                Attack();
        }
    }

    protected override void Attack()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawner.position, Quaternion.identity);
        bullet.GetComponent<Bullet>().target = targetEnemy;
        shootEffect.Play();
        reloadTimeLeft = reloadTime;
    }

    private void RotateToTarget()
    {
        Quaternion lookRotation = Quaternion.LookRotation(targetEnemy.transform.position - partToRotate.position);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, rotationSpeed * Time.deltaTime).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }
}
