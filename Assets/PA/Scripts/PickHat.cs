using UnityEngine;

public class PickHat : MonoBehaviour {
  public SpriteRenderer spriteRenderer;

  void Start() {
    GameObject playerInput = GameObject.Find("Player Input");
    PlayerColourIndicators indicators = playerInput.GetComponent<PlayerColourIndicators>();
    if (indicators == null) return;

    spriteRenderer.sprite = indicators.GetNextHat();
  }
}
