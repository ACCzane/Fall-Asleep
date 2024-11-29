using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour
{

    public static EnemyManager Singleton;

    private void Awake() {
        if(Singleton == null){
            Singleton = this;
        }
    }

    [SerializeField] private List<EnemySpawner> enemySpawners;
    public List<Enemy> enemies;
    public void GenerateEnemySoldiors_Day(){
        foreach (var enemySpawner in enemySpawners)
        {
            enemySpawner.canGenEnemy = true;        //鬼魂可以重生
            enemies.Add(enemySpawner.GenEnemy(true));
        }
    }

    public void GenerateEnemySoldiors_Night(){
        foreach (var enemySpawner in enemySpawners)
        {

            //如果场景中的鬼魂还在CD，马上重生它
            //将所有鬼魂的circleLight关闭

            // enemySpawner.canGenEnemy = false;       //关闭鬼魂的重生
            Enemy enemy = enemySpawner.GenEnemy(false);      //有可能为Null
            
            if(enemy != null){
                if(!enemies.Contains(enemy))
                {
                    enemies.Add(enemy);
                }
                //关闭鬼魂的光照效果
                enemy.CircleLight.enabled = false;
            }
                
            //
        }
    }

    public void RemoveEnemy(Enemy enemy){
        enemies.Remove(enemy);
        Destroy(enemy.gameObject);
    }
}
