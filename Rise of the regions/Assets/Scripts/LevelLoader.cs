using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class LevelLoader : MonoBehaviour
{
    public static LevelLoader Instancia; // Singleton

    public GameObject pantallaCarga;
    public Slider barraProgreso;
    public TextMeshProUGUI textoCarga;

    private bool animacionActiva = false;
    private CanvasGroup canvasGroup;

    void Awake()
    {
        // Implementación de singleton
        if (Instancia == null)
        {
            Instancia = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

    
        canvasGroup = pantallaCarga.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0f;
        // pantallaCarga.SetActive(false); // Asegurarse que esté desactivado al inicio
    }

    public void CargarNivel(string nombreEscena) //PARA CARGAR CUALQUIER ESCENA USAMOS ESTE METODO
    {
        StartCoroutine(CargarAsincronamente(nombreEscena));
    }

    public void CargarNivel() //PARA CARGAR LOS NIVELES USAMOS ESTE METODO
    {
        String region = MenuInicarNivel.SelectedTag;

        if(region != null){
            switch(region)
            {
                case "Costa":
                StartCoroutine(CargarAsincronamente("Costa"));
                break;
                case "Sierra":
                StartCoroutine(CargarAsincronamente("SierraNivel"));
                break;
                case "Oriente":
                StartCoroutine(CargarAsincronamente("Amazonia"));
                break;
            }
        }else{
            Debug.Log("No se ha podido leer la region");
        }
        
    }

    IEnumerator CargarAsincronamente(string nombreEscena)
    {
        // No hace falta activar la pantallaCarga aquí
        // Solo hacemos fade-in para que aparezca

        yield return StartCoroutine(FadeIn());

        if (!animacionActiva)
        {
            animacionActiva = true;
            StartCoroutine(AnimarTexto());
        }

        AsyncOperation operacion = SceneManager.LoadSceneAsync(nombreEscena);

        while (!operacion.isDone)
        {
            float progreso = Mathf.Clamp01(operacion.progress / 0.9f);
            barraProgreso.value = Mathf.Lerp(barraProgreso.value, progreso, Time.deltaTime * 5f);
            yield return null;
        }

        yield return new WaitForSecondsRealtime(0.5f);

        yield return StartCoroutine(FadeOut());
    }

    IEnumerator AnimarTexto()
    {
        string[] textos = { "Cargando", "Cargando.", "Cargando..", "Cargando..." };
        int i = 0;

        while (true)
        {
            textoCarga.text = textos[i % textos.Length];
            i++;
            yield return new WaitForSecondsRealtime(0.4f);
        }
    }

    IEnumerator FadeIn()
    {
        pantallaCarga.SetActive(true);
        float duracion = 1f;
        float t = 0;

        while (t < duracion)
        {
            t += Time.deltaTime;
            canvasGroup.alpha = t / duracion;
            yield return null;
        }

        canvasGroup.alpha = 1f;
    }

    IEnumerator FadeOut()
    {
        float duracion = 1f;
        float t = 0;

        while (t < duracion)
        {
            t += Time.deltaTime;
            canvasGroup.alpha = 1f - t / duracion;
            yield return null;
        }

        canvasGroup.alpha = 0f;
        pantallaCarga.SetActive(false);
        animacionActiva = false;

    }
}
