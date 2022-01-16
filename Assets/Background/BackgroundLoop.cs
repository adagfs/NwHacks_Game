using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundLoop : MonoBehaviour {
	public GameObject[] levels;
	private Camera mainCamera;
	private Vector2 screenBounds;

	// Use this for initialization
	void Start () {
		mainCamera = gameObject.GetComponent<Camera>();
		screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));
		foreach(GameObject obj in levels) {
			loadChildrenObjects(obj);
		}
	}

	void loadChildrenObjects(GameObject obj) {
		float objectWidth = obj.GetComponent<SpriteRenderer>().bounds.size.x;
		int childrenNeeded = (int) Mathf.Ceil(screenBounds.x * 2 / objectWidth);
		GameObject clone = Instantiate(obj) as GameObject;
		for (int i = 0; i <= childrenNeeded; i++) {
			GameObject c = Instantiate(clone) as GameObject;
			c.transform.SetParent(obj.transform);
			c.transform.position = new Vector3(objectWidth * i, obj.transform.position.y, obj.transform.position.z);
			c.name = obj.name + i;
		}
		Destroy(clone);
		Destroy(obj.GetComponent<SpriteRenderer>());
	}

	void repositionChildObjects(GameObject obj) {
		Transform[] children = obj.GetComponentsInChildren<Transform>();
		if (children.Length > 1) {
			GameObject first = children[1].gameObject;
			GameObject last = children[children.Length - 1].gameObject;
			float halfObjectWidth = last.GetComponent<SpriteRenderer>().bounds.extents.x;
			
			if (transform.position.x + screenBounds.x > last.transform.position.x + halfObjectWidth) {
				first.transform.SetAsLastSibling();
				first.transform.position = new Vector3(last.transform.position.x + halfObjectWidth * 2, last.transform.position.y, last.transform.position.z);
			} else if (transform.position.x - screenBounds.x < first.transform.position.x - halfObjectWidth) {
				last.transform.SetAsFirstSibling();
				last.transform.position = new Vector3(first.transform.position.x - halfObjectWidth * 2, first.transform.position.y, first.transform.position.z);
			}
		}
	}
	
	// Update is called once per frame
	void LateUpdate () {
		foreach(GameObject obj in levels) {
			repositionChildObjects(obj);
		}
	}
}
