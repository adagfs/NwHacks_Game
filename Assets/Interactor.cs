using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
	GameObject currentObj = null;
    public Animator animator;

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Interactable"))
		{
			currentObj = other.gameObject;
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.CompareTag("Interactable"))
		{
			if (other.gameObject == currentObj)
			{
				currentObj = null;
			}
		}
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetButtonDown("Interact") && currentObj)
		{
			// change character to have umbrella 
			currentObj.SendMessage("DoInteraction");
            animator.SetBool("HasUmbrella", true);
		}

	}
}