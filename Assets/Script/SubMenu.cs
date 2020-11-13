using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubMenu : MonoBehaviour
{
    public GameObject SubMenuView;

    public void onClickStartBtn()
    {
        SubMenuView.SetActive(false);
        Time.timeScale = 1.0f;
    }
}
