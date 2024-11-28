using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private List<EnemySpawner> enemySpawners;

    public void GenerateEnemySoldiors_Day(){
        foreach (var enemySpawner in enemySpawners)
        {
            enemySpawner.canGenEnemy = true;        //鬼魂可以重生
            enemySpawner.GenEnemy();
        }
    }

    public void GenerateEnemySoldiors_Night(){
        foreach (var enemySpawner in enemySpawners)
        {
            enemySpawner.canGenEnemy = false;       //关闭鬼魂的重生
            Enemy enemy = enemySpawner.GenEnemy();

            //关闭鬼魂的光照效果
            enemy.CircleLight.enabled = false;
            //
        }
    }
}
