using UnityEngine;

public class Grenade : MonoBehaviour {
    private float timeOfInstantiation = 0;
    public float timeToExplodeInSeconds = 5f;
    public Rigidbody2D projectile;
    public int numberOfProjectiles = 10;

    public float projectileSpeed = 10f;

    public void Start() {
        timeOfInstantiation = Time.fixedTime;
    }

    void Update() {
        if (Time.fixedTime - timeOfInstantiation > timeToExplodeInSeconds) {
            Explode();
        }
    }
    void Explode() {
        for (int i = 0; i < numberOfProjectiles; i++) {
            Rigidbody2D pellet = Instantiate(projectile, transform.position, transform.rotation);
            pellet.velocity = AbstractWeapon.ConvertDegAngleToUnitVector(360 / numberOfProjectiles * i) * projectileSpeed;
        }
        Destroy(gameObject);
    }
}
