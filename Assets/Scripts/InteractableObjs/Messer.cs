using UnityEngine;

public class Messer : MonoBehaviour
{
    private Collider2D collider2D;

    [Header("参数")]
    [SerializeField] private Vector2 inPos;
    [SerializeField] private Vector2 outPos;

    private void Awake() {
        collider2D = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player"){
            //玩家可以按键交互
            other.GetComponent<PlayerControl>().canPressQ = true;

        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.tag == "Player"){
            other.GetComponent<PlayerControl>().canPressQ = false;
        }
    }

    private void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere(new Vector2(transform.position.x, transform.position.y) + inPos, 0.1f);
        Gizmos.DrawWireSphere(new Vector2(transform.position.x, transform.position.y) + outPos, 0.1f);
    }
}
