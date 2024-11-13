using System;
using UnityEngine;

namespace DefaultNamespace
{
	[RequireComponent(typeof(PositionSaver))]
	public class ReplayMover : MonoBehaviour
	{
		private PositionSaver _save;

		private int _index;
		private PositionSaver.Data _prev;
		private float _duration;

		private void Start()
		{
			////todo comment: зачем нужны эти проверки?
			//Эта проверка подтверждает, что компонент PositionSaver существует в сцене. И что есть записи в Records
			if (!TryGetComponent(out _save) || _save.Records.Count == 0)
			{
				Debug.LogError("Records incorrect value", this);
				//todo comment: Для чего выключается этот компонент?
				//Чтобы предотвратить выполнение кода, который может привести к проблемам
				enabled = false;
			}
		}

		private void Update()
		{
			var curr = _save.Records[_index];
			//todo comment: Что проверяет это условие (с какой целью)? 
			//Условие Time.time > curr.Time проверяет, прошло ли достаточное время для перехода к следующему элементу списка
			if (Time.time > curr.Time)
			{
				_prev = curr;
				_index++;
				//todo comment: Для чего нужна эта проверка?
				//Проверка _index >= _save.Records.Count проверяет, находится ли индекс _index вне пределов списка записей
				if (_index >= _save.Records.Count)
				{
					enabled = false;
					Debug.Log($"<b>{name}</b> finished", this);
				}
			}
			//todo comment: Для чего производятся эти вычисления (как в дальнейшем они применяются)?
			//Эти вычисления используются для интерполяции положения объекта между двумя последовательными записями в списке Records
			var delta = (Time.time - _prev.Time) / (curr.Time - _prev.Time);
			//todo comment: Зачем нужна эта проверка?
			//Проверка float.IsNaN(delta) проверяет, не стало ли значение delta (отношение времени) нечисленным (NaN)
			if (float.IsNaN(delta)) delta = 0f;
			//todo comment: Опишите, что происходит в этой строчке так подробно, насколько это возможно
			//Эта строка выполняет интерполяцию положения объекта между двумя точками, основываясь на времени, прошедшем с момента последней записи до текущего момента времени
			transform.position = Vector3.Lerp(_prev.Position, curr.Position, delta);
		}
	}
}