using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ShopItems              //custom method for saving data
{
    public string characterName;    //name of character
    public int    characterCost;    //cost of character
    public Sprite characterSprite;  //sprite of character
    [HideInInspector]
    public bool  unLocked;          //check if its unlocked or not
}

public class ShopManager : MonoBehaviour {

    public static ShopManager instance;

    public GameObject  shopPanel, lockObj;          //ref to shopPanel and lock image
    public Image       characterImage;              //ref to the Image gameobject which shows the character image
    public Text        characterName, coinText;     //text object of name and cost
    public Text        selectBtnText;               //text object of select btn
    public ShopItems[] shopItems;                   //array of shop items

    private int currentCharcter = 0;                //to check which shop item to show

    void Awake()
    {
        if (instance == null)
            instance = this;
    }

	// Use this for initialization
	void Start ()
    {
        coinText.text = "" + GameManager.instance.coins;                        //update the coin text
    }

    public void OpenShop()                                                      //called when shop is opened
    {
        UIManager.instance.ClickSound();
        for (int i = 0; i < GameManager.instance.skinUnlocked.Length; i++)      //loop throungh all the shop item
        {
            shopItems[i].unLocked = GameManager.instance.skinUnlocked[i];       //set there unlock value with respective to the store value in GameManager data
        }
        currentCharcter = GameManager.instance.selectedSkin;                    //ge the selected character and store that value to currentCharacter
        BtnCode();                                                              //update the select btn

        shopPanel.SetActive(true);                                              //set the shop panel active
    }

    public void CloseShop()                                                     //called when shop is opened
    {
        UIManager.instance.ClickSound();
        shopPanel.SetActive(false);                                             //set the shop panel deactive
    }

    public void NextBtn()                                                       //called when next btn is clicked
    {
        UIManager.instance.ClickSound();
        if (currentCharcter < shopItems.Length - 1)                             //we check if the current character is 1 less than total items
        {
            currentCharcter++;                                                  //we increase the current character index
            BtnCode();                                                          //update the select btn
        }
    }

    public void PreviousBtn()                                                   //called when previous btn is clicked
    {
        UIManager.instance.ClickSound();
        if (currentCharcter > 0)                                                //we check if the current character is greater than 0
        {
            currentCharcter--;                                                  //we decrease the current character index
            BtnCode();                                                          //update the select btn
        }
    }

    void BtnCode()
    {
        characterImage.sprite = shopItems[currentCharcter].characterSprite;                                   //set the character image 
        characterName.text = shopItems[currentCharcter].characterName;                                        //set the character name

        if (shopItems[currentCharcter].unLocked && currentCharcter != GameManager.instance.selectedSkin)      //if character is unlocked and its not selected
        {
            lockObj.SetActive(false);                                                                         //deactivate lock gameobject
            selectBtnText.text = "Select";                                                                    //set the btn text to select
        }
        
        else if (shopItems[currentCharcter].unLocked && currentCharcter == GameManager.instance.selectedSkin) //if character is unlocked and its  selected
        {
            lockObj.SetActive(false);                                                                         //deactivate lock gameobject
            selectBtnText.text = "Selected";                                                                  //set the btn text to selected
        }
        
        else if (!shopItems[currentCharcter].unLocked)                                                        //if character is locked
        {
            lockObj.SetActive(true);                                                                          //activate lock gameobject
            selectBtnText.text = shopItems[currentCharcter].characterCost + " Coins";                         //set the btn text to cost
        }
    }

    public void SelectBtn()                                                                                   //called when select btn is clicked
    {
        UIManager.instance.ClickSound();
        if (shopItems[currentCharcter].unLocked == false)                                                     //if the current character is not unlocked
        {
            if (GameManager.instance.coins >= shopItems[currentCharcter].characterCost)                       //we check if we have enought coins to buy
            {   
                GameManager.instance.coins -= shopItems[currentCharcter].characterCost;                       //we then reduce the total coins by the cost of character
                shopItems[currentCharcter].unLocked = true;                                                   //unlcok it in shopItems
                GameManager.instance.skinUnlocked[currentCharcter] = true;                                    //unlcok it in GameManager
                GameManager.instance.selectedSkin = currentCharcter;                                          //set it as selected character
                GameManager.instance.Save();                                                                  //Save the data on device
                
                lockObj.SetActive(false);                                                                     //deactivate lock object
                selectBtnText.text = "Selected";                                                              //set select btn text to Selected
                coinText.text = "" + GameManager.instance.coins;                                              //update conis text
                PlayerController.instance.ChangeSprite();                                                     //and change the player sprite
            }
            else if (GameManager.instance.coins < shopItems[currentCharcter].characterCost)                   //if we dont have enough money
                Debug.Log("Require More Coins");                                                              //you can open the IAP menu here
        }
        else if (shopItems[currentCharcter].unLocked == true)                                                 //the character is unlocked
        {   
            if (currentCharcter == GameManager.instance.selectedSkin)                                         //if the current charater is equal to selected skin
                return;                                                                                       //do nothing
            
            else if (currentCharcter != GameManager.instance.selectedSkin)                                    //if the current charater is not equal to selected skin
            {   
                GameManager.instance.selectedSkin = currentCharcter;                                          //select the character
                GameManager.instance.Save();                                                                  //save it
                selectBtnText.text = "Selected";                                                              //change the text
                PlayerController.instance.ChangeSprite();
            }
        }
    }

}
