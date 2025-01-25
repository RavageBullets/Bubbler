using UnityEngine;

public class DieAfterTimeOrCollision : MonoBehaviour {


  public float timeToLiveSeconds = 2f;

  public bool dieAfterCollision = true;
  public float invulnerablePeriodSeconds = 0.01f;

  private float timeOfInstantiation = 0;

  void Start() {
    timeOfInstantiation = Time.fixedTime;
  }

  void Update() {
    if (Time.fixedTime - timeOfInstantiation > timeToLiveSeconds) {
      Destroy(gameObject);
    }
  }

  void OnCollisionEnter2D() {
    if (dieAfterCollision && Time.fixedTime - timeOfInstantiation > invulnerablePeriodSeconds) {
      Destroy(gameObject);
    }
  }
}
