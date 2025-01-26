using Unity.VisualScripting;
using UnityEngine;

public class StartGamePortal : MonoBehaviour {

  public void OnTriggerEnter2D(Collider2D other) {
    Collider2D[] colliders = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y), 5f);

    int playerCount = 0;
    foreach (Collider2D collider in colliders) {
      if (collider.gameObject.CompareTag("Player")) {
        playerCount++;
      }
    }

    Debug.Log(GameManager.instance.PlayerList);

    if (playerCount == GameManager.instance.PlayerList.Count) {
      GameManager.instance.StartCoroutine("NextLevel");
    }
  }

}
