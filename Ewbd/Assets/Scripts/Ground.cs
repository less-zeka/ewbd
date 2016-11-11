using UnityEngine;

public class Ground : MonoBehaviour
{
    void Start()
    {
        var scaleX = 10.050f;
        var scaleY = 10.050f;
        transform.GetComponent<Renderer>().material.mainTextureScale = new Vector2(scaleX, scaleY);
    }
}