using UnityEngine;

public class PlayerColourIndicators : MonoBehaviour {

  public Sprite[] hats;

  private int nextHatIndex = 0;

  public Sprite GetNextHat() {
    return hats[nextHatIndex++ % hats.Length];
  }

}
