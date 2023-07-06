using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SceneLoader : MonoBehaviour
{
    string Scene;
    public GameObject Warning;
    public TMP_Text Text;

    private bool isLoadingScene = false;
    private AsyncOperation sceneLoadingOperation;

    private void Update()
    {
        if (isLoadingScene && sceneLoadingOperation.progress >= 0.9f)
        {
            Debug.Log("Escena Cargada");
            Text.gameObject.SetActive(false);
            Warning.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Joystick1Button0))
            {
                ContinueToLoadedScene();
            }
        }
    }

    private System.Collections.IEnumerator LoadScene()
    {
        isLoadingScene = true;
        sceneLoadingOperation = SceneManager.LoadSceneAsync(Scene);
        sceneLoadingOperation.allowSceneActivation = false;

        while (!sceneLoadingOperation.isDone)
        {
            float progress = Mathf.Clamp01(sceneLoadingOperation.progress / 0.9f);
            PorcentajeCarga(progress * 100f);
            yield return null;
        }
    }

    public void LoadedScene(string scene)
    {
        Scene = scene;
        StartCoroutine(LoadScene());
    }
    public void ContinueToLoadedScene()
    {
        sceneLoadingOperation.allowSceneActivation = true;
    }

    private void PorcentajeCarga(float percentage)
    {
        Debug.Log("PORCENTAJE DE CARGA: " + percentage.ToString("F0") + "%");
        Text.text = "PORCENTAJE DE CARGA:\n" + percentage.ToString("F0") + "%";
    }
}



