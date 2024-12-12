using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class restartGame : MonoBehaviour
{
    public void restarGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
