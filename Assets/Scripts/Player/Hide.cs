using UnityEngine;

public class Hide : MonoBehaviour
{

    [Header("引用")]
    [SerializeField] private SpriteRenderer playerBodySpriteRenderer;
    [SerializeField] private SpriteRenderer hideButtonRenderer;

    [SerializeField] private Sprite hideButtonSprite;
    // [SerializeField] private Sprite hidingSprite;

    private Vector2 hideInPos;
    private Vector2 getOutPos;

    public bool IsHiding{get; private set;}

    private void OnEnable() {
        EventHandler.Hide += OnHide;
    }

    private void OnDisable() {
        EventHandler.Hide -= OnHide;
    }

    private void OnHide()
    {
        if(IsHiding){
            GetOut();
        }else{
            HideIn();
        }
    }

    public void HideIn(){
        transform.position = hideInPos;

        playerBodySpriteRenderer.enabled = false;

        IsHiding = true;
        // hideButtonRenderer.sprite = hidingSprite;
        hideButtonRenderer.sprite = null;
    }

    public void GetOut(){
        transform.position = getOutPos;

        playerBodySpriteRenderer.enabled = true;
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
}
