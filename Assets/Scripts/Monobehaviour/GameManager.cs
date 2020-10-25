using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Monobehaviour;
using TMPro;
using Unity.Entities;
using Unity.Scenes;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
   public static GameManager instance;

   public SubScene[] levelSubScenes;
   [Space]
   public PlayerAnim playerAnim;
   [Space]
   public GameObject titleUI;
   public GameObject gameUI;
   public GameObject winUI;
   public GameObject loseUI;
  
   [Space]
   public TextMeshProUGUI pelletsText;
   public TextMeshProUGUI scoreText;
  
   [Space]
   public int score;
   public int level;
   public int lives;

   private Entity currentLevelEntity;
   
   public void Awake()
   {
      instance = this;
      score = 0;
      level = 0;
      LoadLevel(0);
      AudioManager.instance.PlayMusicRequest("title");
   }
   
   public void Reset()
   {
      SwitchUI(titleUI);
      score = 0;
      lives = 3;
      LoadLevel(0);
      AudioManager.instance.PlayMusicRequest("title");
   }

   public void InGame()
   {
      SwitchUI(gameUI);
      AudioManager.instance.PlayMusicRequest("game");
   }

   public void Win()
   {
      playerAnim.Win();
      SwitchUI(winUI);
      AudioManager.instance.PlayMusicRequest("win");
   }

   public void GameOver()
   {
      playerAnim.Lose();
      SwitchUI(loseUI);
      AudioManager.instance.PlayMusicRequest("lose");
   }

   public void LoseLife()
   {
      lives--;
      if (lives <= 0)
      {
         GameOver();
      }
   }

   public void SwitchUI(GameObject newUI)
   {
      titleUI.SetActive(false);
      gameUI.SetActive(false);
      winUI.SetActive(false);
      loseUI.SetActive(false);
      
      newUI.SetActive(true);
   }

   public void SetPelletCount(int count)
   {
      if (!gameUI.activeInHierarchy)
      {
         return;
      }
      
      pelletsText.text = "Pellets: " + count;

      if (count <= 0)
      {
         Win();
      }
   }

   public void AddPoints(int points)
   {
      score += points;
      scoreText.text = "Score: " + score;
   }

   public void NextLevel()
   {
      InGame();
      LoadLevel(level + 1);
   }
   
   public void LoadLevel(int newLevel)
   {  
      if (newLevel > 2)
      {
         Reset();
         return;
      }
      
      UnloadLevel();
      level = newLevel;
      lives = 3;
     
      var sceneSystem = World.DefaultGameObjectInjectionWorld.GetOrCreateSystem<SceneSystem>();
      currentLevelEntity = sceneSystem.LoadSceneAsync(levelSubScenes[level].SceneGUID);
   }

   public void UnloadLevel()
   {
      var sceneSystem = World.DefaultGameObjectInjectionWorld.GetOrCreateSystem<SceneSystem>();
      sceneSystem.UnloadScene(currentLevelEntity);
   }
}
