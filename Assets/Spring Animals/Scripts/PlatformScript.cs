using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlatformScript : MonoBehaviour {

    private GameObject mainCamera;                                               //ref to main camera

    private bool scored       = false;                                           //this is use to increse score
    private int platformIndex = 0;                                               //this stores the current platform spawned index

    public bool Scored       { get { return scored; }  set { scored = value; } } //score geter and setter
    public int PlatformIndex { get { return platformIndex; } }                   //Platfrom indes getter
    public GameObject coinObj;                                                   //ref to child coin gameobject

    // Use this for initialization
    void Start ()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");	         //we get refrence to main camera in the scene
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (GameManager.instance.isGameOver) return;                             //if gameover is true we return

        if ((transform.position.x - mainCamera.transform.position.x) <= -5)      //if the position is -5 of less from main camera
            gameObject.SetActive(false);                                         //we deactivate the platform
	}
                
    public void Reset(int index , bool activeVal)                                //call when the platfrom is activate by LevelController
    {
        scored = false;                                                          //reset score value
        platformIndex = index;                                                   //set the plaform index
        coinObj.SetActive(activeVal);                                            //we activate or deactivate the coin
    }

    void Move()                                                                  // assigning platform an horizontal speed
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(-3f, 0f);
    }

    void Stop()                                                                  // assigning platform an horizontal speed
    {    
        GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
    }

}
