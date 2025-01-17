using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightManager : MonoBehaviour
{
    [SerializeField] private Light2D globalLight;

    public void TurnOnGlobalLight(){
        globalLight.intensity = 1f;
    }

    public void TurnOffGlobalLight(){
        globalLight.intensity = 0f;
    }
}
