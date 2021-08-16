using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
  [SerializeField]
  private Transform checkpoint2;
  [SerializeField]
  private Transform checkpoint3;
  [SerializeField]
  private GameObject player;
  //[SerializeField]
  //private TMP_Text soulText;
  public void Start () {
    if(PlayerPrefs.HasKey("currentLevel")){
      currentLevel = PlayerPrefs.GetInt("currentLevel");
      numberOfSouls = dictionary[currentLevel-1];
      if(currentLevel==2){
        player.transform.position = checkpoint2.position;
      }else if(currentLevel==3){
        player.transform.position = checkpoint3.position;
      }
    }else {
     // Debug.Log("Yeni Başladın Demek Heee");
    }
    //soulText.text = numberOfSouls.ToString();
  }
  /*public void IncreaseSoul () {
    print("touched");
    numberOfSouls++;
    soulText.text = numberOfSouls.ToString();
    if(dictionary[currentLevel]==numberOfSouls){
      UpdateLevel();
    }
    
  }*/
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