using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager lvlManager;
    public int currentCoinsAmount;
    public string sceneToLoad;

    public List<EnemyController> enemies;
    public GameObject door;

    public float safetyTime;
    private float safetyTimeCounter;

    public bool isPaused;

    private void Awake()
    {
        lvlManager = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        UIController.UICanvas.coinBarText.text = currentCoinsAmount.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !UIController.UICanvas.isDeathScreen)
            PauseUnpause();
        if (!UIController.UICanvas.isCutscene)
        {
            if (enemies.Count <= 0)
                door.SetActive(false);
            if (safetyTimeCounter > 0)
                safetyTimeCounter -= Time.deltaTime;
        }
    }

    public void GetCoins(int amount)
    {
        currentCoinsAmount += amount;
        UIController.UICanvas.coinBarText.text = currentCoinsAmount.ToString();
    }

    public void SpendCoins(int amount)
    {
        if (safetyTimeCounter <= 0)
        {
            currentCoinsAmount -= amount;
            if (currentCoinsAmount < 0)
                currentCoinsAmount = 0;
            UIController.UICanvas.coinBarText.text = currentCoinsAmount.ToString();
            safetyTimeCounter = safetyTime;
        }
    }

    public void PauseUnpause()
    {
        if (!isPaused)
        {
            UIController.UICanvas.pauseScreen.SetActive(true);
            isPaused = true;
            Time.timeScale = 0f;
        }
        else
        {
            UIController.UICanvas.pauseScreen.SetActive(false);
            isPaused = false;
            Time.timeScale = 1f;
        }
    }
}
