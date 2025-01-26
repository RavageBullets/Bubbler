using System.Collections;
using UnityEngine;

public class StartGamePortal : MonoBehaviour {

  public GameObject howToPlayUi;

  public void OnTriggerEnter2D(Collider2D other) {
    Collider2D[] colliders = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y), 5f);

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
