using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBubble : MonoBehaviour
{
  public float BubbleValue = 10.0f;
  [SerializeField]
  private Rigidbody2D RB2D;

    // Start is called before the first frame update
    void Start()
    {
      RB2D = this.gameObject.GetComponent<Rigidbody2D> ();
     
    }

    // Update is called once per frame
    void Update()
    {
      RB2D.AddForce (Vector2.up * Time.deltaTime * BubbleValue, ForceMode2D.Impulse);
    }
}
