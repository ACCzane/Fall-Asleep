using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Tank : MonoBehaviour, IInteractable
{

    private HoldObj holdObj;            //获取玩家手上物体

    private bool isBroken = true;
    public bool IsBroken{
        get{
            return isBroken;
        }
        private set{
            isBroken = value;
        }
    }
    [SerializeField] private SpriteRenderer tankSpr;
    [SerializeField] private Sprite tankBrokenSp;
    [SerializeField] private Sprite tankNotBrokenSp;

    [SerializeField] private GameObject lightSrc;

    public void Interact()
    {
        if(isBroken && holdObj.holdingObj is Battery){
            FixTank();
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player"){
            other.GetComponent<PlayerControl>().currentInteractableTarget = this;
            holdObj = other.GetComponent<HoldObj>();
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Player"){
            if(other.GetComponent<PlayerControl>().currentInteractableTarget == this){
                other.GetComponent<PlayerControl>().currentInteractableTarget = null;
                holdObj = null;                     //防止非法操作
            } 
        }
    }

    public void FixTank(){
        
        EventHandler.CallUseItemInHand();   //将手上的物品销毁
        //修好电箱
        tankSpr.sprite = tankNotBrokenSp;
        tankSpr.color = Color.green;        //先用颜色代替
        isBroken = false;

        lightSrc.SetActive(true);
    }

    public void BreakTank(){
        tankSpr.sprite = tankBrokenSp;
        tankSpr.color = Color.white;        //先用颜色代替
        isBroken = true;

        lightSrc.SetActive(false);
    }
}
