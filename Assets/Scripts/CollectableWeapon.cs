using UnityEngine;

public class CollectableWeapon : MonoBehaviour {

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

}
