using UnityEngine;

public class MenuPausa : MonoBehaviour
{
    [SerializeField] private GameObject menuPausaCanvas;


    public void Pausa()
    {
        Time.timeScale = 0;
        menuPausaCanvas.SetActive(true);
    }
}
