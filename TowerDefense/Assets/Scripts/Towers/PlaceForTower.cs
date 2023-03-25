using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaceForTower : MonoBehaviour, IClickable
{
    [SerializeField] private GameObject buildingPanel;
    private GameObject tower;
    private PlayerStats playerStats;

    private void Update()
    {
        GetServices();
    }

    private void GetServices()
    {
        if (playerStats == null)
            playerStats = ServiceLocator.instance.GetService<PlayerStats>();
    }

    private bool CanSelectThisPlace()
    {
        if (tower == null)
            return true;
        else
            return false;
    }

    public void BuildTower(GameObject towerPrefab)
    {
        if (CanSelectThisPlace())
        {
            AbstractTower tower = towerPrefab.GetComponent<AbstractTower>();

            if (tower.cost <= playerStats.money)
            {
                GameObject newTower = Instantiate(towerPrefab, transform.position, Quaternion.identity);

                playerStats.MinusMoney(tower.cost);
                this.tower = newTower;
                Undo();
            }
        }
    }

    public void Execute()
    {
        if (CanSelectThisPlace())
            buildingPanel.SetActive(true);
    }

    public void Undo()
    {
        buildingPanel.SetActive(false);
    }
}
