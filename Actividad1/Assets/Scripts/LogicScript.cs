using UnityEngine;
using UnityEngine.UI;

public class LogicScript : MonoBehaviour
{
    public int playerScore = 0;
    public Text scoreText;
    public Text lifeText;

    
    public void addScore(){
        playerScore+=1;
        scoreText.text = playerScore.ToString();
    }
}
