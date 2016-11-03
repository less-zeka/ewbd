using UnityEngine;

public class Ground : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        //TODO?!
        var scaleX = 10.050f;
        var scaleY = 10.050f;
        transform.GetComponent<Renderer>().material.mainTextureScale = new Vector2(scaleX, scaleY);
    }
}