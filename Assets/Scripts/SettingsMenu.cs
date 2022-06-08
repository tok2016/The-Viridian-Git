using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public string sceneToLoad;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetVolume (float volume)
    {
        audioMixer.SetFloat("volume",volume);
    }

    float volume;
    /*void SaveSliderValue()
    {
        PlayerPrefs.SetFloat("SliderVolumeLevel", volume);
    }*/

    public void ReturnToGame()
    {
        UIController.UICanvas.settingsMenu.SetActive(false);
        UIController.UICanvas.pauseScreen.SetActive(true);
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
