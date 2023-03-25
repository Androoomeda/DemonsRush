using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public int damage;
    [HideInInspector] public Transform waypoints;

    [SerializeField] private float moveSpeed;

    private EnemyHealth enemyHealth;
    private float rotationDuration = 0.1f;
    private int pointIndex = 0;
    private Vector3 currentPointPos; 
    private Vector3 currentTarget; 

    private void Start()
    {
        enemyHealth = GetComponent<EnemyHealth>();
        currentPointPos = waypoints.GetChild(0).position;
        currentTarget = new Vector3(currentPointPos.x, transform.position.y, currentPointPos.z);
    }

    private void FixedUpdate()
    {
        MoveToNextPoint();
        RotateToPoint();
    }

    private void MoveToNextPoint()
    {
        Vector3 direction = currentTarget - transform.position;
        transform.Translate(direction.normalized * moveSpeed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, currentTarget) <= 0.1f)
            GetNextPoint();
    }

    private void GetNextPoint()
    {
        if (pointIndex >= waypoints.childCount - 1)
        {
            enemyHealth.Dead();
            ServiceLocator.instance.GetService<PlayerStats>().MinusLives();
            return;
        }

        pointIndex++;
        currentPointPos = waypoints.GetChild(pointIndex).position;
        currentTarget = new Vector3(currentPointPos.x, transform.position.y, currentPointPos.z);
    }

    private void RotateToPoint()
    {
        Quaternion lookRotation = Quaternion.LookRotation(currentTarget - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotationDuration);
    }
}
