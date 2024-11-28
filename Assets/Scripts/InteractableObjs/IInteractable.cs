using UnityEngine;

public interface IInteractable
{
    public void OnTriggerEnter2D(Collider2D other);
    public void OnTriggerExit2D(Collider2D other);

    public void Interact(){
        
    }
}
