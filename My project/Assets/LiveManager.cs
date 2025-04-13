using TMPro;
using UnityEngine;

public class LiveManager : MonoBehaviour
{
    public TextMeshProUGUI lives_textHolder;
    public static int lives_count = 5;

    [Header("Panel")]
    public GameObject gameOver_panel;
    public TextMeshProUGUI outOf_textHolder;
    public GameObject game_panel;
    

    // Update is called once per frame
    void Update()
    {
        lives_textHolder.text = lives_count.ToString();
        
        if(lives_count == 0)
        {
            Time.timeScale = 0;
            outOf_textHolder.text = "Shoot, you are out of lives";
            gameOver_panel.SetActive(true);
            game_panel.SetActive(false);

        }
    }
}
