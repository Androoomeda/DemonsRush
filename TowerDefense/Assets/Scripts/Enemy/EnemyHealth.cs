using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private ParticleSystem deadEffect;
    [SerializeField] private int moneyForDeath;
    [SerializeField] private int maxHealth;

    private int currentHealth;
    private SpawnSystem spawnSystem;
    private PlayerStats playerStats;

    private void OnEnable()
    {
        currentHealth = maxHealth;
        playerStats = ServiceLocator.instance.GetService<PlayerStats>();
        spawnSystem = ServiceLocator.instance.GetService<SpawnSystem>();

        spawnSystem.aliveEnemies++;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if(currentHealth <= 0)
            Dead();
    }

    public void Dead()
    {
        Instantiate(deadEffect, transform.position, Quaternion.identity);
        playerStats.AddMoney(moneyForDeath);
        spawnSystem.aliveEnemies--;
        Destroy(this.gameObject);
    }
}
