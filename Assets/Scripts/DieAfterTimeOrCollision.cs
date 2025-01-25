using UnityEngine;

public class DieAfterTimeOrCollision : MonoBehaviour {

  public float timeToLiveSeconds = 2f;

  public bool dieAfterCollision = true;
  public float invulnerablePeriodSeconds = 0.01f;
  public GameObject explosionCollider;

  private float timeOfInstantiation = 0;

  void Start() {
    timeOfInstantiation = Time.fixedTime;
  }

  void Update() {
    if (Time.fixedTime - timeOfInstantiation > timeToLiveSeconds) {
      Destroy(gameObject);
    }
  }

  void OnCollisionEnter2D(Collision2D collision) {
    if (
      dieAfterCollision &&
      Time.fixedTime - timeOfInstantiation > invulnerablePeriodSeconds &&
      !collision.collider.CompareTag("Bouncy")
    ) {
      if (explosionCollider != null){
        collision.GetComponent<ApplyExplosionForce>().Explode();
      }
      Destroy(gameObject);
    }
  }
}
