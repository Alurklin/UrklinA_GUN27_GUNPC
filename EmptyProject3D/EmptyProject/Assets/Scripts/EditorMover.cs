using UnityEngine;

namespace DefaultNamespace
{
	
	[RequireComponent(typeof(PositionSaver))]
	public class EditorMover : MonoBehaviour
	{
		private PositionSaver _save;
		private float _currentDelay;

        //todo comment: Что произойдёт, если _delay > _duration?
        //Скрипт завершит свою работу раньше, чем успеет записать все позиции, которые планировалось сохранить
        [Range(0.2f, 1.0f)]
        [SerializeField] private float _delay = 0.5f;

        [SerializeField] private float _duration = 5f;

        private void Start()
		{
            //todo comment: Почему этот поиск производится здесь, а не в начале метода Update?
            //Для оптимизации. Чтоб getcomponent вызывался только 1 раз.
            _save = GetComponent<PositionSaver>();
            _save.Records.Clear();

            if (_duration <= _delay)
            {
                _duration = _delay * 5;
            }
        }

		private void Update()
		{
			_duration -= Time.deltaTime;
			if (_duration <= 0f)
			{
				enabled = false;
				Debug.Log($"<b>{name}</b> finished", this);
				return;
			}
			
			//todo comment: Почему не написать (_delay -= Time.deltaTime;) по аналогии с полем _duration?
			/*
			Строка _currentDelay -= Time.deltaTime; эквивалентна записи (_delay -= Time.deltaTime), 
			обе выполняют уменьшение переменной на значение Time.deltaTime.
			Однако использование отдельной переменной _currentDelay позволяет лучше структурировать код и
			разделить логику задержки между сохранениями от общей длительности работы скрипта.
			*/
			_currentDelay -= Time.deltaTime;
			if (_currentDelay <= 0f)
			{
				_currentDelay = _delay;
				_save.Records.Add(new PositionSaver.Data
				{
					Position = transform.position,
					//todo comment: Для чего сохраняется значение игрового времени?
					//Записываем в какой момент времени в какой позиции находился объект
					Time = Time.time,
				});
			}
		}
	}
}