using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class LevelManager : MonoBehaviour {
  public List<Vector2> SpawnPoints;
  public string NextScene = "";
  public bool ready = false;

  public bool allowsPlayersToJoin = false;

  public AbstractWeapon defaultWeapon;

  void Start() {
    SpawnPoints.Clear();
    GameObject[] spawnPointObjects = GameObject.FindGameObjectsWithTag("SpawnPoint");

    foreach (GameObject spawnPoint in spawnPointObjects) {
      Vector3 position = spawnPoint.transform.position;
      SpawnPoints.Add(new(position.x, position.y));
    }
    ready = true;
  }

  public void QuitToMenu (InputAction.CallbackContext context) {
    if(context.phase != InputActionPhase.Performed)
      return;
    // Kill off the don't destroy on load players and the gamemanager and load the first scene
    PlayerManager[] players = FindObjectsOfType<PlayerManager> ();

    for (int i = 0; i < players.Length; i++) {
      Destroy (players [i].gameObject);
    }

    Destroy (GameManager.instance.gameObject);

    SceneManager.LoadScene ("Main Menu");
  }
}
