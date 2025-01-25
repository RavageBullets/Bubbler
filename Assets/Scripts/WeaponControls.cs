using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponControls : MonoBehaviour {

  public void Look(InputAction.CallbackContext context) {
    Vector2 lookXY = context.ReadValue<Vector2>();
    float lookDirection = Vector2.SignedAngle(Vector2.right, lookXY);

    transform.rotation = Quaternion.Euler(0, 0, lookDirection);
  }
}
