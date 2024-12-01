using System.Collections;
using UnityEngine;

public abstract class PicakableObjectBase : MonoBehaviour, IPickable
{
    [SerializeField] protected Collider2D coll;
    [SerializeField] protected SpriteRenderer sp;

    public void Drop(HoldObj playerHold)
    {
        playerHold.holdingObj = null;
        SetTransformParent(null);
        StartCoroutine(DropItem());
    }

    public void Pick(HoldObj playerHold)
    {
        if(!playerHold.canHold){return;}
        
        playerHold.holdingObj = this;
        coll.enabled = false;
        SetTransformParent(playerHold.holdAnchor);
        transform.localPosition = Vector3.zero;
    }

    public void SetTransformParent(Transform other){ 
        transform.parent = other;
    }

    protected void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player"){
            HoldObj holdObj = other.GetComponent<HoldObj>();
            // Debug.Log("检测到玩家");
            if(holdObj.currentTargetObj == null)
                holdObj.currentTargetObj = this;
        }
    }

    protected void OnTriggerExit2D(Collider2D other) {
        if(other.tag == "Player"){
            HoldObj holdObj = other.GetComponent<HoldObj>();
            if(holdObj.currentTargetObj != this as IInteractable)
                holdObj.currentTargetObj = null;
        }
    }

    protected IEnumerator DropItem(){
        
        //由于扔出物体不能马上捡回来，用协程

        float timeDuration = .2f;
        float elapsedTime = 0;
        Vector2 targetDropPos = new Vector2(transform.position.x, transform.position.y-0.2f);
        while(elapsedTime < timeDuration){
            elapsedTime+=Time.deltaTime;
            transform.position = Vector2.Lerp(transform.position, targetDropPos, elapsedTime/timeDuration);
            yield return null;
        }
        transform.position = targetDropPos;
        coll.enabled = true;
    }

}
