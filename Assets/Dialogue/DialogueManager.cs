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

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        // Debug.Log("Starting conversation with person named" + dialogue.name);

        animator.SetBool("IsOpen", true);

        nameText.text = dialogue.name;

        sentences.Clear();

        foreach(string sentence in dialogue.sentences)
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
        dialogueText.text = string.Empty;

        StartCoroutine(TypeLine());
        //string sentence = sentences.Dequeue();
        //dialogueText.text = sentence;
    }

    IEnumerator TypeLine()
    {
        //Type characters one by one
        foreach(char c in sentences.Dequeue().ToCharArray())
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void EndDialogue()
    { 
        animator.SetBool("IsOpen", false);
    }
}
