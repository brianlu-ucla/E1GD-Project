using UnityEngine;

public class MusicController : MonoBehaviour
{
    void Start()
    {
        // gets the saved volume and applies it to the current track
        GetComponent<AudioSource>().volume = AudioListener.volume;
    }
}