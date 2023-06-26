using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MainTitleScene : MonoBehaviour

{
    public GameObject settingMenu;
    public GameObject buttons;
 
    public void NextScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void OptionON(bool isActive)
    {
        settingMenu.SetActive(isActive);
    }

    public void SetButtons(bool isActive)
    {
        buttons.SetActive(isActive);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
