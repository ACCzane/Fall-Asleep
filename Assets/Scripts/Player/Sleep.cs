using UnityEngine;

public class Sleep : MonoBehaviour
{
    [Header("引用")]
    [SerializeField] private SpriteRenderer playerBodySpriteRenderer;
    [SerializeField] private SpriteRenderer sleepButtonRenderer;

    [SerializeField] private Sprite sleepButtonSprite;
    // [SerializeField] private Sprite hidingSprite;

    private Vector2 sleepInPos;
    private Vector2 getOutPos;

    // public bool IsSleeping{get; private set;}

    private void OnEnable() {
        EventHandler.Sleep += Onsleep;
    }

    private void OnDisable() {
        EventHandler.Sleep -= Onsleep;
    }

    private void Onsleep()
    {
        // if(IsSleeping){
        //     GetOut();
        // }else{
        //     sleepIn();
        // }
        sleepIn();
    }

    public void sleepIn(){
        transform.position = sleepInPos;

        // playerBodySpriteRenderer.enabled = false;

        //通知GameManager进入黑夜
        EventHandler.CallNightFall();

        // IsSleeping = true;
        // sleepButtonRenderer.sprite = hidingSprite;
        sleepButtonRenderer.sprite = null;
    }

    public void GetOut(){
        transform.position = getOutPos;

        playerBodySpriteRenderer.enabled = true;
        // IsSleeping = false;

        sleepButtonRenderer.sprite = sleepButtonSprite;
    }

    public void SetPos(Vector2 inPos, Vector2 outPos){
        sleepInPos = inPos;
        getOutPos = outPos;
    }

    public void TurnHintButton(bool open){
        if(open){
            sleepButtonRenderer.sprite = sleepButtonSprite;
            sleepButtonRenderer.enabled = true;
        }else{
            sleepButtonRenderer.enabled = false;
        }
    }
}
