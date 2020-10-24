using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
   public static GameManager instance;
   
   public GameObject titleUI;
   public GameObject gameUI;
   public GameObject winUI;
   public GameObject loseUI;

   public TextMeshProUGUI pelletsText;
   public TextMeshProUGUI scoreText;

   public int score;

   public void Awake()
   {
      instance = this;
   }
   
   public void Reset()
   {
      SwitchUI(titleUI);
      score = 0;
   }

   public void InGame()
   {
      SwitchUI(gameUI);
   }

   public void Win()
   {
      SwitchUI(winUI);
   }

   public void Lose()
   {
      SwitchUI(loseUI);
   }

   public void SwitchUI(GameObject newUI)
   {
      titleUI.SetActive(false);
      gameUI.SetActive(false);
      winUI.SetActive(false);
      loseUI.SetActive(false);
      
      newUI.SetActive(true);
   }

   public void AddPoints(int points)
   {
      score += points;
      scoreText.text = "Score: " + score;
   }
   
}
