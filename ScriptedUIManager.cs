using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScriptedUIManager : MonoBehaviour
{
    public ScoreController _scoreController;
    
    public TextMeshProUGUI scoreText;
    public GameObject mainMenu;
    public GameObject gameOverMenu;
    public GameObject waitMenu;
    public GameObject pauseMenu;
    public GameObject inGameMenu;

    public List<GameObject> allMenues = new();
    
    

    public void InitializeSelf()
    {
        allMenues.Add(mainMenu);
        allMenues.Add(gameOverMenu);
        allMenues.Add(waitMenu);
        allMenues.Add(pauseMenu);
        allMenues.Add(inGameMenu);
    }


    public void Update()
    {
        UpdateScore();
    }


    void UpdateScore()
    {
        scoreText.text = _scoreController.score.ToString();
    }
    
    
    public void ShowHideMenu(int number)
    {
        switch (number)
        {
         case   1: HideAll();
             waitMenu.SetActive(true);
             ResetScore();
             break;
         
         case   2: HideAll();
             inGameMenu.SetActive(true);
             break;
         
         case 3: HideAll();
             inGameMenu.SetActive(true);
             pauseMenu.SetActive(true);
             break;
         
         case 4: HideAll();
             inGameMenu.SetActive(true);
             break;
         
         case 6: HideAll();
             inGameMenu.SetActive(true);
             gameOverMenu.SetActive(true);
             break;
         
         case 7: HideAll();
             mainMenu.SetActive(true);
             ResetScore();
             break;
        }
    }

    void HideAll()
    {
        foreach (var menu in allMenues)
        {
            menu.SetActive(false);
        }
    }

    void ResetScore()
    {
        _scoreController.score = 0;
    }
}