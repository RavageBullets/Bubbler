using System.Collections;
using UnityEngine;

public class StartGamePortal : MonoBehaviour {

  public GameObject howToPlayUi;
  public float overlapAmount = 12f;

  public void OnTriggerEnter2D(Collider2D other) {
    Collider2D[] colliders = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y), overlapAmount);

    int playerCount = 0;
    foreach (Collider2D collider in colliders) {
      if (collider.gameObject.CompareTag("Player")) {
        playerCount++;
      }
    }

    if (playerCount == GameManager.instance.PlayerList.Count && playerCount > 1) {
      StartCoroutine("BeginGame");
    }
  }

  private IEnumerator BeginGame() {
    howToPlayUi.SetActive(true);
    yield return new WaitForSeconds(3);
    GameManager.instance.StartCoroutine("NextLevel");
  }

}
