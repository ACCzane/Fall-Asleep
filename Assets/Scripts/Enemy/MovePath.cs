using System.Collections.Generic;
using UnityEngine;

public class MovePath : MonoBehaviour
{
    [SerializeField] private List<Vector2> posNodes = new List<Vector2>();

    private void OnDrawGizmosSelected() {
        foreach (var pos in posNodes)
        {
            Gizmos.DrawWireSphere(pos, 0.5f);
        }
    }

    public List<Vector2> GetPos(){
        List<Vector2> posNodesToPass = new List<Vector2>(posNodes);
        return posNodesToPass;
    }
}
