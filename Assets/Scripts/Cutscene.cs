using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Cutscene : MonoBehaviour
{
    public GameObject computerOnScreen;
    public Animator anim;
    public GameObject switcher;
    public List<string> dialogue;

    public List<Sprite> playerSpritesForCutscene;
    public Image player;
    public Image gun;

    public static Cutscene cutscene;

    public string sceneToLoad;

    public bool isInMiddle;

    private void Awake()
    {
        cutscene = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(switcher.activeInHierarchy)
        {
            anim.SetBool("isOn", true);
            computerOnScreen.SetActive(true);
            gameObject.GetComponent<UIController>().dialogues.GetComponent<Dialogue>().StartDialogue(dialogue);
        }
    }
}
