
using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Xml;
using System.IO;
using TMPro;

public class DialogueManager : MonoBehaviour
{
	//TODO  Добавить в дилаоги Teller(Имя персонажа), charStatus (Имя спрайта для отображения эмоции) 

	public Image charactere;
	public ScrollRect scrollRect;
	public ButtonComponent[] buttons; // первый элемент списка, всегда будет использоваться для вывода текста NPC, остальные элементы для ответов, соответственно, общее их количество должно быть достаточным
	public string folder = "Russian"; // подпапка в Resources, для чтения
	public int offset = 10;

	private string fileName, lastName;
	private List<Dialogue> node;
	private List<Sprite> charactereIm = new ();
	private Dialogue dialogue;
	private Answer answer;
	private float curY, height;
	private static DialogueManager _internal;
	private int id;
	private static bool _active;

	struct Dialogue
	{
		public int id;
		public string npcText;
		public List<Answer> answer;
		//public CharName 
	}


	struct Answer
	{
		public string text, questName;
		public int toNode, questValue, questValueGreater, setValue;
		public bool exit;
	}

	public static void DialogueStart(string val)
	{
		if(val == string.Empty) return;
		_internal.DialogueStart_internal(val);
	}

	void DialogueStart_internal(string val)
	{
		fileName = val;
		Load();
	}

	public static bool isActive
	{
		get{ return _active; }
	}

	void Awake()
	{
		_internal = this;
		CloseWindow();
	}

	void Load()
	{
		if(lastName == fileName) // проверка, чтобы не загружать уже загруженный файл
		{
			BuildDialogue(0);
			return;
		}

		node = new List<Dialogue>();

		try // чтение элементов XML и загрузка значений атрибутов в массивы
		{
			TextAsset binary = Resources.Load<TextAsset>(folder + "/" + fileName);
			XmlTextReader reader = new XmlTextReader(new StringReader(binary.text));

			int index = 0;
			while(reader.Read())
			{
				if(reader.IsStartElement("node"))
				{
					dialogue = new Dialogue();
					dialogue.answer = new List<Answer>();
					dialogue.npcText = reader.GetAttribute("npc");
					dialogue.id = GetINT(reader.GetAttribute("id"));
					charactereIm.Add(Resources.Load<Sprite>(reader.GetAttribute("charName")));
					node.Add(dialogue);

					XmlReader inner = reader.ReadSubtree();
					while(inner.ReadToFollowing("answer"))
					{
						answer = new Answer();
						answer.text = reader.GetAttribute("text");
						answer.toNode = GetINT(reader.GetAttribute("node"));
						answer.exit = GetBOOL(reader.GetAttribute("exit"));
						answer.setValue = GetINT(reader.GetAttribute("set"));
						answer.questValue = GetINT(reader.GetAttribute("value"));
						answer.questValueGreater = GetINT(reader.GetAttribute("greater"));
						answer.questName = reader.GetAttribute("quest");
						node[index].answer.Add(answer);
					}
					inner.Close();

					index++;
				}
			}

			lastName = fileName;
			reader.Close();
		}
		catch(System.Exception error)
		{
			Debug.Log(this + " ошибка чтения файла диалога: " + fileName + ".xml | Error: " + error.Message);
			CloseWindow();
			lastName = string.Empty;
		}

		BuildDialogue(0);
	}

	void AddToList(bool exit, int toNode, string text, int setValue, string questName, bool isActive)
	{
		buttons[id].text.text = text;
		buttons[id].rect.sizeDelta = id == 0 ? new Vector2(buttons[id].rect.sizeDelta.x, buttons[id].rect.sizeDelta.y) : // высота панельки с текстом 
			                                   new Vector2(buttons[id].rect.sizeDelta.x, buttons[id].text.preferredHeight + offset);
		
		buttons[id].button.interactable = isActive;

		if (id != 0)
			height = buttons[id].rect.sizeDelta.y;

		buttons[id].rect.anchoredPosition = new Vector2(0, -height/2 - curY);

		if(exit)
		{
			SetExitDialogue(buttons[id].button);
		}
		else
		{
			SetNextNode(buttons[id].button, toNode);
		}
		
		if(setValue != 0) SetQuestStatus(buttons[id].button, setValue, questName);

		id++;
		
		curY += height + offset;

		height = 0;
		
		RectContent();
	}

