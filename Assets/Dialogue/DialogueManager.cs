using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{

    public Text nameText;
    public Text dialogueText;
    public float textSpeed;

    public Animator animator;

    private Queue<string> sentences;
    private Queue<string> names;

    //private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
        names = new Queue<string>();
        //player = GameObject.FindGameObjectWithTag("Player");
    }

    public void StartDialogue(Dialogue dialogue)
    {
        // Debug.Log("Starting conversation with person named" + dialogue.name);

        PlayerMovement.canMove = false;

        animator.SetBool("IsOpen", true);

        sentences.Clear();
        names.Clear();

        foreach (string name in dialogue.names)
        {
            names.Enqueue(name);
        }
        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        string name = names.Dequeue();
        nameText.text = name;
        string sentence = sentences.Dequeue();       
        StopAllCoroutines();
        StartCoroutine(TypeLine(sentence));

        //string sentence = sentences.Dequeue();
        //dialogueText.text = sentence;
    }

    IEnumerator TypeLine(string sentence)
    {
        dialogueText.text = "";
        //Type characters one by one
        foreach(char c in sentence.ToCharArray())
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void EndDialogue()
    { 
        animator.SetBool("IsOpen", false);
        PlayerMovement.canMove = true;
    }
}
