using UnityEngine;

public class Grave : MonoBehaviour, IInteractable
{

    private HoldObj holdObj;            //玩家手上拿的物体

    [SerializeField] private SpriteRenderer graveSpr;
    [SerializeField] private EnemySpawner enemySpawner;
    private bool canInteract = true;

    private void OnEnable() {
        // EventHandler.GiveFlower += OnGiveFlower;
    }

    private void OnDisable() {
        // EventHandler.GiveFlower -= OnGiveFlower;
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
            if(other.GetComponent<PlayerControl>().currentInteractableTarget == this)
            {
                other.GetComponent<PlayerControl>().currentInteractableTarget = null;
                holdObj = null;
            }
        }
    }

    // private void OnGiveFlower()
    // {
        
    // }

    public void Interact()
    {

        if(canInteract && holdObj.holdingObj is Flower ){
            EventHandler.CallUseItemInHand();   //将手上的物品销毁
            //墓碑变颜色，表示无法再交互
            graveSpr.color = Color.red;

            canInteract = false;

            //墓碑无法再生成幽灵
            enemySpawner.canGenEnemy = false;
        }
        
    }
}
