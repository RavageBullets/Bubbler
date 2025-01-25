using UnityEngine;
using UnityEngine.Events;

public class LevelButton : MonoBehaviour, IInteractable {

  public Sprite normalSprite;
  public Sprite pressedSprite;

  public UnityEvent<GameObject> buttonAction;

  private SpriteRenderer spriteRenderer;

  private bool pressed = false;
  private float lastPressedAtSeconds = 0;
  private readonly float remainsPressedForSeconds = 0.1f;

  void Start() {
    spriteRenderer = GetComponent<SpriteRenderer>();
  }

  void Update() {
    if (pressed && Time.fixedTime - lastPressedAtSeconds > remainsPressedForSeconds) {
      pressed = false;
      spriteRenderer.sprite = normalSprite;
    }
  }

  public void Interact(GameObject player) {
    spriteRenderer.sprite = pressedSprite;
    pressed = true;
    lastPressedAtSeconds = Time.fixedTime;

    buttonAction.Invoke(player);
  }
}
