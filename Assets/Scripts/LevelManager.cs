using System.Collections.Generic;
using UnityEngine;

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

}
