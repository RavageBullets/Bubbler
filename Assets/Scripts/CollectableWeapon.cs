using UnityEngine;

public class CollectableWeapon : MonoBehaviour, IInteractable {

  public AbstractWeapon weapon;

  public void Start() {
    RenderWeaponSprite();
  }

  void RenderWeaponSprite() {
    SpriteRenderer weaponSpriteRenderer = weapon.GetComponent<SpriteRenderer>();
    SpriteRenderer ourSpriteRenderer = this.GetComponent<SpriteRenderer>();

    if (weaponSpriteRenderer == null || ourSpriteRenderer == null) return;

    ourSpriteRenderer.sprite = weaponSpriteRenderer.sprite;
  }

  public void Interact(GameObject player) {
    WeaponInventory inventory = player.GetComponentInChildren<WeaponInventory>();
    if (inventory != null) {
      inventory.ChangeWeapon(weapon);
      Destroy(gameObject);
    }
  }

}
