using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Dialogue : MonoBehaviour
{
    public Text textComponent;

    public GameObject playerDialogueBox;
    public GameObject masterDialogueBox;
    public GameObject originalPlayerDialogueBox;
    public GameObject secretDialogueBox;
    //public GameObject dialogueWindow;

    public List<string> lines;
    public float textSpeed;

    private int index;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetMouseButtonDown(0))
        {
            if(UIController.UICanvas.isCutscene && index == 2 && !Cutscene.cutscene.isInMiddle && !Cutscene.cutscene.switcher.activeInHierarchy)
            {
                Cutscene.cutscene.player.enabled = true;
            }
            if(UIController.UICanvas.isCutscene && index == 14 && !Cutscene.cutscene.isInMiddle && !Cutscene.cutscene.switcher.activeInHierarchy)
                Cutscene.cutscene.gun.enabled = true;
            NextLine();
        }
    }

    // вызывается из другого метода для начало диалога
    public void StartDialogue(List<string> lines)
    {
        //dialogueWindow.SetActive(true);
        gameObject.SetActive(true);
        this.lines = lines;
        Time.timeScale = 0f;
        if(!UIController.UICanvas.isCutscene || UIController.UICanvas.isCutscene && !Cutscene.cutscene.isInMiddle && index != 3 || Cutscene.cutscene.isInMiddle)
            index = -1;
        if(!UIController.UICanvas.isCutscene)
            PlayerController.player.weapons[PlayerController.player.currentWeapon].isAvailableToShoot = false;
        NextLine();
    }

    // перед каждой репликой должны стоять три символа для обозначения говорящего: [P] или [M] для игрока и мастера соответственно
    void NextLine()
    {
        if (index < lines.Count - 1)
        {
            index++;
            if (lines[index][1] == 'P')
            {
                playerDialogueBox.SetActive(true);
                masterDialogueBox.SetActive(false);
                originalPlayerDialogueBox.SetActive(false);
                secretDialogueBox.SetActive(false);
            }
            else if(lines[index][1] == 'M')
            {
                masterDialogueBox.SetActive(true);
                playerDialogueBox.SetActive(false);
                originalPlayerDialogueBox.SetActive(false);
                secretDialogueBox.SetActive(false);
            }
            else if(lines[index][1] == 'O')
            {
                playerDialogueBox.SetActive(false);
                masterDialogueBox.SetActive(false);
                originalPlayerDialogueBox.SetActive(true);
                secretDialogueBox.SetActive(false);
            }
            else
            {
                playerDialogueBox.SetActive(false);
                masterDialogueBox.SetActive(false);
                originalPlayerDialogueBox.SetActive(false);
                secretDialogueBox.SetActive(true);
            }
            textComponent.text = lines[index].Substring(3, lines[index].Length - 3);
            Audio.instance.PlayEffects(4);
            if (UIController.UICanvas.isCutscene && index >= 3 && !Cutscene.cutscene.isInMiddle)
            {
                Cutscene.cutscene.player.sprite = Cutscene.cutscene.playerSpritesForCutscene[index];
                Cutscene.cutscene.player.SetNativeSize();
            }
        }
        else
        {
            //dialogueWindow.SetActive(false);
            Time.timeScale = 1f;
            if (!UIController.UICanvas.isCutscene)
                PlayerController.player.weapons[PlayerController.player.currentWeapon].isAvailableToShoot = true;
            else
            {
                SceneManager.LoadScene(Cutscene.cutscene.sceneToLoad);
            }
            gameObject.SetActive(false);
            lines.Clear();
        }
    }
}

//public class Dialogue : MonoBehaviour
//{
//    public Text textComponent;

//    public GameObject playerDialogueBox;
//    public GameObject masterDialogueBox;

//    // внешний массив для разных комнат(диалогов)
//    public string[] lines;
//    public float textSpeed;

//    private int index;

//    private bool isLineEnded;

//    // Start is called before the first frame update
//    void Start()
//    {

//    }

//    // Update is called once per frame
//    void Update()
//    {
//        if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetMouseButtonDown(0))
//        {
//            if (isLineEnded)
//            {
//                NextLine();
//            }
//            else
//            {
//                StopAllCoroutines();
//                textComponent.text = lines[index].Substring(3, lines[index].Length - 3);
//                isLineEnded = true;
//            }
//        }
//    }

//    // вызывается из другого метода для начало диалога
//    public void StartDialogue()
//    {
//        gameObject.SetActive(true);
//        //Time.timeScale = 0f;
//        index = -1;
//        NextLine();
//    }

//    // перед каждой репликой должны стоять три символа для обозначения говорящего: [P] или [M] для игрока и мастера соответственно
//    void NextLine()
//    {
//        if (index < lines.Length - 1)
//        {
//            index++;
//            if (lines[index][1] == 'P')
//            {
//                playerDialogueBox.SetActive(true);
//                masterDialogueBox.SetActive(false);
//            }
//            else
//            {
//                masterDialogueBox.SetActive(true);
//                playerDialogueBox.SetActive(false);
//            }
//            StartCoroutine(TypeLine());
//        }
//        else
//        {
//            gameObject.SetActive(false);
//            Time.timeScale = 1f;
//        }
//    }

//    IEnumerator TypeLine()
//    {
//        isLineEnded = false;
//        textComponent.text = string.Empty;
//        foreach (var c in lines[index].Skip(3))
//        {
//            textComponent.text += c;
//            yield return new WaitForSeconds(textSpeed);
//        }
//        isLineEnded = true;
//    }
//}
