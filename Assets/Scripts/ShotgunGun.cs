using UnityEngine;

public class ShotgunGun : AbstractWeapon {
  public Rigidbody2D projectile;

  public int numberOfProjectiles = 5;
  public int shotgunRangeInDegrees = 45;

  public override void Fire() {
    Vector3 eulerAngles = transform.parent.rotation.eulerAngles;

    Rigidbody2D[] projectiles = new Rigidbody2D[numberOfProjectiles];

    float startAngle = eulerAngles.z - (shotgunRangeInDegrees / 2);
    float angleIncrement = shotgunRangeInDegrees / numberOfProjectiles;

    for (int i = 0; i < numberOfProjectiles; i++) {
      projectiles[i] = Instantiate(projectile, transform.position, transform.parent.rotation);
      projectiles[i].velocity = ConvertDegAngleToUnitVector(startAngle + angleIncrement * i) * projectileSpeed;
    }

    GetComponent<AudioSource>()?.Play();
  }
}
