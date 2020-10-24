using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPooling : MonoBehaviour
{
    public static ObjectPooling instance;

    public GameObject platform;                                     //get refrenace to prefab
        
    public int poolAmount;                                          //amount of obj to spawn at start

    List<GameObject> Platform;                                      //we create a list

    void Awake()
    {
        MakeInstance();

        Platform = new List<GameObject>();                          //make list empty
        for (int i = 0; i < poolAmount; i++)                        //the we crate the number of objects we want
        {
            GameObject obj = (GameObject)Instantiate(platform);     //spawn the objects
            obj.transform.parent = transform;                       //set there parent
            Platform.Add(obj);                                      //add them to list
            obj.SetActive(false);                                   //deactivate them
        }
    }

    void MakeInstance()
    {
        if (instance == null)
            instance = this;
    }

    public GameObject GetPlatform()
    {                                                              //loop throught the list
        for (int i = 0; i < Platform.Count; i++)
        {                                                          //check is any1 is not active in scene
            if (!Platform[i].activeInHierarchy)
            {                                                      //return that object
                return Platform[i];
            }
        }
        
        GameObject obj = (GameObject)Instantiate(platform);       //if all are active , we create new
        obj.transform.parent = transform;                         //set there parent
        Platform.Add(obj);                                        //add them to list
        obj.SetActive(false);                                     //deactivate them
        return obj;

    }

}
