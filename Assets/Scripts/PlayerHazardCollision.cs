using UnityEngine;

public class PlayerHazardCollision : MonoBehaviour {

  void OnCollisionEnter2D(Collision2D c) {
    if (c.gameObject.tag == "Hazard") {
      this.gameObject.GetComponent<PlayerManager>().Die();
    }
  }
}
