using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour {
  public float MovementSpeed = 1.0f;
  [SerializeField]
  private Rigidbody2D RB2D;
  private SpriteRenderer _SR;

  private GameManager _gm;
  private Vector2 movement = Vector2.zero;

  [SerializeField]
  private SpriteRenderer _hat;

  // Start is called before the first frame update
  void Start() {
    if (RB2D == null) {
      RB2D = this.gameObject.GetComponent<Rigidbody2D>();
    }

    if (_gm == null) {
      _gm = GameManager.instance;
    }

    // To Be Removed
    if (_SR == null) {
      _SR = GetComponent<SpriteRenderer>();
    }

    if (_hat == null) {
      _hat = transform.Find("Hat").GetComponent<SpriteRenderer>();
    }
  }

  public void SetHat(Sprite playerHat) {
    _hat.sprite = playerHat;
  }

  public void Move(InputAction.CallbackContext context) {
    movement = context.ReadValue<Vector2>() * MovementSpeed;
    if (_SR) {
      _SR.flipX = Vector2.Dot(movement, Vector2.right) < 0;
    }

  }

  void FixedUpdate() {
    RB2D.AddForce((movement - RB2D.velocity) * Time.deltaTime, ForceMode2D.Impulse);
    // 'Move' code here
  }
}
