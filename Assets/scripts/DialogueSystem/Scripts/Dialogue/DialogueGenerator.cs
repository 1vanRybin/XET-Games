using UnityEngine;
using System.Collections;
using System.IO;
using System.Xml;

public class DialogueGenerator : MonoBehaviour {

	public string fileName = "Example"; // имя генерируемого файла (без разрешения)
	public string folder = "Russian"; // подпапка в Resources, для записи
	public DialogueNode[] node;

	[System.Serializable] public struct DialogueNode
	{
		public string npcText;
		public string Teller;
		public string tellerImage;
		public string playerImage;
		public PlayerAnswer[] playerAnswer;
	}

	[System.Serializable] public struct PlayerAnswer
	{
		public string text;
		[Tooltip("Этот ответ закрывает окно диалога?")]
		public bool exit;

		[Tooltip("Переход на другой узел диалога.")]
		public int toNode;

		[Tooltip("Строка появится в диалоге, если прогресс квеста соответствует данному значению.")]
		public int questValue;

		[Tooltip("Строка появится в диалоге, если прогресс квеста больше или равно этому значению. [значение от '1']")]
		public int questValueGreater;

		[Tooltip("Имя квеста с которым взаимодействует данный ответ.")]
		public string questName;

		[Tooltip("Установить текущее значение квеста, который запрашивает 'questValue'")]
		public int setValue;
	}

	public void Generate()
	{
		if(node.Length == 0) return;

		string path = Application.dataPath + "/Resources/" + folder + "/" + fileName + ".xml";

		XmlNode userNode;
		XmlElement element;
		XmlAttribute attribute;

		XmlDocument xmlDoc = new XmlDocument();
		XmlNode rootNode = xmlDoc.CreateElement("dialogue");
		xmlDoc.AppendChild(rootNode);

		for(int j = 0; j < node.Length; j++)
		{
			userNode = xmlDoc.CreateElement("node");
			attribute = xmlDoc.CreateAttribute("id");
			attribute.Value = j.ToString();
			userNode.Attributes.Append(attribute);
			attribute = xmlDoc.CreateAttribute("npc");
			attribute.Value = node[j].npcText;
			userNode.Attributes.Append(attribute);
			attribute = xmlDoc.CreateAttribute("teller");
			attribute.Value = node[j].Teller;
			userNode.Attributes.Append(attribute);
			attribute = xmlDoc.CreateAttribute("tellerImage");
			attribute.Value = node[j].tellerImage;
			userNode.Attributes.Append(attribute);
			attribute = xmlDoc.CreateAttribute("playerImage");
			attribute.Value = node[j].playerImage;
			userNode.Attributes.Append(attribute);

			for(int i = 0; i < node[j].playerAnswer.Length; i++)
			{
				element = xmlDoc.CreateElement("answer");
				element.SetAttribute("text", node[j].playerAnswer[i].text);

				if(node[j].playerAnswer[i].exit)
				{
					element.SetAttribute("exit", node[j].playerAnswer[i].exit.ToString());

					if(node[j].playerAnswer[i].setValue != 0 && node[j].playerAnswer[i].questName.Trim().Length > 0)
					{
						element.SetAttribute("set", node[j].playerAnswer[i].setValue.ToString());
						element.SetAttribute("quest", node[j].playerAnswer[i].questName);
					}
				}
				else
				{
					element.SetAttribute("node", node[j].playerAnswer[i].toNode.ToString());

					if(node[j].playerAnswer[i].setValue != 0 && node[j].playerAnswer[i].questName.Trim().Length > 0)
					{
						element.SetAttribute("set", node[j].playerAnswer[i].setValue.ToString());
					}

					if(node[j].playerAnswer[i].questValueGreater >= 1 && node[j].playerAnswer[i].questName.Trim().Length > 0)
					{
						element.SetAttribute("greater", node[j].playerAnswer[i].questValueGreater.ToString());
						element.SetAttribute("quest", node[j].playerAnswer[i].questName);
					}
					else if(node[j].playerAnswer[i].questValue >= 0 && node[j].playerAnswer[i].questName.Trim().Length > 0)
					{
						element.SetAttribute("value", node[j].playerAnswer[i].questValue.ToString());
						element.SetAttribute("quest", node[j].playerAnswer[i].questName);
					}
				}

				userNode.AppendChild(element);
			}

			rootNode.AppendChild(userNode);
		}

		xmlDoc.Save(path);
		Debug.Log(this + " создан XML файл диалога [ " + fileName + " ] по адресу: " + path);
	}
}