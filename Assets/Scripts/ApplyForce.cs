using UnityEngine;

public class ApplyForce : MonoBehaviour {

  public float force = 2f;

  private Vector2 direction;

  void OnTriggerStay2D(Collider2D collider) {
    Rigidbody2D rigidbody = collider.GetComponent<Rigidbody2D>();

    if (rigidbody != null) {
      Vector3 eulerAngles = transform.rotation.eulerAngles;
      direction = (Vector2)(Quaternion.Euler(0, 0, eulerAngles.z) * Vector2.up);
      rigidbody.AddForce(direction * force);
    }
  }
}
