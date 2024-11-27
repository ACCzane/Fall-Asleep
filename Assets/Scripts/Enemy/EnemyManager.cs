using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private List<EnemySpawner> enemySpawners;

    public void GenerateEnemies_Day(){
        foreach (var enemySpawner in enemySpawners)
        {
            enemySpawner.GenEnemy();
        }
    }

    public void GenerateEnemies_Night(){

    }
}
