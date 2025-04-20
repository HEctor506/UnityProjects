using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Diagnostics;

public class SceneLoadManager : MonoBehaviour
{
    public CanvasGroup studioCanvas;
    public CanvasGroup loadbarCanvas;
    public CanvasGroup fadePanel;
    public Slider loadingSlider;

    public float studioDuration = 2f;
    public float fadeDuration = 1f;
    public string nextSceneName;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(LoadSequence());
        
    }

    IEnumerator LoadSequence()
    {
        // Ocultar LoadbarCanvas al inicio
        loadbarCanvas.alpha = 0;
        loadbarCanvas.interactable = false;
        loadbarCanvas.blocksRaycasts = false;

        // Asegurarse de que el panel negro esté transparente
        fadePanel.alpha = 0;

        yield return new WaitForSeconds(studioDuration);

        // Fade a negro
        yield return StartCoroutine(FadeCanvasGroup(fadePanel, 0, 1, fadeDuration));

        // Cambiar canvas
        studioCanvas.alpha = 0;
        loadbarCanvas.alpha = 1;
        loadbarCanvas.interactable = true;
        loadbarCanvas.blocksRaycasts = true;

        // Fade desde negro a visible
        yield return StartCoroutine(FadeCanvasGroup(fadePanel, 1, 0, fadeDuration));

        // Barra de carga
        float progress = 0f;
        while (progress < 1f)
        {
            progress += Time.deltaTime / 2f;
            loadingSlider.value = progress;
            yield return null;
        }

        SceneManager.LoadScene(nextSceneName);
    }


    IEnumerator FadeCanvasGroup(CanvasGroup cg, float start, float end, float duration)
    {
        float elapsed = 0f;
        while(elapsed < duration)
        {
            elapsed += Time.deltaTime;
            cg.alpha = Mathf.Lerp(start, end, elapsed / duration);
            yield return null;
        }
        cg.alpha = end;

        //Ajustar interactuabilidad
        cg.interactable = end == 1;
        cg.blocksRaycasts = end == 1;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
