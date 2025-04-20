using System.Collections;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            LoadNextLevel();
        }
        
    }

    public void LoadNextLevel()
    {
        int levelIndex = SceneManager.GetActiveScene().buildIndex +1;
        StartCoroutine(LoadLevel(levelIndex));
    }


    IEnumerator LoadLevel(int levelIndex)
    {
        //Play animation 
        transition.SetTrigger("Start");

        //wait
        yield return new WaitForSeconds(transitionTime);

        //load scene
        SceneManager.LoadScene(levelIndex);
    }


}
