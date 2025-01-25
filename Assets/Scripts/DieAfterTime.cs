using UnityEngine;

public class DieAfterTime : MonoBehaviour {

  public float timeToLiveSeconds = 2f;

  private float timeOfInstantiation = 0;

  void Start() {
    timeOfInstantiation = Time.fixedTime;
  }

  void Update() {
    if (Time.fixedTime - timeOfInstantiation > timeToLiveSeconds) {
      Destroy(gameObject);
    }
  }
}
