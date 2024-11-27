using UnityEngine;

public class DetectPlayer : MonoBehaviour
{

    [SerializeField] private Enemy enemy;

    private void OnTriggerStay2D(Collider2D player) {
        if (player.CompareTag("Player")) {
            if (!player.GetComponent<Hide>().IsHiding)
            FindPlayer(player.transform);
        }
    }

    private void FindPlayer(Transform transform){
        //enemy注册Player的Transform
        enemy.TargetPlayer(transform);
        //enemy通知stateMachine进入Found状态
        enemy.ChangeState(EnemyState.Found);
    }
}
