using System.Collections;
using UnityEngine;

public class RotateSight : MonoBehaviour
{
    [SerializeField] Transform sightTransform;
    [SerializeField] RoundingView roundingView;

    public IEnumerator RotateToRightZValue(float targetZValue, float timeDuration) {

        float elapsedTime = 0f;

        // 获取初始角度，确保它在 0 到 360 度之间
        if(sightTransform==null){yield break;}
        float initialAngle = sightTransform.localEulerAngles.z;
        if (initialAngle < 0) {
            initialAngle += 360;
        }

        // 确保目标角度也是正值
        if (targetZValue < 0) {
            targetZValue += 360;
        }

        // 计算角度差
        float angleDifference = targetZValue - initialAngle;

        // 确保 angleDifference 在 -180 到 180 度之间
        if (angleDifference > 180) {
            angleDifference -= 360; // 顺时针转
        } else if (angleDifference < -180) {
            angleDifference += 360; // 逆时针转
        }

        while (elapsedTime < timeDuration) {

            elapsedTime += Time.deltaTime;

            // 计算当前的 Z 轴角度
            float zValue = initialAngle + angleDifference * (elapsedTime / timeDuration);

            // 设置旋转
            if(sightTransform==null){yield break;}
            sightTransform.localRotation = Quaternion.Euler(0, 0, zValue);

            yield return null;
        }

        // 确保最后设置为目标旋转
        sightTransform.localRotation = Quaternion.Euler(0, 0, targetZValue);
    }


    /// <summary>
    /// 根据z的角度来旋转
    /// </summary>
    /// <param name="zRot"></param>
    public void UpdateSightRotationImmiediate(float zRot){
        sightTransform.localRotation = Quaternion.Euler(0, 0, zRot);
    }

    public IEnumerator LookingAround() {

        foreach (var targetEulerAngles_z in roundingView.eulerAngles_z) {
            float elapsedTime = 0f;
            float timeDuration = Random.Range(1f, 3f);

            // 获取初始 Z 轴角度，并确保在 0 到 360 度之间
            float initialEulerZValue = sightTransform.eulerAngles.z;
            if (initialEulerZValue < 0) {
                initialEulerZValue += 360;
            }

            // 确保目标角度也是正值
            float targetEulerZValue = targetEulerAngles_z;
            if (targetEulerZValue < 0) {
                targetEulerZValue += 360;
            }

            // 计算角度差
            float angleDifference = targetEulerZValue - initialEulerZValue;

            // 确保 angleDifference 在 -180 到 180 度之间
            if (angleDifference > 180) {
                angleDifference -= 360; // 顺时针转
            } else if (angleDifference < -180) {
                angleDifference += 360; // 逆时针转
            }

            while (elapsedTime < timeDuration) {

                elapsedTime += Time.deltaTime;

                // 计算当前的 Z 轴角度
                float currentZValue = initialEulerZValue + angleDifference * (elapsedTime / timeDuration);

                // 设置旋转
                if(sightTransform==null){yield break;}
                sightTransform.eulerAngles = new Vector3(0, 0, currentZValue);

                yield return null;
            }

            // 确保最后设置为目标旋转
            sightTransform.eulerAngles = new Vector3(0, 0, targetEulerZValue);
        }
    }
}
