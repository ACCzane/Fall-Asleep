using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    [SerializeField] private AudioClip pick;
    [SerializeField] private AudioClip drop;
    // [SerializeField] private AudioClip hide;

    [SerializeField] private AudioSource audioSource;

    public void PlayPick(){
        audioSource.PlayOneShot(pick);
    }

    public void PlayDrop(){
        audioSource.PlayOneShot(drop);
    }

    // public void PlayHide(){
    //     audioSource.PlayOneShot(hide);
    // }
}
