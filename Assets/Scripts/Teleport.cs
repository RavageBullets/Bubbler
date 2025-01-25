using UnityEngine;

public class Teleport : MonoBehaviour {
  public Teleport target;
  public float timeToDisableAfterExitSeconds = 1f;

  private bool disabled = false;
  private float disabledTime = 0;

  void Update() {
    if (disabled && Time.fixedTime - disabledTime >= timeToDisableAfterExitSeconds) {
      disabled = false;
    }
  }

  void OnTriggerEnter2D(Collider2D collider) {
    if (disabled) return;
    target.TemporarilyDisable();
    collider.transform.position = target.transform.Find("Exit").transform.position;
  }

  public void TemporarilyDisable() {
    disabled = true;
    disabledTime = Time.fixedTime;

  }
}
