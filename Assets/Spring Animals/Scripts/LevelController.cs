using UnityEngine;

public class LevelController : MonoBehaviour {

    public static LevelController instance;

    public GameObject playerObj;         //ref to player gameobject prefab
    public float jumpForce;              //jump force of player
    public float maxJumpForce;           //max jump force of player
    [HideInInspector]
    public bool gameStarted = false;     //to check if game is started or not

    private float chargePower = 0;       //power with which the player jumps
    private bool isCharging = false;     //check if charging is true or not
    private float minPlatfromGap = 1.5f; //min gap between 2 platforms
    private float maxPlatformGap = 2.5f; //max gap between 2 platforms
    private Rigidbody2D playerBody;      //ref to rigidbody of player

    private int platformsSpawned = 0;    //this is to keep track number of platforms spawned

    public float ChargePower             //getter for chargepower variable
    {
        get { return chargePower; }
    }

    void Awake()
    {
        if (instance == null)
            instance = this;
    }

	// Use this for initialization
	void Start ()
    {
        GameManager.instance.isGameOver = false;            //we set gameover false at start of game
        GameObject player = Instantiate(playerObj);         //we spawn the player obj
        PositionPlatform(-2f);                              //we spawn the platforms ,starting from -2.5f x axis
        playerBody = player.GetComponent<Rigidbody2D>();    //we get the player rigidbody
	}
    //this method spawn and position the platforms
    void PositionPlatform(float posX)
    {
        if (posX < 10f)                                                                    //we check if the posX is less than 10
        {   
            GameObject platform = ObjectPooling.instance.GetPlatform();                    //if yes we get the platform obj from objectpooling
            platform.transform.position = new Vector2(posX, -1.5f - Random.value * 2.5f);  //we then position the platfrom
            posX += minPlatfromGap + Random.value * (maxPlatformGap - minPlatfromGap);     //the reset the posX value
            platform.SetActive(true);                                                      //and make the platform active in scene

            int r = Random.Range(0, 6);                                                    //we get a random number between 0-6
                                                                                           //this is to decide the coin spawn
            if (r == 2)                                                                    //if r is 2 we spawn coins
                platform.GetComponent<PlatformScript>().Reset(platformsSpawned, true);
            else                                                                           //if r is not 2 we dont spawn coins
                platform.GetComponent<PlatformScript>().Reset(platformsSpawned, false);

            platformsSpawned++;                                                            //we increase platform count by 1
            PositionPlatform(posX);                                                        //and again we call this methode
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (!gameStarted || GameManager.instance.isGameOver)                               //we check if game is over of game is not started , then return
            return;

        if (isCharging) chargePower = Mathf.Min(chargePower + Time.deltaTime, 1f);         //we check for charging and then set the chargepower value

        else if (!isCharging) chargePower = 0;                                             //if charging is false we set the chargepower value to zero

        PlayerController.instance.Compress(chargePower);                                   //we set compression value for player depending on chargepower value

        if (Input.GetMouseButtonDown(0) && playerBody.velocity.y == 0 && !isCharging)      //if mouse is clicked , player Y velocity is zero and charging is false
        {
            isCharging = true;                                                             //we set charging to true
            GameObject[] platforms = GameObject.FindGameObjectsWithTag("Platform");        //get all the platforms in scene and store it in an Array
            float maxPlatformDistance = 0;                                                 //set max distance to 0
            foreach (GameObject platform in platforms)                                     //the we create foreach loop
            {
                maxPlatformDistance = Mathf.Max(maxPlatformDistance, platform.transform.position.x);    //and set there max distance
            }
            if (maxPlatformDistance < 10f)                                                 //if max distance is less than 10
                PositionPlatform(maxPlatformDistance + minPlatfromGap + Random.value * (maxPlatformGap - minPlatfromGap)); //we position the platform
        }

        if (Input.GetMouseButtonUp(0) && playerBody.velocity.y == 0 && isCharging)        //if mouse is not clicked , player Y velocity is zero and charging is true
        {           
            PlayerController.instance.Jump(maxJumpForce * chargePower + 50);              //we make player jump
            isCharging = false;                                                           //we set charging to false
        }


	}
}
