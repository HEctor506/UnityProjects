using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLoadManager : MonoBehaviour
{
    public CanvasGroup studioCanvas;
    public CanvasGroup loadbarCanvas;
    public CanvasGroup fadePanel;
    public Slider loadingSlider;

    public float studioDuration = 2f;
    public float fadeDuration = 1f;
    public string nextSceneName;

    void Start()
    {
        StartCoroutine(LoadSequence());
    }

    IEnumerator LoadSequence()
    {
        // Ocultar barra de carga
        loadbarCanvas.alpha = 0;
        loadbarCanvas.interactable = false;
        loadbarCanvas.blocksRaycasts = false;

        // Asegurarse de que el panel negro esté transparente
        fadePanel.alpha = 0;

        yield return new WaitForSeconds(studioDuration);

        // Fade a negro
        yield return StartCoroutine(FadeCanvasGroup(fadePanel, 0, 1, fadeDuration));

        // Cambiar canvas: ocultar estudio y mostrar barra
        studioCanvas.alpha = 0;
        loadbarCanvas.alpha = 1;
        loadbarCanvas.interactable = true;
        loadbarCanvas.blocksRaycasts = true;

        // Fade desde negro a visible
        yield return StartCoroutine(FadeCanvasGroup(fadePanel, 1, 0, fadeDuration));

        // Empezar carga de escena real
        AsyncOperation operacion = SceneManager.LoadSceneAsync(nextSceneName);

        operacion.allowSceneActivation = false; // Evita que cargue automáticamente

        while (operacion.progress < 0.9f)
        {
            loadingSlider.value = Mathf.Clamp01(operacion.progress / 0.9f);
            yield return null;
        }

        // Llenar hasta el 100%
        float progreso = 0.9f;
        while (progreso < 1f)
        {
            progreso += Time.deltaTime;
            loadingSlider.value = Mathf.Clamp01(progreso);
            yield return null;
        }

        // Fade out final antes de activar la escena
        yield return StartCoroutine(FadeCanvasGroup(fadePanel, 0, 1, fadeDuration));

        operacion.allowSceneActivation = true;
    }

    IEnumerator FadeCanvasGroup(CanvasGroup cg, float start, float end, float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            cg.alpha = Mathf.Lerp(start, end, elapsed / duration);
            yield return null;
        }
        cg.alpha = end;

        cg.interactable = end == 1;
        cg.blocksRaycasts = end == 1;
    }
}
