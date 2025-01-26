using UnityEngine;

public class PlayerManager : MonoBehaviour {

  public PlayerAvatar avatar;
  public bool isDead;
  public ParticleSystem deathParticles;
  public int score = 0;

  private bool canDie = true;

  public void Die() {
    if (!canDie) return;

    var particles = Instantiate(deathParticles, this.transform.position, this.transform.rotation);
    particles.Play();

    this.isDead = true;
    this.gameObject.GetComponent<Rigidbody2D>().Sleep();
    SetEnabled(false);
    GameManager.instance.RemovePlayer(this.gameObject);
  }

  public void ResetPlayer() {
    isDead = false;
    gameObject.GetComponent<Rigidbody2D>().WakeUp();
    SetEnabled(false);
  }

  public void SetAvatar(PlayerAvatar avatar) {
    if (avatar != null) 
      Destroy(this.avatar);
    this.avatar = avatar;
    this.avatar.transform.parent = gameObject.transform;
    this.avatar.transform.localPosition = Vector3.zero;
  }

  public void EquipWeapon(AbstractWeapon weapon) {
    weapon.GetComponent<SpriteRenderer>().sortingOrder = avatar.LeftArm.sortingOrder - 1;
    avatar.UpdateInverseKinematics(weapon.GunAnchorL, weapon.GunAnchorR);
  }

  public void SetColor(Color color) {
    transform.Find("SurroundingBubble").GetComponent<SpriteRenderer>().color = color;
  }

  public void SetEnabled(bool active) {
    canDie = active;
    GetComponent<PlayerController>().movementDisabled = !active;
    foreach (Transform child in transform) {
      child.gameObject.SetActive(active);
    }
  }

}
