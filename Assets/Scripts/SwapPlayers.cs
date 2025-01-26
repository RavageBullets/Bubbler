using System.Linq;
using UnityEngine;

public class SwapPlayers : MonoBehaviour, IInteractable {

  public ParticleSystem teleportationParticles;

  public void Interact(GameObject player) {
    GameObject[] players = Shuffle(GameObject.FindGameObjectsWithTag("Player"));
    GameObject[] alivePlayers = players.Where(player => !player.GetComponent<PlayerManager>().isDead).ToArray();

    for (int i = 0; i < alivePlayers.Length; i += 2) {
      if (i + 1 >= alivePlayers.Length) break;

      GameObject playerA = alivePlayers[i];
      GameObject playerB = alivePlayers[i + 1];

      Rigidbody2D rigidbodyA = playerA.GetComponent<Rigidbody2D>();
      Rigidbody2D rigidbodyB = playerB.GetComponent<Rigidbody2D>();

      (playerA.transform.position, playerB.transform.position) = (playerB.transform.position, playerA.transform.position);
      (rigidbodyA.velocity, rigidbodyB.velocity) = (rigidbodyB.velocity, rigidbodyA.velocity);

      Instantiate(teleportationParticles, playerA.transform.position, playerA.transform.rotation);
      Instantiate(teleportationParticles, playerB.transform.position, playerB.transform.rotation);
    }
  }

  private T[] Shuffle<T>(T[] items) {
    T[] result = (T[])items.Clone();

    for (int i = 0; i < items.Length; i++) {
      int forwardIndex = Random.Range(i, items.Length);
      (items[i], items[forwardIndex]) = (items[forwardIndex], items[i]);
    }

    return result;
  }
}

