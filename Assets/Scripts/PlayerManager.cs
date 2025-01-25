using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public bool IsDead;
    public int score = 0;

    public void Die (){
            this.IsDead = true;
            this.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
            GameManager.instance.RemovePlayer (this.gameObject);

    }

  public void RevivePlayer (Vector2 spawnLocation) {
    this.IsDead = false;
    this.gameObject.GetComponent<Rigidbody2D> ().isKinematic = false;
    this.gameObject.transform.position = spawnLocation;

  }
}
