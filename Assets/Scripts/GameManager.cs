using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
  public static GameManager instance;
  [SerializeField]
  private List<GameObject> PlayerList;

  private void Awake ()
  {
    if (instance == null) {
      instance = this;
    } else {
      Destroy (gameObject);
    }
  }

  public void OnPlayerJoined (PlayerInput playerInput) {
    Debug.Log ("Player Joined Yay");
    AddPlayer (playerInput.gameObject);
  }

  public void AddPlayer (GameObject playerObject)
  {
    if (PlayerList == null) {
      PlayerList = new List<GameObject> ();
    }
    PlayerList.Add (playerObject);

    playerObject.GetComponent<PAPlayerController>().SetHat( this.gameObject.GetComponent<PlayerColourIndicators>().GetNextHat () );
    Debug.Log ("Player " + playerObject.name + " Added");
  }


  public void RemovePlayer (GameObject playerObject) {
    PlayerList.Remove (playerObject);

    if (PlayerList.Count <= 1) {
      EndOfRound ();
    }
  }

  public void EndOfRound () {
    Debug.Log ("Player " + PlayerList [0].name + " Wins!!!");
    StartCoroutine ("RestartLevel");
  }


  private IEnumerator RestartLevel () {
    yield return new WaitForSeconds (3); 
    SceneManager.LoadScene (SceneManager.GetActiveScene().name);
  }

    // Update is called once per frame
    void Update()
    {
        
    }
}
