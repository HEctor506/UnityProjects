using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI score_textHoler;
    public static int score = 0;
    public TextMeshProUGUI final_score;

    // Update is called once per frame
    void Update()
    {
        score_textHoler.text = score.ToString();
        final_score.text = score.ToString();
    }
}
