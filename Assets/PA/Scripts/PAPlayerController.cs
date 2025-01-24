using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PAPlayerController : MonoBehaviour {
  public float MovementSpeed = 1.0f;
  [SerializeField]
  private Rigidbody2D RB2D;
  private SpriteRenderer _SR;

  private Vector2 movement = Vector2.zero;
  // Start is called before the first frame update
  void Start() {
    if (RB2D == null)
      RB2D = this.gameObject.GetComponent<Rigidbody2D>();
    if (_SR == null)
      _SR = GetComponent<SpriteRenderer>();

  }

  public void Move(InputAction.CallbackContext context) {
    movement = context.ReadValue<Vector2>() * MovementSpeed;
    _SR.flipX = Vector2.Dot(movement, Vector2.right) < 0;

  }

  void FixedUpdate() {
    RB2D.AddForce((movement - RB2D.velocity) * Time.deltaTime, ForceMode2D.Impulse);
    // 'Move' code here
  }
}
