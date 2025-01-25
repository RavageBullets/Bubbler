using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponControls : MonoBehaviour {

  public void Look(InputAction.CallbackContext context) {
    Vector2 lookXY = context.ReadValue<Vector2>();
    float lookDirection = Vector2.SignedAngle(Vector2.right, lookXY);

    transform.parent.rotation = Quaternion.Euler(0, 0, lookDirection);

    // make child weapon look
    SpriteRenderer childWeapon = GetComponentInChildren<SpriteRenderer>();
    if(childWeapon) {
      bool isLeft = lookXY.x < 0;
      childWeapon.flipX = isLeft;
      childWeapon.transform.localEulerAngles = isLeft ? new Vector3(0,0,180) : Vector3.zero;
    }

  }
}
