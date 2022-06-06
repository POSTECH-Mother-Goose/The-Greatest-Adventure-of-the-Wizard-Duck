using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedWater : MonoBehaviour
{
    public float speedX = 0.1f;
    public float speedY = 0.1f;
    private float curX;
    private float curY;

    // Start is called before the first frame update
    void Start()
    {
        // curX = GetComponent<MeshRenderer>().material.mainTextureOffset.x;
        curX = 0;
        curY = GetComponent<MeshRenderer>().material.mainTextureOffset.y;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // curX += Time.deltaTime * speedX;
        curY -= Time.deltaTime * speedY;
        gameObject.GetComponent<Renderer>().material.mainTextureOffset = new Vector2(curX, curY);
        gameObject.GetComponent<MeshRenderer>().material.mainTextureOffset = new Vector2(curX, curY);
        gameObject.GetComponent<MeshRenderer>().material.SetTextureOffset("_MainTex", new Vector2(curX, curY));
    }
}
