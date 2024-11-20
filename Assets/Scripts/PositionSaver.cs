using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace DefaultNamespace
{
	[Serializable]
	public class PositionSaver : MonoBehaviour
	{
        [System.Serializable]
        public struct Data
		{
			public Vector3 Position;
			public float Time;
		}

        [Tooltip("Use context menu option ‘Create File’ to populate this field.")]
        [SerializeField,ReadOnly]
        private TextAsset _json;

		[SerializeField]
        public List<Data> Records { get; private set; }

		private void Awake()
		{
			//todo comment: Что будет, если в теле этого условия не сделать выход из метода?
			//Если не выйти из метода, то возникнет исключение NullReferenceException, так как попытка обращения к несуществующему объекту _json.
			if (_json == null)
			{
				gameObject.SetActive(false);
				Debug.LogError("Please, create TextAsset and add in field _json");
				return;
			}
			
			JsonUtility.FromJsonOverwrite(_json.text, this);
			//todo comment: Для чего нужна эта проверка (что она позволяет избежать)?
			//Исключений, возникающих при попытке работать с несуществующей или пустой коллекцией Records
			if (Records == null)
				Records = new List<Data>(10);
		}

		private void OnDrawGizmos()
		{
			//todo comment: Зачем нужны эти проверки (что они позволляют избежать)?
			//Эта проверка необходима для предотвращения попыток рисования при отсутствии данных в коллекции Records, что могло бы привести к ошибкам или лишним вычислениям.
			if (Records == null || Records.Count == 0) return;
			var data = Records;
			var prev = data[0].Position;
			Gizmos.color = Color.green;
			Gizmos.DrawWireSphere(prev, 0.3f);
			//todo comment: Почему итерация начинается не с нулевого элемента?
			//Если начать с нулевой точки, линия пройдет через одну точку дважды
			for (int i = 1; i < data.Count; i++)
			{
				var curr = data[i].Position;
				Gizmos.DrawWireSphere(curr, 0.3f);
				Gizmos.DrawLine(prev, curr);
				prev = curr;
			}
		}
		
#if UNITY_EDITOR
		[ContextMenu("Create File")]
		private void CreateFile()
		{
			//todo comment: Что происходит в этой строке?
			//Создается файл с заданным именем в пути Application.dataPath, который затем открывается для записи
			var stream = File.Create(Path.Combine(Application.dataPath, "Path.txt"));
			//todo comment: Подумайте для чего нужна эта строка? (а потом проверьте догадку, закомментировав)
			//Напоминает разработчику о необходимости закрыть файловый поток, чтобы избежать возможных проблем с утечкой ресурсов.
			stream.Dispose();
			UnityEditor.AssetDatabase.Refresh();
			//В Unity можно искать объекты по их типу, для этого используется префикс "t:"
			//После нахождения, Юнити возвращает массив гуидов (которые в мета-файлах задаются, например)
			var guids = UnityEditor.AssetDatabase.FindAssets("t:TextAsset");
			foreach (var guid in guids)
			{
				//Этой командой можно получить путь к ассету через его гуид
				var path = UnityEditor.AssetDatabase.GUIDToAssetPath(guid);
				//Этой командой можно загрузить сам ассет
				var asset = UnityEditor.AssetDatabase.LoadAssetAtPath<TextAsset>(path);
				//todo comment: Для чего нужны эти проверки?
				//Эти проверки необходимы для предотвращения обработки неправильных данных или ошибок, связанных с отсутствием нужных ресурсов.
				if(asset != null && asset.name == "Path")
				{
					_json = asset;
					UnityEditor.EditorUtility.SetDirty(this);
					UnityEditor.AssetDatabase.SaveAssets();
					UnityEditor.AssetDatabase.Refresh();
					//todo comment: Почему мы здесь выходим, а не продолжаем итерироваться?
					//Мы прерываем цикл, чтобы остановить поиск после нахождения нужного файла, чтобы не продолжать перебор объектов, когда нужный файл уже найден.
					return;
				}
			}
		}

		private void OnDestroy()
		{
			// Если _json не пуст, сериализуем Records в JSON и сохраняем в этот TextAsset
			if (_json != null && Records != null && Records.Count > 0)
			{
                // Сериализуем объект, включая список Records, в строку JSON
                string json = JsonUtility.ToJson(this, true);

                // Получаем путь к файлу, который связан с _json
                string filePath = UnityEditor.AssetDatabase.GetAssetPath(_json);

                // Записываем строку JSON в файл
                File.WriteAllText(filePath, json);

                // Обновляем AssetDatabase, чтобы изменения вступили в силу
                UnityEditor.AssetDatabase.SaveAssets();
				UnityEditor.AssetDatabase.Refresh();

				Debug.Log($"Records have been serialized and saved{filePath}");
			}
			else if (_json = null)
			{
				Debug.LogWarning("_json is null. Unable to serialize data.");
			}
			else 
			{
                Debug.LogWarning("No records. Unable to serialize data.");
            }
        }
#endif
	}
}