using UnityEngine;

public class PlayerManager : MonoBehaviour {
  public bool isDead;
  public ParticleSystem deathParticles;
  public int score = 0;



  public void Die() {
    var particles = Instantiate(deathParticles, this.transform.position, this.transform.rotation);
    particles.Play();
    this.isDead = true;
    this.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
    SpriteRenderer[] spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
    foreach (var sr in spriteRenderers) {
      sr.enabled = false;
    }
    GameManager.instance.RemovePlayer(this.gameObject);
  }

  public void RevivePlayer(Vector2 spawnLocation) {
    this.isDead = false;
    this.gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
    this.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
    this.gameObject.transform.position = spawnLocation;
  }

  public void SetColor(Color color) {
    transform.Find("SurroundingBubble").GetComponent<SpriteRenderer>().color = color;
  }

  public void SetEnabled(bool active) {
    foreach (Transform child in transform) {
      child.gameObject.SetActive(active);
    }
  }

}
