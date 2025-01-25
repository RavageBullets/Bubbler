using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public bool IsDead;
    public int score = 0;

  public void Awake () {
    DontDestroyOnLoad (this.gameObject);
  }

  public void Die (){
            this.IsDead = true;
            this.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
    this.gameObject.GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
            GameManager.instance.RemovePlayer (this.gameObject);

    }

  public void RevivePlayer (Vector2 spawnLocation) {
    this.IsDead = false;
    this.gameObject.GetComponent<Rigidbody2D> ().isKinematic = false;
    this.gameObject.GetComponent<Rigidbody2D> ().bodyType = RigidbodyType2D.Dynamic;
    this.gameObject.transform.position = spawnLocation;

  }
}
