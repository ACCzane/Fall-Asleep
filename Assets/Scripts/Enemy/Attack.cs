using System.Collections;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] private SightCheck sightCheck;

    [SerializeField] private float chaseTime = 1f;

    private void OnEnable() {
        EventHandler.FindPlayer += OnFindPlayer;
    }

    private void OnDisable() {
        EventHandler.FindPlayer -= OnFindPlayer;
    }

    private void OnFindPlayer(Transform transform)
    {
        StartCoroutine(AttackTarget(transform));
    }

    /// <summary>
    /// 鬼魂发现玩家后直接飞到玩家头顶，然后播放动画，玩家逃不掉（速度再快也没用）
    /// </summary>
    /// <param name="targetTransform"></param>
    /// <returns></returns>
    private IEnumerator AttackTarget(Transform targetTransform){
        yield return StartCoroutine(ChaseTarget(targetTransform));

        EventHandler.CallAttack();
    }

    private IEnumerator ChaseTarget(Transform targetTransform){
        float timeDuration = chaseTime;

        float elapsedTime = 0;

        Vector3 primaryPos = transform.position;
        Vector3 targetPos = targetTransform.position;

        Vector3 lerpedPos = new Vector3();

        while(elapsedTime < timeDuration){
            elapsedTime += Time.deltaTime;
            //更新目标位置（玩家可能逃跑）
            targetPos = targetTransform.position;

            lerpedPos = Vector3.Lerp(primaryPos, targetPos, elapsedTime/timeDuration);

            transform.position = lerpedPos;

            yield return null;
        }
    }
}
