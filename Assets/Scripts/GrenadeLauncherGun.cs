using UnityEngine;

public class GrenadeLauncherGun : AbstractWeapon {
  public Rigidbody2D projectile;
  public Rigidbody2D subProjectile;


  public override void Fire() {
    Rigidbody2D instantiatedProjectile = Instantiate(projectile, transform.position, transform.parent.rotation);
    Vector3 eulerAngles = transform.parent.rotation.eulerAngles;
    instantiatedProjectile.velocity = ConvertDegAngleToUnitVector(eulerAngles.z) * projectileSpeed * 2;

    // grenades don't die and last longer
    DieAfterTimeOrCollision dieConditions = instantiatedProjectile.transform.GetComponent<DieAfterTimeOrCollision>();
    dieConditions.dieAfterCollision = false;
    dieConditions.timeToLiveSeconds = float.PositiveInfinity;

    Grenade grenade = instantiatedProjectile.transform.GetComponent<Grenade>();
  }
}
