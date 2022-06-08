using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public bool containsDialogue;
    private bool isDialogueCompleted;
    private bool isInRoom;
    public List<string> dialogueLines;
    public LayerMask layer;
    private float dialogueDelayTime = 0.8f;
    private float dialogueDelayCounter;

    public static Room room;

    private void Awake()
    {
        room = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (containsDialogue)
            isDialogueCompleted = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(dialogueDelayCounter > 0)
            dialogueDelayCounter -= Time.deltaTime;
        if (isInRoom && containsDialogue && !isDialogueCompleted && dialogueDelayCounter <= 0 && dialogueDelayCounter >= -10)
        {
            UIController.UICanvas.dialogues.GetComponent<Dialogue>().StartDialogue(dialogueLines);
            isDialogueCompleted = true;
            isInRoom = false;
            dialogueDelayCounter = -10;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            CameraController.instance.ChangeTarget(transform);
            dialogueDelayCounter = dialogueDelayTime;
            isInRoom = true;
            //if (isInRoom && containsDialogue && !isDialogueCompleted && dialogueDelayCounter <= 0)
            //{
            //    gameObject.GetComponent<Dialogue>().StartDialogue();
            //}
        }
    }

    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    if (collision.tag == "Player")
    //    {
    //        dialogueDelayCounter -= Time.deltaTime;
    //        if (containsDialogue && !isDialogueCompleted && dialogueDelayCounter <= 0)
    //        {
    //            gameObject.GetComponent<Dialogue>().StartDialogue();
    //            isDialogueCompleted =true;
    //        }
    //    }
    //}
}
