using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
Some audio clips need to be played after the GameObject is Destroyed
*/
public class SfxManager : MonoBehaviour
{
    public AudioClip popClip;
    public AudioClip boingClip;
    public AudioClip bubbleClip;
    public AudioClip bumpClip;
    public AudioClip droneClip;
    public AudioClip tinyImpactClip;

    private static SfxManager Instance;
    private AudioSource audioSource;


    private void OnEnable() {
        Debug.Log("OnEnable!");
        if (Instance != null && Instance != this) {
            Destroy(this);
            return;
        }

        Instance = this;
        audioSource = this.GetComponent<AudioSource>();
    }

    public static void PlayPop() {        
        Instance.GetComponent<AudioSource>().PlayOneShot(Instance.popClip);
    }
    public static void PlayBoing() {        
        Instance.GetComponent<AudioSource>().PlayOneShot(Instance.boingClip);
    }
    public static void PlayBubble() {        
        Instance.GetComponent<AudioSource>().PlayOneShot(Instance.bubbleClip);
    }
    public static void PlayBump() {        
        Instance.GetComponent<AudioSource>().PlayOneShot(Instance.bumpClip);
    }
    public static void PlayDrone() {        
        Instance.GetComponent<AudioSource>().PlayOneShot(Instance.droneClip);
    }
    public static void PlayTinyImpact() {        
        Instance.GetComponent<AudioSource>().PlayOneShot(Instance.tinyImpactClip);
    }
}
