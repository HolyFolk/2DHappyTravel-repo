using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class RuntimeUI : MonoBehaviour
{
    public GameObject settingMenu;
    public GameObject buttons;
    private GameObject[] players;
    private PlayerStatHandler[] playerStatHandlers;
    public Text goText;
    // Start is called before the first frame update
    void Start()
    {
        players= GameObject.FindGameObjectsWithTag("Player");
        Time.timeScale = 1;
    }

    private void LateUpdate()
    {
      int GO_Count= 0;
      for(int i=0; i<players.Length; i++)
        {
            if (players[i].GetComponent<PlayerStatHandler>().isGameOver()==true)
            {
                GO_Count++;
            }
        }
      if (GO_Count==players.Length) 
      {
            SetGameOver();
      }
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

    public void SetGameOver()
    {
        Time.timeScale = 0.1f;
        goText.gameObject.SetActive(true);
    }
}
