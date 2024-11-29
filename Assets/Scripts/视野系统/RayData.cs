using UnityEngine;
 
public class RayData
{
    public Vector2 m_start;
    public float m_distance;
    public float m_angle;
    public Vector2 m_direction;
    public Vector2 m_end;
    public Collider2D m_hitCollider;
    public bool m_hit;
 
    /// <summary>
    /// 
    /// </summary>
    /// <param name="start">起始位置</param>
    /// <param name="angle">从X轴正方向出发的角度</param>
    /// <param name="distance"></param>
    public RayData(Vector2 start, float angle, float distance)
    {
        m_start = start;
        m_distance = distance;
 
        UpdateDirection(angle);
    }
 
    /// <summary>
    /// 
    /// </summary>
    /// <param name="angle">距离X正轴的角度</param>
    public void UpdateDirection(float angle)
    {
        m_angle = angle;
        m_direction = DirectionFromAngle(m_angle);
        m_end = m_start + m_direction * m_distance;
    }
 
    private Vector3 DirectionFromAngle(float angle)
    {
        return new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));            //适用于2D场景
        // return new Vector2(1,0);
    }
 
    public static bool IsHittingSameObject(RayData data1, RayData data2)
    {
        return data1.m_hitCollider == data2.m_hitCollider;
    }

}