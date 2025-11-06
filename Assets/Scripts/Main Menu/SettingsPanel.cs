using UnityEngine;
using UnityEngine.UI;

public class SettingsPanel : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public Slider volumeSlider;
    public AudioSource musicSource;

    void Start()
    {
        if (volumeSlider != null)
            volumeSlider.value = AudioListener.volume;

        volumeSlider.onValueChanged.AddListener(ChangeVolume);
    }

    void ChangeVolume(float value)
    {
        AudioListener.volume = value;
    }

    public void Back()
    {
        gameObject.SetActive(false);
        mainMenuPanel.SetActive(true);
    }
}
