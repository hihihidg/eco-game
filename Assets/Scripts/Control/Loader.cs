using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Loader : MonoBehaviour
{
    public string[] sceneNames;
    public float transitionTime = 1f;
    public CanvasGroup transitionCanvas;

    private bool transitioning = false;

    public void LoadSceneWithDelay(int sceneIndex)
    {
        // Check if the scene index is valid and not already transitioning
        if (sceneIndex >= 0 && sceneIndex < sceneNames.Length && !transitioning)
        {
            StartCoroutine(LoadSceneCoroutine(sceneIndex));
        }
        else
        {
            Debug.LogError("Invalid scene index or already transitioning!");
        }
    }

    IEnumerator LoadSceneCoroutine(int sceneIndex)
    {
        transitioning = true;

        // Set initial alpha to fully transparent
        transitionCanvas.alpha = 0f;

        // Fade in
        float t = 0f;
        while (t < transitionTime)
        {
            t += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0.2f, t / transitionTime);
            transitionCanvas.alpha = alpha;
            yield return null;
        }

        // Load scene by name
        SceneManager.LoadScene(sceneNames[sceneIndex]);

        // Reset alpha to fully transparent
        transitionCanvas.alpha = 0.2f;

        transitioning = false;
    }
}