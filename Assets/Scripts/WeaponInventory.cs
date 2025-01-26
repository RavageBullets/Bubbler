using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponInventory : MonoBehaviour {

  public AbstractWeapon currentWeapon;
  public PlayerInput playerInput;
  public CollectableWeapon collectableWeaponPrefab;
  public Collider2D playerCollider;

  public void Start() {
    HoldCurrentWeapon();
  }

  public void SetWeapon(AbstractWeapon newWeapon) {
    currentWeapon = newWeapon;
    HoldCurrentWeapon();
  }

  public void ChangeWeapon(AbstractWeapon newWeapon) {
    DropCurrentWeapon();
    currentWeapon = newWeapon;
    HoldCurrentWeapon();
  }

  private void HoldCurrentWeapon() {
    foreach (Transform child in transform) {
      Destroy(child.gameObject);
    }

    AbstractWeapon newWeapon = Instantiate(currentWeapon, transform);
    newWeapon.playerInput = playerInput;
  }

  private void DropCurrentWeapon() {
    Vector3 droppedWeaponPosition = new(
         transform.position.x + Random.Range(-1f, 1f),
         transform.position.y + 1,
         transform.position.z
       );
    CollectableWeapon droppedWeapon = Instantiate(collectableWeaponPrefab, droppedWeaponPosition, transform.rotation);
    droppedWeapon.weapon = currentWeapon;
  }

}
