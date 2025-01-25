using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
  public static GameManager instance;
  [SerializeField]
  private List<GameObject> PlayerList;
  private LevelManager _lm;

  private void Awake() {
    if (instance == null) {
      instance = this;
    } else {
      Destroy(gameObject);
    }
  }

  public void OnPlayerJoined(PlayerInput playerInput) {
    Debug.Log("Player '" + playerInput.currentControlScheme + "' -  Yay");

    AddPlayer(playerInput.gameObject);
  }

  public void AddPlayer(GameObject playerObject) {
    if (PlayerList == null) {
      PlayerList = new List<GameObject>();
    }
    PlayerList.Add(playerObject);

    playerObject.GetComponent<PlayerController>().SetHat(this.gameObject.GetComponent<PlayerColourIndicators>().GetNextHat());

    // Teleport to a spawn point.
    _lm = FindObjectOfType<LevelManager> ();
    if (_lm != null) {
      playerObject.transform.position = _lm.SpawnPoints [PlayerList.Count - 1];
    }
  }


  public void RemovePlayer(GameObject playerObject) {
    PlayerList.Remove(playerObject);

    if (PlayerList.Count <= 1) {
      EndOfRound();
    }
  }

  public void EndOfRound() {
    if (PlayerList.Count > 0) {
      Debug.Log("Player " + PlayerList[0].name + " Wins!!!");
    }

    StartCoroutine("RestartLevel");
  }


  private IEnumerator RestartLevel() {
    yield return new WaitForSeconds(3);
    if (_lm != null) {
      if(_lm.NextScene.Length == 0)
        SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
      else
        SceneManager.LoadScene (_lm.NextScene);
    } else {
      SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
    }
  }

  // Update is called once per frame
  void Update() {

  }
}
