using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SightCheck : MonoBehaviour
{
    private PolygonCollider2D polygonCollider;
    [SerializeField] private List<Transform> sightEnds;
    [SerializeField] private Transform sightStart;
    [SerializeField] private LayerMask layerMask;

    [SerializeField] private Enemy enemy;

    private void Awake() {
        polygonCollider = GetComponent<PolygonCollider2D>();
    }

    private void OnTriggerStay2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            //玩家只有先进入视锥的碰撞体，才会执行射线检测
            Debug.Log("碰撞");
            CheckPlayerVisibility(other);
        }
    }


    private void CheckPlayerVisibility(Collider2D player) {
        if (!player.GetComponent<Hide>().IsHiding) { // 检测玩家是否隐藏
            foreach (Transform sightEnd in sightEnds) {
                Vector2 rayDirection = sightEnd.position - sightStart.position; // 计算射线的方向
                // 射线检测
                RaycastHit2D hit = Physics2D.Raycast(sightStart.position, rayDirection.normalized, rayDirection.magnitude, layerMask);

                // 检查射线是否击中玩家
                if (hit.collider != null && hit.collider.CompareTag("Player")) {
                    Debug.Log("检测到玩家");
                    FindPlayer(hit.transform);
                    return; // 找到玩家后立即返回，跳出循环
                }

            }
        }
    }

    private void FindPlayer(Transform transform){
        //enemy注册Player的Transform
        enemy.TargetPlayer(transform);
        //enemy通知stateMachine进入Found状态
        enemy.ChangeState(EnemyState.Found);
    }
    
}
