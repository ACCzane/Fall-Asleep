using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FinalTank : MonoBehaviour, IInteractable
{

    private PlayerStat playerStat;

    [SerializeField] private Light2D light;
    [SerializeField] private Exit exit;

    public void Interact()
    {
        if(playerStat!=null){
            if(playerStat.currentCoin == playerStat.MaxCoinValue){
                Debug.Log("GOOOOOOO");
                StartCoroutine(Success());
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player"){
            other.GetComponent<PlayerControl>().currentInteractableTarget = this;
            playerStat = other.GetComponent<PlayerStat>();
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag != "Player"){return;}
        if(other.GetComponent<PlayerControl>().currentInteractableTarget == this){
            other.GetComponent<PlayerControl>().currentInteractableTarget = null;
            playerStat = null;
        } 
    }

    private IEnumerator Success(){
        light.color = Color.green;

        yield return new WaitForSeconds(1);

        exit.EnterNextLevel();
    }

}
