using UnityEngine;


public class MapTrigger : MonoBehaviour
{
    public enum Direction { UpDown, LeftRight }

    [SerializeField] private Direction direction; // 下拉框选择方向
    public Direction DirectionValue
    {
        get { return direction; }
        set { direction = value; }
    }
    [SerializeField] private GameObject[] l_U_Tip; // 左上提示
    public GameObject[] L_U_Tip
    {
        get { return l_U_Tip; }
        set { l_U_Tip = value; }
    }
    [SerializeField] private GameObject[] r_D_Tip; // 右下提示
    public GameObject[] R_D_Tip
    {
        get { return r_D_Tip; }
        set { r_D_Tip = value; }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 逻辑实现
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            ChoosePlayTip(other.transform);
        }
    }

    // 选择提示逻辑
    private void ChoosePlayTip(Transform playerTransform)
    {
        // 实现提示逻辑
    }
}
