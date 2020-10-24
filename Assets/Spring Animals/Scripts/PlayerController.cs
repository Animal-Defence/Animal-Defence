using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour {

    public static PlayerController instance;

    public AudioClip jump, coinCollect, dead, land;    //ref to sound effects

    [SerializeField] private GameObject spriteHolder;  //ref to sprite renderer gameobject
    private AudioSource audioS;                        //ref to audiosource component

    void Awake()
    {
        if (instance == null)
            instance = this;
    }

	// Use this for initialization
	void Start ()
    {
        audioS = GetComponent<AudioSource>();         //we get ref to audiosource component attached to gameobject
        transform.position = new Vector2(-2f, 2f);    //we set the tranform of player
        ChangeSprite();                               //method to change the player sprite
    }

    public void ChangeSprite()
    {   //depending on selected player we change its sprite by refering to Shop script
        spriteHolder.GetComponent<SpriteRenderer>().sprite = ShopManager.instance.shopItems[GameManager.instance.selectedSkin].characterSprite;
    }
	
	// Update is called once per frame
	void Update ()
    {
        transform.position = new Vector2(-2f, transform.position.y);  //make the player to reset its x value
    }

    public void Jump(float jumpForce)                                           //method which make player to jump
    {
        audioS.PlayOneShot(jump);                                               //we play jump sound
        GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce));       // adding a vertical force to make the player jump
        GameObject[] platforms = GameObject.FindGameObjectsWithTag("Platform"); // get all objects tagged with "Platform"
        foreach (GameObject platform in platforms)
        {
            // send them all "Move" message
            platform.SendMessage("Move");
        }
    }
                                                                                        
    public void Compress(float ratio)                                                               //method which make player sprite to compress
    {
        float compressRatio = 1 - ratio;                                                            //we get the compression value
        spriteHolder.transform.localScale = new Vector3(1, Mathf.Clamp(compressRatio, 0.5f, 1), 1); //and set the x value which is clamped between 0.5 and 1
    }

    void OnTriggerEnter2D(Collider2D other)                                                         //method called when any object enter the player collider
    {
        if (other.CompareTag("Platform"))                                                           //check if the tag is "Platform"
        {
            PlatformScript script = other.GetComponent<PlatformScript>();                           //the we get the script component attach to it

            if (script.Scored == false)                                                             //check if the scored is false
            {
                audioS.PlayOneShot(land);                                                           //then we play land sound
                GameManager.instance.currentScore += (script.PlatformIndex - GameManager.instance.currentScore); //increase the score
                UIManager.instance.inGameScore.text = "" + GameManager.instance.currentScore;       //update the score text
                script.Scored = true;                                                               //set scored value to true
            }
        }

        if (other.CompareTag("Coin"))                                                               //check if the tag is "Coin"
        {
            audioS.PlayOneShot(coinCollect);                                                        //then we play coin colect sound
            other.gameObject.SetActive(false);
            GameManager.instance.currentCoins++;                                                    //increase the current coin value
            GameManager.instance.coins++;                                                           //increase the total coins value
            UIManager.instance.inGameCoinText.text = "" + GameManager.instance.currentCoins;        //set the coin text value
            GameManager.instance.Save();                                                            //save the data
        }

        if (other.CompareTag("Enemy"))                                                              //check if the tag is "Enemy"
        {
            audioS.PlayOneShot(dead);                                                               //then we play death sound

            GameManager.instance.isGameOver = true;                                                 //set gameover to true
            UIManager.instance.GameOver();                                                          //call gameover method of UImanager
            StopPlatforms();                                                                        //stop movements of platfroms
            Destroy(gameObject, 1f);                                                                //we destroy the player after 1 sec
        }   
    }

    void OnCollisionEnter2D()                                                                       //if player collides with any object we the stop the platfomrs
    {
        StopPlatforms();
    }

    public void StopPlatforms()
    {                                                                                                   
        GameObject[] platforms = GameObject.FindGameObjectsWithTag("Platform");                     // get all objects tagged with "Platform"
        foreach (GameObject platform in platforms)
        {
            // send them all "Move" message
            platform.SendMessage("Stop");
        }
    }
}
