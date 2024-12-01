using UnityEngine;

public class Hide : MonoBehaviour
{

    [Header("引用")]
    [SerializeField] private SpriteRenderer playerBodySpriteRenderer;
    [SerializeField] private Transform playerHoldingAnchor;
    private SpriteRenderer playerHoldingSpriteRenderer;
    [SerializeField] private SpriteRenderer hideButtonRenderer;

    [SerializeField] private Sprite hideButtonSprite;
    // [SerializeField] private Sprite hidingSprite;

    private Vector2 hideInPos;
    private Vector2 getOutPos;

    public bool IsHiding{get; private set;}

    private void OnEnable() {
        EventHandler.Hide += OnHide;
        EventHandler.NightFall += OnNightFall;
    }

    private void OnDisable() {
        EventHandler.Hide -= OnHide;
        EventHandler.NightFall -= OnNightFall;
    }

    private void OnNightFall()
    {
        //如果躲在柜子里等天黑，出来，不然有Bug，玩家不能移动
        IsHiding = false;
    }

    private void OnHide()
    {

        //先读一下玩家有没有拿物品
        playerHoldingSpriteRenderer = playerHoldingAnchor.GetComponentInChildren<SpriteRenderer>();


        if(IsHiding){
            GetOut();
        }else{
            HideIn();
        }
    }

    public void HideIn(){
        transform.position = hideInPos;

        playerBodySpriteRenderer.enabled = false;
        if(playerHoldingSpriteRenderer!=null)
            playerHoldingSpriteRenderer.enabled = false;

        IsHiding = true;
        // hideButtonRenderer.sprite = hidingSprite;
        hideButtonRenderer.sprite = null;
    }

    public void GetOut(){
        transform.position = getOutPos;

        playerBodySpriteRenderer.enabled = true;
        if(playerHoldingSpriteRenderer!=null)
            playerHoldingSpriteRenderer.enabled = true;
            
        IsHiding = false;

        hideButtonRenderer.sprite = hideButtonSprite;
    }

    public void SetPos(Vector2 inPos, Vector2 outPos){
        hideInPos = inPos;
        getOutPos = outPos;
    }

    public void TurnHintButton(bool open){
        if(open){
            hideButtonRenderer.sprite = hideButtonSprite;
            hideButtonRenderer.enabled = true;
        }else{
            hideButtonRenderer.enabled = false;
        }
    }

    public void DisablePlayerSpr(){
        playerBodySpriteRenderer.enabled = false;
    }

    public void EnablePlayerSpr(){
        playerBodySpriteRenderer.enabled = true;
    }
}
