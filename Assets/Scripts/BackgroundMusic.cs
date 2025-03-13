using UnityEngine;
using UnityEngine.UI;

public class BackgroundMusic : MonoBehaviour
{
    private AudioSource audioSource;
    public Slider volumeSlider;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        // setting the default volume
        if (volumeSlider != null)
        {
            volumeSlider.value = audioSource.volume; // sets the slider to current volume
            volumeSlider.onValueChanged.AddListener(SetVolume);
        }
    }

    public void SetVolume(float volume)
    {
        audioSource.volume = volume; // changes the volume based on slider value
    }
}