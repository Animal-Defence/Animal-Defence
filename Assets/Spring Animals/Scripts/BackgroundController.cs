using UnityEngine;

public class BackgroundController : MonoBehaviour {

    public static BackgroundController instance;

    public float waterSpeed, cloudsSpeed;        //ref to the speed
    public Renderer waterTexture, cloudsTexture; //ref to the renderer

    void Awake()
    {
        if (instance == null) instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        ScrollWater(waterSpeed, waterTexture);      //setting offeset of renderer
        ScrollClouds(cloudsSpeed, cloudsTexture);   //setting offeset of renderer
    }

    void ScrollWater(float scrollSpeed, Renderer rend)
    {
        float offset = Time.time * scrollSpeed;                              //we creat a variable for offset
        rend.material.SetTextureOffset("_MainTex", new Vector2(offset, 0));  //assign the offset value
    }

    void ScrollClouds(float scrollSpeed, Renderer rend)
    {
        float offset = Time.time * scrollSpeed;
        rend.material.SetTextureOffset("_MainTex", new Vector2(offset, 0));
    }

}
