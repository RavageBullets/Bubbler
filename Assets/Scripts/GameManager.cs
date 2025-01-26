using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour {
  public static GameManager instance;
  [SerializeField]
  public List<GameObject> PlayerList;
  [SerializeField]
  private List<GameObject> DeadPlayerList;
  private LevelManager _lm;

  public int scoreUntilNextLevel = 1;

  // minimal singleton pattern
  private void Awake() {
    if (instance == null) {
      instance = this;
      DontDestroyOnLoad(gameObject);
    } else {
      Destroy(gameObject);
    }
  }

  public void Start() {
    var levelManagerObject = GameObject.Find("Level Manager");
    if (levelManagerObject != null) {
      _lm = levelManagerObject.GetComponent<LevelManager>();
    }
  }

  // Unity's Player Input Manager calls this when it creates a new player
  public void OnPlayerJoined(PlayerInput playerInput) {
    if (_lm.allowsPlayersToJoin) {
      AddPlayer(playerInput.gameObject);
      return;
    }

    Destroy(playerInput.gameObject);
  }


  public void AddPlayer(GameObject player) {
    PlayerList ??= new List<GameObject>();
    PlayerList.Add(player);

    var playerManager = player.GetComponent<PlayerManager>();
    player.transform.position = _lm.SpawnPoints[PlayerList.Count - 1 % _lm.SpawnPoints.Count];
    playerManager.SetColor(gameObject.GetComponent<PlayerColourIndicators>().GetNextColor());

    DontDestroyOnLoad(player);
  }

  // To be called after a new level scene has been loaded.
  // Needs to happen after the new scenes LevelManager has awoken.
  // Currently half-assing that by using coroutines to delay a second after calling LoadScene before calling this
  public void BeginNewRound() {
    _lm = FindObjectOfType<LevelManager>();
    if (_lm.ready) {
      // make sure all players are in appropriate starting places and ready to go.
      int i = 0;
      foreach (GameObject player in PlayerList) {
        player.transform.position = _lm.SpawnPoints[i++];
        Debug.Log(player);
        player.GetComponent<PlayerManager>().SetEnabled(true);
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        player.GetComponentInChildren<WeaponInventory>().SetWeapon(_lm.defaultWeapon);
      }
    } else {
      Debug.Log("Bugger");
    }
  }

  // called when a player dies.  When down to the last player, declare them the winner and end the round
  public void RemovePlayer(GameObject playerObject) {
    DeadPlayerList ??= new List<GameObject>();
    DeadPlayerList.Add(playerObject);
    PlayerList.Remove(playerObject);

    if (PlayerList.Count <= 1) {
      EndOfRound();
    }
  }

  // One round of combat till the last player standing has finished.
  // Prepare for the next round.
  public void EndOfRound() {
    bool endLevel = false;
    // declare the winner and decide if there's been enough rounds and the game/level should be finished.
    if (PlayerList.Count > 0) {
      Debug.Log("Player " + PlayerList[0].name + " Wins!!!");
      PlayerList[0].GetComponent<PlayerManager>().score += 1;
      Debug.Log("Their score is: " + PlayerList[0].GetComponent<PlayerManager>().score);
      if (PlayerList[0].GetComponent<PlayerManager>().score >= scoreUntilNextLevel) {
        endLevel = true;
      }
    }

    if (endLevel) {
      StartCoroutine("NextLevel");
    } else
      StartCoroutine("RestartLevel");
  }

  // Had enough rounds in this scene, time to move to the next one (if it's specified)
  public IEnumerator NextLevel() {
    // Pause so the winner can admire their achievement.
    // yield return new WaitForSeconds(3);

    // Begin resetting the dead players
    foreach (GameObject _go in DeadPlayerList) {
      PlayerList.Add(_go);
    }
    DeadPlayerList.Clear();

    foreach (GameObject _go in PlayerList) {
      PlayerManager manager = _go.GetComponent<PlayerManager>();
      manager.ResetPlayer();
      manager.score = 0;
    }

    // Determine which scene to load.
    // As this object isn't getting destroyed, after loading the scene, wait a second,then reset things for a new set of rounds.
    SceneManager.LoadScene(GetSceneToLoad());
    yield return new WaitForSeconds(1);
    BeginNewRound();
  }

  private string GetSceneToLoad() {
    if (_lm == null || _lm.NextScene.Length == 0) {
      return SceneManager.GetActiveScene().name;
    }

    return _lm.NextScene;
  }

  // Hasn't been enough rounds so after a few seconds to display scores / etc, reset the level
  // Leaving the winner where they were as the level isn't changing.
  private IEnumerator RestartLevel() {
    yield return new WaitForSeconds(3);

    int index = Random.Range(0, _lm.SpawnPoints.Count);
    foreach (GameObject _go in DeadPlayerList) {
      PlayerList.Add(_go);
      _go.GetComponent<PlayerManager>().ResetPlayer();
      _go.transform.position = _lm.SpawnPoints[index++];
      if (index == _lm.SpawnPoints.Count)
        index = 0;
    }
    DeadPlayerList.Clear();
  }

}
