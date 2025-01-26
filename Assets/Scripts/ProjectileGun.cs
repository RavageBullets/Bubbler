using UnityEngine;

public class ProjectileGun : AbstractWeapon {

  public Rigidbody2D projectile;

  public override void Fire() {
    Rigidbody2D instantiatedProjectile = Instantiate(projectile, transform.position, transform.parent.rotation);
    Vector3 eulerAngles = transform.parent.rotation.eulerAngles;

    instantiatedProjectile.velocity = ConvertDegAngleToUnitVector(eulerAngles.z) * projectileSpeed;

    // both work
    // GetComponent<AudioSource>()?.Play();
    SfxManager.PlayBubble();
  }

}