	void RectContent()
	{
		scrollRect.content.sizeDelta = new Vector2(scrollRect.content.sizeDelta.x, curY);
		scrollRect.content.anchoredPosition = Vector2.zero;
	}

	void ClearDialogue()
	{
		id = 0;
		curY = offset;
		foreach(ButtonComponent b in buttons)
		{
			b.text.text = string.Empty;
			//b.rect.sizeDelta = new Vector2(b.rect.sizeDelta.x, b.rect.sizeDelta.y);
			b.rect.anchoredPosition = new Vector2(b.rect.anchoredPosition.x, 0);
			b.button.onClick.RemoveAllListeners();
		}
		RectContent();
	}

	void SetQuestStatus(Button button, int i, string questName) // событие, для управлением статуса, текущего квеста
	{
		string t = questName + "|" + i; // склейка имени квеста и значения, которое ему назначено
		button.onClick.AddListener(() => QuestStatus(t));
	}

	void SetNextNode(Button button, int i) // событие, для перенаправления на другой узел диалога
	{
		if (i == 0) return;
		button.onClick.AddListener(() => BuildDialogue(i));
	}

	void SetExitDialogue(Button button) // событие, для выхода из диалога
	{
		button.onClick.AddListener(() => CloseWindow());
	}

	void QuestStatus(string s) // меняем статус квеста
	{
		string[] t = s.Split(new char[]{'|'});
		QuestManager.SetQuestStatus(t[0], GetINT(t[1]));
	}

	void CloseWindow() // закрываем окно диалога
	{
		_active = false;
		scrollRect.gameObject.SetActive(false);
		buttons[0].gameObject.SetActive(false);
	}

	void ShowWindow() // показываем окно диалога
	{
		buttons[0].gameObject.SetActive(true);
		_active = true;
	}

	int FindNodeByID(int i)
	{
		int j = 0;
		foreach(Dialogue d in node)
		{
			if(d.id == i) return j;
			j++;
		}

		return -1;
	}

	void BuildDialogue(int current)
	{
		ClearDialogue();

		int j = FindNodeByID(current);

		if(j < 0)
		{
			Debug.LogError(this + " в диалоге [ " + fileName + ".xml ] отсутствует или указан неверно идентификатор узла.");
			return;
		}

		charactere.sprite = charactereIm[j];
		AddToList(false, 0, node[j].npcText, 0, string.Empty, true); // добавление текста NPC

		for(int i = 0; i < node[j].answer.Count; i++)
		{
			int value = QuestManager.GetCurrentValue(node[j].answer[i].questName);

			// фильтр ответов, относительно текущего статуса квеста
			if(value >= node[j].answer[i].questValueGreater && node[j].answer[i].questValueGreater != 0 || 
				node[j].answer[i].questValue == value && node[j].answer[i].questValueGreater == 0 || 
				node[j].answer[i].questName == null)
			{
				AddToList(node[j].answer[i].exit, node[j].answer[i].toNode, node[j].answer[i].text, node[j].answer[i].setValue, node[j].answer[i].questName, true); // текст игрока
			}
		}

		EventSystem.current.SetSelectedGameObject(scrollRect.gameObject); // выбор окна диалога как активного, чтобы снять выделение с кнопок диалога
		ShowWindow();
	}

	int GetINT(string text)
	{
		int value;
		if(int.TryParse(text, out value)) return value;
		return 0;
	}

	bool GetBOOL(string text)
	{
		bool value;
		if(bool.TryParse(text, out value)) return value;
		return false;
	}
}