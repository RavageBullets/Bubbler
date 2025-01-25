using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {
  public List<Vector2> SpawnPoints;
  public string NextScene = "";

  void Awake() {
    GameObject[] spawnPointObjects = GameObject.FindGameObjectsWithTag("SpawnPoint");

    foreach (GameObject spawnPoint in spawnPointObjects) {
      Vector3 position = spawnPoint.transform.position;
      SpawnPoints.Add(new(position.x, position.y));
    }
  }

}
