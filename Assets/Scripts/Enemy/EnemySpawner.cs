using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemySoldiorPrefab;
    [SerializeField] private MovePath movePath;
    [SerializeField] private float rebirthCD = 2f;


    private Enemy_Soldior enemy_Soldior;
    public Enemy_Soldior Enemy_Soldior{
        get{
            return enemy_Soldior;
        }
        private set{
            enemy_Soldior = value;
        }
    }

    public bool canGenEnemy = true;

    private void OnEnable() {
        EventHandler.NightFall += OnNightFall;
    }

    private void OnDisable() {
        EventHandler.NightFall -= OnNightFall;
    }

    private void OnNightFall()
    {
        // enemyLightOn = false;
    }

    public Enemy GenEnemy(bool lightOn){

        if(!canGenEnemy){return null;}

        if(enemy_Soldior != null){
            enemy_Soldior.Initialize(movePath, this);       //重新初始化，敌人位置，状态改变
            return enemy_Soldior;
        }

        GameObject enemyGO = Instantiate(enemySoldiorPrefab);
        enemy_Soldior = enemyGO.GetComponent<Enemy_Soldior>();
        enemy_Soldior.Initialize(movePath, this);

        return enemy_Soldior;
    }

    // public void EnemyRebirth(){
    //     StartCoroutine(EnemyRibirthAsync());
    // }

    private IEnumerator EnemyRibirthAsync(){
        float elapsedTime = 0;
        while(elapsedTime < rebirthCD){
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        if(!canGenEnemy){yield break;}

        if(enemy_Soldior.isActiveAndEnabled){Debug.Log("该路线上的敌人已存在"); yield break;}

        // GenEnemy(enemyLightOn);
        // enemy_Soldior.transform.position = movePath.PosNodes[0];
        // enemy_Soldior.ChangeState(EnemyState.Move);
        enemy_Soldior.Initialize();
        enemy_Soldior.gameObject.SetActive(true);
        
    }

    public void DespawnEnemy(){
        enemy_Soldior.gameObject.SetActive(false);

        StartCoroutine(EnemyRibirthAsync());
    }

    //鬼魂执行攻击动作
    // private void OnAttack()
    // {
    //     Destroy(enemy_Soldior.gameObject);
    //     if(canGenEnemy){
    //         EnemyRebirth();
    //     }
        
    // }
}
