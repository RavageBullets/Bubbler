using UnityEngine;
using UnityEngine.InputSystem;

public abstract class AbstractWeapon : MonoBehaviour {
  public PlayerInput playerInput;
  public float fireDelaySeconds = 0.5f;
  public float projectileSpeed = 4f;

  private float timeOfLastFire = 0;

  public void Update() {
    if (!playerInput.actions["Fire"].IsPressed()) return;

    if (Time.fixedTime - timeOfLastFire < fireDelaySeconds) return;
    timeOfLastFire = Time.fixedTime;

    Fire();
  }


  public abstract void Fire();

  static public Vector2 ConvertDegAngleToUnitVector(float angle) {
    return (Vector2)(Quaternion.Euler(0, 0, angle) * Vector2.right);
  }

}