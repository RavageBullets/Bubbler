using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControl : MonoBehaviour
{
  public string firstLevelSceneName;
  public void OnClick(string option) {
    Debug.Log($"Clicked! {option}");
    switch (option) {
      case "play":
      SceneManager.LoadScene(firstLevelSceneName);
        break;
      case "options":
        break;
      case "quit":
        break;
         // no-op
    }
  }
}
