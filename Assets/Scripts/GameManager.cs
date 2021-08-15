using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
  private int numberOfSouls = 0;
  /*
  Level1 -> 3
  Level2 -> 6
  Level3 -> 9
  */
  private int currentLevel = 1;

  Dictionary<int, int> dictionary = new Dictionary<int, int>(){
    {1, 3},
    {2, 6},
    {3, 9}
  };
  public void Start () {
    print(currentLevel);
    if(PlayerPrefs.HasKey("currentLevel")){
      currentLevel = PlayerPrefs.GetInt("currentLevel");
      numberOfSouls = dictionary[currentLevel-1];
      Debug.Log(currentLevel);
    }else {
      Debug.Log("Yeni Başladın Demek Heee");
    }
  }
  public void IncreaseSoul () {
    numberOfSouls++;
    if(dictionary[currentLevel]==numberOfSouls){
      UpdateLevel();
    }
    
  }
  public void UpdateLevel () {
    if(currentLevel>3)
      return;
    currentLevel++;
    OpenDoor(currentLevel);
    PlayerPrefs.SetInt("currentLevel",currentLevel);
    PlayerPrefs.Save();
  }
  public void OpenDoor(int level) {

  }
}