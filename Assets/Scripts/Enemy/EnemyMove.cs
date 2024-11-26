using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{

    [SerializeField] private RotateSight rotateSight;

    private MovePath movePath;
    [SerializeField] private float enemySpeed;

    private int currentHeadingNodeIndex;
    private List<Vector2> posNodes;
    private int totalPos;
    private bool reverse;
    private Vector2 faceDirVec2;
    private float faceDirZEulerAngle;
    private bool isMoving = false;
    
    private Coroutine rotationCorotine;

    private void Update() {
        if(isMoving)
            Move();
    }

    public void SetMovPath(MovePath movePath){
        this.movePath = movePath;
    }

    private void Move() {
        if (totalPos == 0) return; // 如果没有路径节点，直接退出

        // 计算目标位置
        Vector2 targetPosition = posNodes[currentHeadingNodeIndex];

        // 移动敌人
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, enemySpeed * Time.deltaTime);

        // 检查是否到达目标位置
        if (Vector2.Distance(transform.position, targetPosition) < 0.1f) {

            LookAround();

            //决定是否逆转节点顺序，实现往回走的功能
            if(!reverse)
                currentHeadingNodeIndex++; // 更新目标节点索引
            else
                currentHeadingNodeIndex--;

                
            if (currentHeadingNodeIndex >= totalPos) {
                currentHeadingNodeIndex-=2;
                reverse = !reverse;
            }
            if (currentHeadingNodeIndex < 0){
                currentHeadingNodeIndex+=2;
                reverse = !reverse;
            }

            targetPosition = posNodes[currentHeadingNodeIndex];
        }

        
        //获取朝向，更新视角
        faceDirVec2 = (targetPosition - (Vector2)transform.position).normalized;

        if (faceDirVec2 != Vector2.zero) {
            // 计算所需的旋转角度
            faceDirZEulerAngle = Mathf.Atan2(faceDirVec2.y, faceDirVec2.x) * Mathf.Rad2Deg;

            // rotateSight.UpdateSightRotationImmiediate(faceDirZEulerAngle);
        }
    }


    private void LookAround(){
        rotationCorotine = StartCoroutine(LookAroundAsync());
    }

    private IEnumerator LookAroundAsync(){
        isMoving = false;

        yield return rotateSight.LookingAround();

        yield return rotateSight.RotateToRightZValue(faceDirZEulerAngle,2f);

        isMoving = true;
    }

    public void RespawnSelf(){
        //获取路径
        posNodes = movePath.GetPos();
        totalPos = posNodes.Count;
        reverse = false;
        transform.position = posNodes[0];
        currentHeadingNodeIndex = 1;

        faceDirVec2 = posNodes[1] - posNodes[0];
        faceDirZEulerAngle = Mathf.Atan2(faceDirVec2.y, faceDirVec2.x) * Mathf.Rad2Deg;
        LookAround();
    }

    private void OnDestroy() {
        if(rotationCorotine != null)
            StopCoroutine(rotationCorotine);
    }

}
