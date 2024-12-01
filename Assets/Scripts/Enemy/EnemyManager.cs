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

    [SerializeField] private List<EnemySpawner> ghostSoldiorSpawners;
    public List<Enemy> ghostSoldiors;
    public void GenerateEnemySoldiors_Day(){
        foreach (var enemySpawner in ghostSoldiorSpawners)
        {
            enemySpawner.canGenEnemy = true;        //鬼魂可以重生
            ghostSoldiors.Add(enemySpawner.GenEnemy(true));
        }
    }

    public void GenerateEnemySoldiors_Night(){
        foreach (var enemySpawner in ghostSoldiorSpawners)
        {

            //如果场景中的鬼魂还在CD，马上重生它
            //将所有鬼魂的circleLight关闭

            // enemySpawner.canGenEnemy = false;       //关闭鬼魂的重生
            Enemy enemy = enemySpawner.GenEnemy(false);      //有可能为Null
            
            if(enemy != null){
                if(!ghostSoldiors.Contains(enemy))
                {
                    ghostSoldiors.Add(enemy);
                }
                //关闭鬼魂的光照效果
                // enemy.CircleLight.enabled = false;
            }
                
            //
        }
    }

    // public void RemoveEnemy(Enemy enemy){
    //     ghostSoldiors.Remove(enemy);
    //     // Destroy(enemy.gameObject);

    // }
}
