using UnityEngine;

public class PlayerColourIndicators : MonoBehaviour {

  public Color[] playerColors = new Color[4]{
    new(255,0,0,1),
    new(0,255,0,1),
    new(255, 128, 0,1),
    new(0,0,255,1),
  };
  private int nextColorIndex = 0;

  public Color GetNextColor() {
    return playerColors[nextColorIndex++ % playerColors.Length];
  }

}
