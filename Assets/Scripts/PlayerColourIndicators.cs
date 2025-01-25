using UnityEngine;

public class PlayerColourIndicators : MonoBehaviour {

  public Color[] playerColors = new Color[2]{
    // new Color(0,227,255,1),
    new Color(255,0,0,1),
    // new Color(0,255,15,1),
    new Color(255,255,0,1),
  };
  public Sprite[] hats;

  private int nextHatIndex = 0;

  public Sprite GetNextHat() {
    return hats[nextHatIndex++ % hats.Length];
  }
  public Color GetNextColor() {
    return playerColors[nextHatIndex++ % hats.Length];
  }

}
