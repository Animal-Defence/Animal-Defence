using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class UnlockAnimalView : MonoBehaviour
{
    public GameObject unlockAnimalView;

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            unlockAnimalView.SetActive(false);
        }
    }

}
