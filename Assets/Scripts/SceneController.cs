using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    // Метод для загрузки главной сцены
    public void OpenMainScene()
    {
        StartCoroutine(LoadSceneByIndexAsync(0));
    }

    // Метод для аддитивной загрузки игровой сцены
    public void OpenGameScene()
    {
        StartCoroutine(LoadAdditiveSceneByIndex(1));
    }

    private IEnumerator LoadSceneByIndexAsync(int sceneBuildIndex)
    {
        yield return SceneManager.LoadSceneAsync(sceneBuildIndex);
    }

    private IEnumerator LoadAdditiveSceneByIndex(int sceneBuildIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneBuildIndex, LoadSceneMode.Additive);
        while (!operation.isDone)
        {
            yield return null;
        }
    }
}
