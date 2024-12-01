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


        if (other.CompareTag("Player") || other.CompareTag("EnemyCanNotice")) {
            //玩家只有先进入视锥的碰撞体，才会执行射线检测
            CheckCollider(other);
        }
    }


    private void CheckCollider(Collider2D other) {
        
        foreach (Transform sightEnd in sightEnds) {
            Vector2 rayDirection = sightEnd.position - sightStart.position; // 计算射线的方向
            // 射线检测
            RaycastHit2D hit = Physics2D.Raycast(sightStart.position, rayDirection.normalized, rayDirection.magnitude, layerMask);

            // 检查射线是否击中玩家
            if (hit.collider != null && hit.collider.CompareTag("Player")) {

                if(!other.TryGetComponent<Hide>(out Hide hide)){
                    //游戏本身没发现逻辑bug，但是编辑器报错
                    return;
                }

                if(!other.GetComponent<Hide>().IsHiding){
                    //如果玩家没躲藏

                    // Debug.Log("检测到玩家");
                    FindPlayer(hit.transform);
                    return; // 找到玩家后立即返回，跳出循环
                }
                
            }

            //检测射线是否击中其他鬼魂能看见的物体
            if(hit.collider != null && hit.collider.CompareTag("EnemyCanNotice")){
                Debug.Log("电箱！");
                if(other.TryGetComponent<Tank>(out Tank tank)){
                    Debug.Log(tank.IsBroken ? "Broken" : "NotBroken");
                    if(!tank.IsBroken){
                        //如果电箱被玩家修好

                        FindFixedTank(hit.transform);
                        return;
                    }
                }
            }

        }
        
    }

    private void FindPlayer(Transform transform){
        //enemy注册Player的Transform
        enemy.Target(transform);
        //enemy通知stateMachine进入Found状态
        enemy.ChangeState(EnemyState.FoundPlayer);
    }
    
    private void FindFixedTank(Transform transform){
        enemy.Target(transform);
        enemy.ChangeState(EnemyState.FoundFixedTank);
    }

}
