using UnityEngine;

public class Gates : MonoBehaviour
{
    // Переменная для хранения текущего игрового счета
    public int score = 0;

    // Метод, вызываемый при столкновении
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision detected");
        // Проверяем, что объект, с которым произошло столкновение, является мячом
        if (other.CompareTag("Ball")) // Предположим, что у мяча установлен тег "Ball"
        {
            // Уничтожаем мяч
            Destroy(other.gameObject);

            // Выводим в консоль текущий игровой счет
            Debug.Log("Current Score: " + score);


            score++;
        }
    }
}
