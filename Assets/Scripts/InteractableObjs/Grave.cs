using UnityEngine;

public class Grave : MonoBehaviour, IInteractable
{

#region 白天
    private bool isNight = false;

    private HoldObj holdObj;            //玩家手上拿的物体


    [SerializeField] private Sprite normalGrave;
    [SerializeField] private Sprite graveWithFlower;

    [SerializeField] private SpriteRenderer graveSpr;
    [SerializeField] private EnemySpawner enemySpawner;
    private bool canInteract = true;
#endregion

#region 夜间
    // private Collider2D collider2D;

    [Header("参数")]
    [SerializeField] private Vector2 inPos;
    [SerializeField] private Vector2 outPos;

    [SerializeField] private ParticleSystem particleSystem;

    // private bool canInteract = true;
    private bool canCollectCoin = false;


    private void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere(new Vector2(transform.position.x, transform.position.y) + inPos, 0.1f);
        Gizmos.DrawWireSphere(new Vector2(transform.position.x, transform.position.y) + outPos, 0.1f);
    }
#endregion

    private void OnEnable() {
        EventHandler.NightFall += OnNightFall;
    }

    private void OnDisable() {
        EventHandler.NightFall -= OnNightFall;
    }

    private void OnNightFall()
    {
        // throw new System.NotImplementedException();
        isNight = true;
        if(canInteract == false){
            //如果canInteract为否，表示白天放花了
            //晚上可以交互
            canInteract = true;
            canCollectCoin = true;
        }else{
            //否则晚上不可以交互
            canInteract = false;
            canCollectCoin = false;
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player"){
            if(!isNight){
                other.GetComponent<PlayerControl>().currentInteractableTarget = this;
                holdObj = other.GetComponent<HoldObj>();
            }
            else{
                //玩家可以按键进入
                other.GetComponent<Hide>().TurnHintButton(true);
                // other.GetComponent<PlayerControl>().currentActivatedInteractType = InteractType.Hide;
                other.GetComponent<PlayerControl>().currentInteractableTarget = this;

                //玩家注册进入位置和退出位置
                other.GetComponent<Hide>().SetPos(
                    new Vector2(transform.position.x, transform.position.y) + inPos,
                    new Vector2(transform.position.x, transform.position.y) + outPos
                );
            }
        }

    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Player"){
            if(!isNight){
                if(other.GetComponent<PlayerControl>().currentInteractableTarget == this)
                {
                    other.GetComponent<PlayerControl>().currentInteractableTarget = null;
                    holdObj = null;
                }
            }
            else{
                other.GetComponent<Hide>().TurnHintButton(false);
                // other.GetComponent<PlayerControl>().currentActivatedInteractType = InteractType.Null;
                other.GetComponent<PlayerControl>().currentInteractableTarget = null;
            }
            
        }
    }

    public void Interact()
    {

        if(!isNight){
            if(canInteract && holdObj.holdingObj is Flower ){
                EventHandler.CallUseItemInHand();   //将手上的物品销毁
                //表示无法再交互
                graveSpr.sprite = graveWithFlower;

                canInteract = false;

                //墓碑无法再生成幽灵
                enemySpawner.canGenEnemy = false;
            }
        }
        else
        {
            if(canInteract){
                EventHandler.CallHide();
            }
            if(canCollectCoin){
                particleSystem.Emit(30);
                graveSpr.color = Color.yellow;
                EventHandler.CallGainCoin();
                canCollectCoin = false;
            }
        }
    }



}
