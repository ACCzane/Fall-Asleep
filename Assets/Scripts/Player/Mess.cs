using UnityEngine;

public class Mess : MonoBehaviour
{

    [Header("引用")]
    [SerializeField] private SpriteRenderer messButtonRenderer;
    [SerializeField] private Sprite messButton;


    private Vector2 interactPos;
    private Vector2 outPos;


    private void OnEnable() {
        EventHandler.Mess += OnMess;
    }

    private void OnDisable() {
        EventHandler.Mess -= OnMess;
    }

    private void OnMess()
    {
        throw new System.NotImplementedException();
    }

    public void SetPos(Vector2 inPos, Vector2 outPos){
        interactPos = inPos;
        this.outPos = outPos;
    }
}
