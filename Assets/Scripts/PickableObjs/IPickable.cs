using UnityEngine;

public interface IPickable{
    public void Pick(HoldObj playerHoldObj);
    public void Drop(HoldObj playerHoldObj);
}