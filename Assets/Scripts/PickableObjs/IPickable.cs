using UnityEngine;

public interface IPickable{
    public void Pick(HoldObj player);
    public void Drop(HoldObj player);
}