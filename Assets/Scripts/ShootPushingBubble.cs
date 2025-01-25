using UnityEngine;
using UnityEngine.InputSystem;

public class ShootPushingBubble : MonoBehaviour {

  public Rigidbody2D projectile;
  public PlayerInput playerInput;
  public float fireDelaySeconds = 0.5f;
  public float projectileSpeed = 4f;

  private float timeOfLastFire = 0;

  public void Update() {
    if (!playerInput.actions["Fire"].IsPressed()) return;

    if (Time.fixedTime - timeOfLastFire < fireDelaySeconds) return;
    timeOfLastFire = Time.fixedTime;

    Rigidbody2D instantiatedProjectile = Instantiate(projectile, transform.position, transform.parent.rotation);
    Vector3 eulerAngles = transform.parent.rotation.eulerAngles;

    instantiatedProjectile.velocity = ConvertDegAngleToUnitVector(eulerAngles.z) * projectileSpeed;
  }


  private Vector2 ConvertDegAngleToUnitVector(float angle) {
    return (Vector2)(Quaternion.Euler(0, 0, angle) * Vector2.right);
  }

}
