using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public void DoInteraction(){
        // has been picked up
        // make it disappear
        gameObject.SetActive(false);
        //GetComponent<DialogueTrigger>().TriggerDialogue();

    }
}