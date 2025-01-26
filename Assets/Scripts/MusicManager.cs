using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour {
    // Start is called before the first frame update
    public AudioClip battleClip;
    public AudioClip defeatClip;
    public AudioClip prepostClip;
    public AudioClip titleClip;
    public AudioClip victoryClip;

    private static MusicManager Instance;
    void Awake() {
        DontDestroyOnLoad(this.gameObject);
    }
    private void OnEnable() {
        // Debug.Log("OnEnable");
        if (Instance != null && Instance != this) {
            Destroy(this);
            return;
        }

        Instance = this;
    }
    private void Start() {
        SceneManager.activeSceneChanged += ChangedActiveScene;
        if (SceneManager.GetActiveScene().name == "Main Menu") {
            this.GetComponent<AudioSource>().clip = this.prepostClip;
            this.GetComponent<AudioSource>().Play();
        }
    }
    private void ChangedActiveScene(Scene current, Scene next) {
        string currentName = current.name;
        string nextName = next.name;
        if (nextName.Substring(0, 5) == "Level" || nextName.Substring(0, 5) == "Arena"
        ) {
            this.GetComponent<AudioSource>().clip = battleClip;
            this.GetComponent<AudioSource>().Play();
        } else if (nextName == "Main Menu") {
            this.GetComponent<AudioSource>().clip = this.prepostClip;
            this.GetComponent<AudioSource>().Play();
        }
    }

}
