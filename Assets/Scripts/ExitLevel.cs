using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitLevel : MonoBehaviour
{
    public Sprite allowedComp;
    public GameObject keyMessage;
    public GameObject lackOfDefeatedEnemiesText;
    private bool isInComputerZone;
    public string sceneToLoad;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(LevelManager.lvlManager.enemies.Count == 0)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = allowedComp;
        }
        if(Input.GetKeyDown(KeyCode.E) && isInComputerZone)
        {
            if(LevelManager.lvlManager.enemies.Count == 0)
            {
                SceneManager.LoadScene(sceneToLoad);
            }
            else
            {
                lackOfDefeatedEnemiesText.SetActive(true);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            keyMessage.SetActive(true);
            isInComputerZone = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            keyMessage.SetActive(false);
            lackOfDefeatedEnemiesText.SetActive(false);
            isInComputerZone=false;
        }
    }
}
