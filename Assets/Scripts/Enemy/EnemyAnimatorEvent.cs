using UnityEngine;

/// <summary>
/// 该脚本纯用来给Animator添加事件
/// </summary>
public class EnemyAnimatorEvent : MonoBehaviour
{

    public void DestroySelf(){
        EnemyManager.Singleton.RemoveEnemy(transform.parent.GetComponent<Enemy>());
    }
}
