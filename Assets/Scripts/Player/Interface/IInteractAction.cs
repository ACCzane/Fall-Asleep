using UnityEngine;

public interface IInteractAction
{
    /// <summary>
    /// 设置玩家点击进行交互时位置移动所需的锚点
    /// </summary>
    public void SetPos(Vector2 inPos, Vector2 outPos);
    /// <summary>
    /// 设置交互按钮的打开与关闭
    /// </summary>
    /// <param name="open"></param>
    public void TurnHintButton(bool open);
}
