using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public static UIController UICanvas;
    public Slider healthBar;
    public Text healthBarText;
    public GameObject deathScreen;
    public Text coinBarText;
    public GameObject victoryScreen;
    public Text ammoBarText;
    public Image weapon;

    public GameObject passwordScreen;
    public Text inputText;
    public GameObject badPasswordAlerte;

    public GameObject dialogues;
    public GameObject pauseScreen;
    public string mainMenu;
    public string level;
    public GameObject settingsMenu;

    public bool isCutscene;
    public bool isDeathScreen;

    private void Awake()
    {
        UICanvas = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (passwordScreen.activeInHierarchy)
        {

            if (Input.GetKeyDown(KeyCode.Return))
            {
                Computer.computer.ChangePassword(inputText.text);
                passwordScreen.SetActive(false);
                Audio.instance.PlayEffects(8);
                Time.timeScale = 1f;
            }

            if (inputText.text.Length != 0 
                && Computer.computer.PasswordQualityToHealAmount(inputText.text) < Computer.computer.healByGoodPassword)
                badPasswordAlerte.SetActive(true);
            else
                badPasswordAlerte.SetActive(false);
        }
    }

    //Main menu

    public void LoadMainMenu()
    {
        isDeathScreen = false;
        SceneManager.LoadScene(mainMenu);
    }

    //Settings menu

    public void LoadSettingsMenu()
    {
        settingsMenu.SetActive(true);
        pauseScreen.SetActive(false);
    }
    //Pause menu
    public void Resume()
    {
        LevelManager.lvlManager.PauseUnpause();
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        isDeathScreen = false;
        SceneManager.LoadScene(level);
    }
}
