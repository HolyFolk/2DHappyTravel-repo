using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuntimeUI : MonoBehaviour
{
    public GameObject settingMenu;
    public GameObject buttons;
    // Start is called before the first frame update
    void Awake()
    {

    }

    public void SettingMenuON(bool isActive)
    {
        settingMenu.SetActive(isActive);
    }

    public void SetButton(bool isActive)
    {
        buttons.SetActive(isActive);
    }


    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
    }
}
