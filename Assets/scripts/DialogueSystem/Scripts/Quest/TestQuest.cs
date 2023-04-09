using UnityEngine;
using System.Collections;

public class TestQuest : MonoBehaviour { // простой тестовый квест

	private static int value;
	private static TestQuest _internal;

	public static TestQuest Internal
	{
		get{ return _internal; }
	}

	void Awake()
	{
		_internal = this;
	}

	public static int questValue // статус квеста, который запрашивает система диалога
	{
		get{ return value; }
	}

	public void QuestStatus(int val)
	{
		// меняем значение квеста
		// в зависимости от необходимости, либо добавляем (например, собираем у NPC что-то)
		// либо можно устанавливать точные значения
		value += val;

		if(value < 0)
		{
			FailedQuest();
			return;
		}

		switch(value) // ловим нужное значение и выполняем соответствующий метод
		{
		case 1:
			ActiveQuest();
			break;
		case 3:
			DoneQuest();
			break;
		case 4:
			CompleteQuest();
			break;
		}
	}

	void ActiveQuest()
	{
		// квест активен
	}

	void DoneQuest()
	{
		// квест выполнен (например, собрал 200 матрасов, но еще не говорил с заказчиком)
	}

	void CompleteQuest()
	{
		// сообщил заказчику, что всё сделано, получил награду

		value = -1; // скрываем текст связанный с этим квестом
	}

	void FailedQuest()
	{
		// квест провален (отказался от квеста или умер ключевой персонаж)

		value = -1;
	}
}
