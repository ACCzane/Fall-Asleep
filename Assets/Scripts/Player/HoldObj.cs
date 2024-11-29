using UnityEngine;

public class HoldObj : MonoBehaviour
{
    public Transform holdAnchor;

    public IPickable holdingObj;            //玩家正拿着的物品
    public IPickable currentTargetObj;     //在场景中被玩家检测到的物品
    public IInteractable interactableObj;
    private bool isHolding;

    private void OnEnable() {
        EventHandler.UseItemInHand += OnUseItemInHand; 
    }
    private void OnDisable() {
        EventHandler.UseItemInHand -= OnUseItemInHand;
    }

    private void OnUseItemInHand()
    {
        if(holdingObj == null){
            return;
        }
        Destroy((holdingObj as MonoBehaviour).gameObject);
        holdingObj = null;

    }

    private void Update() {
        //
        // Debug.Log(holdingObj);
    }

}
