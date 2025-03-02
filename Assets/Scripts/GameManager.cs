using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : MonoBehaviour {
  public static GameManager instance;
  [SerializeField]
  public PlayerAvatarList avatarList;
  [SerializeField]
  public List<GameObject> PlayerList;
  [SerializeField]
  private List<GameObject> DeadPlayerList;
  private LevelManager _lm;

  public GameObject gameOverScreen;

  public int scoreUntilNextLevel = 1;

  // minimal singleton pattern
    void Awake() {
        DontDestroyOnLoad(this.gameObject);
    }
    private void OnEnable() {
        if (instance != null && instance != this) {
            Destroy(this.gameObject);
            return;
        }

        instance = this;
    }


  public void Start() {
    var levelManagerObject = GameObject.Find("Level Manager");
    if (levelManagerObject != null) {
      _lm = levelManagerObject.GetComponent<LevelManager>();
    }
  }

  // Unity's Player Input Manager calls this when it creates a new player
  public void OnPlayerJoined(PlayerInput playerInput) {
    if(playerInput.gameObject.GetComponent<LevelManager>() != null)
      return;

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
    PlayerAvatar newAvatar = Instantiate(avatarList.playerAvatars[PlayerList.Count % 2], Vector3.zero, Quaternion.identity);
    playerManager.SetAvatar(newAvatar);
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
        player.GetComponent<PlayerManager>().SetEnabled(true);
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        player.GetComponentInChildren<WeaponInventory>().SetWeapon(_lm.defaultWeapon);
      }
    } else {
      Debug.Log("Bugger");
    }
  }

  public void ContinueRound() {
    _lm = FindObjectOfType<LevelManager>();
    if (_lm.ready) {
      // make sure all players are in appropriate starting places and ready to go.
      foreach (GameObject player in PlayerList) {
        player.GetComponent<PlayerManager>().SetEnabled(true);
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
      StartCoroutine("EndOfRound");
    }
  }

  // One round of combat till the last player standing has finished.
  // Prepare for the next round.
  public IEnumerator EndOfRound() {
    bool endLevel = false;
    PauseAllPhysics();
    ShowRoundOverScreen(PlayerList.Count > 0 ? PlayerList[0] : null);


    if (PlayerList.Count > 0) {
      PlayerList[0].GetComponent<PlayerManager>().score += 1;
      PlayerList[0].GetComponent<PlayerController>().movementDisabled = true;

      if (PlayerList[0].GetComponent<PlayerManager>().score >= scoreUntilNextLevel) {
        endLevel = true;
      }
    }

    yield return new WaitForSeconds(3);

    if (endLevel) {
      StartCoroutine("NextLevel");
    } else
      StartCoroutine("RestartLevel");
  }

  // Had enough rounds in this scene, time to move to the next one (if it's specified)
  public IEnumerator NextLevel() {
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
      PlayerManager manager = _go.GetComponent<PlayerManager>();
      manager.ResetPlayer();
      _go.transform.position = _lm.SpawnPoints[index++];
      _go.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
      if (index == _lm.SpawnPoints.Count)
        index = 0;
    }
    DeadPlayerList.Clear();
    ContinueRound();
  }

  private void PauseAllPhysics() {
    Rigidbody2D[] rigidbodies = FindObjectsOfType<Rigidbody2D>();

    foreach (Rigidbody2D rigidbody in rigidbodies) {
      rigidbody.Sleep();
    }
  }

  private void ShowRoundOverScreen(GameObject winner) {
    GameObject gameOver = Instantiate(gameOverScreen);

    if (winner != null) {
      var canvas = gameOver.transform.Find("Canvas");
      var playerDisplay = canvas.Find("PlayerDisplay");

      var color = winner.transform.Find("SurroundingBubble").GetComponent<SpriteRenderer>().color;
      var staticSprite = winner.GetComponent<PlayerManager>().avatar.staticSprite;

      playerDisplay.gameObject.SetActive(true);
      playerDisplay.Find("Bubble").GetComponent<Image>().color = color;
      playerDisplay.Find("Sprite").GetComponent<Image>().sprite = staticSprite;
      playerDisplay.Find("Winner").GetComponent<TMP_Text>().color = color;
      MusicManager.PlayVictory();
    }
  }
}
