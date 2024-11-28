using UnityEngine;

public interface IPickable{
    public void Pick(Transform player);
    public void Drop();
}