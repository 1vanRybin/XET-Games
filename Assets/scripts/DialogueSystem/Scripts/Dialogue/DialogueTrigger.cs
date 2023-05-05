using UnityEngine;
using System.Collections;

public class DialogueTrigger : MonoBehaviour {

	private bool _nearTheNpc;
	public string fileName; // указываем имя файла диалога

	private void OnTriggerEnter2D(Collider2D collision)
	{
		_nearTheNpc = true;
	}
	private void OnTriggerStay2D(Collider2D collision)
	{
		_nearTheNpc = true;
	}
	private void OnTriggerExit2D(Collider2D collision)
	{
		_nearTheNpc = false;
	}

	void Update()
	{
		if (_nearTheNpc && Input.GetKeyDown(KeyCode.E))
		{
			DialogueManager.DialogueStart(fileName);
		}
	}
}