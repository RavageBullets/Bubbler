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
            this.GetComponent<AudioSource>().clip = this.titleClip;
            this.GetComponent<AudioSource>().Play();
        }
    }
    private void ChangedActiveScene(Scene current, Scene next) {
        string currentName = current.name;
        string nextName = next.name;
        AudioSource _as = Instance.GetComponent<AudioSource>();
        if (nextName.Substring(0, 5) == "Level" || nextName.Substring(0, 5) == "Arena"
        ) {
            _as.loop = true;
            _as.clip = battleClip;
            _as.Play();
        } else if (nextName == "Main Menu") {
            _as.loop = true;
            _as.clip = this.prepostClip;
            _as.Play();
        } else if (nextName == "Join Menu") {
            _as.loop = true;
            _as.clip = this.prepostClip;
            _as.Play();
        }
    }

    public static void PlayVictory() {
        AudioSource _as = Instance.GetComponent<AudioSource>();
        _as.loop = false;
        _as.clip = Instance.victoryClip;
        _as.Play();
    }

}
