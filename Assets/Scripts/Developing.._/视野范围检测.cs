using UnityEngine;

public class 视野范围检测 : MonoBehaviour
{

    [SerializeField] private LayerMask layerMask;

    [SerializeField] private int _divide;
    [SerializeField] private float _angle;
    [SerializeField] private float _radius;
    // private RaycastHit _hit;
    // private RaycastHit2D _hit;

    private RayData[] rayDatas;

    [SerializeField] private 程序化Mesh programmableMesh;


    //在最一开始先透过简单的运算，计算出射线的起始点、方向与半径
    private RayData[] GetOriginalDatas()
    {        
        RayData[] rayDatas = new RayData[_divide + 1];
    
        Vector2 center = transform.position;
        float startAngle = -_angle / 2;                 //指向X正方向
        float angle = _angle / _divide;
        RayData rayDataCache = null;
    

        float angleOffset = transform.eulerAngles.z; //当前世界坐标下，transform的x轴与坐标系x轴的角度


        for(int i = 0; i <= _divide; i++)
        {
            rayDataCache = new RayData(center, startAngle + angle * i + angleOffset, _radius);
    
            rayDatas[i] = rayDataCache;
        }
    
        return rayDatas;
    }

    private RayData[] GetNormalDatas()
    {        
        RayData[] rayDatas = GetOriginalDatas();
    
        for (int i = 0; i < rayDatas.Length; i++)
        {
            UpdateRaycast(rayDatas[i]);
        }
    
        return rayDatas;
    }
 
    private void UpdateRaycast(RayData rayData)
    {

        RaycastHit2D hit = Physics2D.Raycast(transform.position, rayData.m_direction, _radius, layerMask);

        if(hit.collider == null){
            rayData.m_hit = false;
        }else{
            rayData.m_hit = true;
        }


        // rayData.m_hit = Physics.Raycast(transform.position, rayData.m_direction, out _hit, _radius);
        

        if (rayData.m_hit)
        {
            rayData.m_hitCollider = hit.collider;
            rayData.m_end = hit.point;
        }
        else
        {
            rayData.m_hitCollider = null;
            rayData.m_end = rayData.m_start + rayData.m_direction * _radius;
        }

        Debug.DrawRay(rayData.m_start, rayData.m_end - rayData.m_start, Color.red);
        
    }

    private void Start() {
        programmableMesh.SetRayDatas(rayDatas);
    }

    private void Update() {
        rayDatas = GetNormalDatas();
        foreach (var item in rayDatas)
        {
            UpdateRaycast(item);
            programmableMesh.SetRayDatas(rayDatas);
            programmableMesh.GenerateMesh();
        }
    }

}