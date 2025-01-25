using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {
  public List<Vector2> SpawnPoints;
  public string NextScene = "";
  public bool ready = false;

  void Awake() {
    Debug.Log ("Adding spawn points");
    SpawnPoints.Clear ();
    GameObject[] spawnPointObjects = GameObject.FindGameObjectsWithTag("SpawnPoint");

    foreach (GameObject spawnPoint in spawnPointObjects) {
      Vector3 position = spawnPoint.transform.position;
      SpawnPoints.Add(new Vector2(position.x, position.y));
    }
    ready = true;
  }

}
