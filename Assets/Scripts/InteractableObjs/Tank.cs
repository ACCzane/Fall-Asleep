using UnityEngine;

public class Tank : MonoBehaviour, IInteractable
{

    private bool isBroken = true;
    [SerializeField] private SpriteRenderer tankSpr;
    [SerializeField] private Sprite tankBrokenSp;
    [SerializeField] private Sprite tankNotBrokenSp;
    public void Interact()
    {

        if(isBroken){
            EventHandler.CallUseItemInHand();   //将手上的物品销毁
            //修好电箱
            tankSpr.sprite = tankNotBrokenSp;
            tankSpr.color = Color.green;        //先用颜色代替
        }
        isBroken = false;

    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player"){
            other.GetComponent<PlayerControl>().currentInteractableTarget = this;
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Player"){
            if(other.GetComponent<PlayerControl>().currentInteractableTarget == this)
                other.GetComponent<PlayerControl>().currentInteractableTarget = null;
        }
    }
}
