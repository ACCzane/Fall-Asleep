using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private MovePath movePath;
    [SerializeField] private float rebirthCD = 2f;
    private GameObject enemyGO;

    private void OnEnable() {
        EventHandler.Attack += OnAttack;
    }

    private void OnDisable() {
        EventHandler.Attack -= OnAttack;
    }

    public void GenEnemy(){
        enemyGO = Instantiate(enemyPrefab);
        Enemy enemy = enemyGO.GetComponent<Enemy>();
        enemy.Initialize(movePath);
    }

    public void EnemyRebirth(){
        StartCoroutine(EnemyRibirthAsync());
    }

    private IEnumerator EnemyRibirthAsync(){

        if(enemyGO != null){yield break;}

        float elapsedTime = 0;
        while(elapsedTime < rebirthCD){
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        GenEnemy();
    }

    private void OnAttack()
    {
        Destroy(enemyGO);
        EnemyRebirth();
    }
}
