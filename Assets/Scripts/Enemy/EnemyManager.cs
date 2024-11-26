using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private List<EnemySpawner> enemySpawners;

    [SerializeField] private Button button;

    private void OnEnable() {
        foreach (var enemySpawner in enemySpawners)
        {
            button.onClick.AddListener(enemySpawner.GenEnemy);
        }
    }
}
