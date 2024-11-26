using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private MovePath movePath;
    [SerializeField] private float rebirthCD = 2f;

    private GameObject enemyGO;
    private bool canGenEnemy = true;

    private void OnEnable() {
        EventHandler.Attack += OnAttack;
    }

    private void OnDisable() {
        EventHandler.Attack -= OnAttack;
    }

    public void GenEnemy(){

        if(enemyGO != null){Debug.Log("该路线上的敌人已存在"); return;}

        enemyGO = Instantiate(enemyPrefab);
        Enemy enemy = enemyGO.GetComponent<Enemy>();
        enemy.Initialize(movePath, this);
    }

    public void EnemyRebirth(){
        StartCoroutine(EnemyRibirthAsync());
    }

    private IEnumerator EnemyRibirthAsync(){
        float elapsedTime = 0;
        while(elapsedTime < rebirthCD){
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        if(enemyGO != null){Debug.Log("该路线上的敌人已存在"); yield break;}

        GenEnemy();
    }

    private void OnAttack()
    {
        Destroy(enemyGO);
        EnemyRebirth();
    }
}
