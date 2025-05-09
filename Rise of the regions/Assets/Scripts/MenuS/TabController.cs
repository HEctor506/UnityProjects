using UnityEngine;
using UnityEngine.UI;

public class TabController : MonoBehaviour
{
    public Image[] tabImages; //Gray them out and highlight the ones we selected
    public GameObject[] pages; //activate pages
    
    void Start()
    {
        ActivateTab(0);   
    }

    public void ActivateTab(int tabNumber)
    {
        for(int i=0; i< pages.Length; i++)
        {
            pages[i].SetActive(false);
            tabImages[i].color = Color.gray;
        }
        pages[tabNumber].SetActive(true);
        tabImages[tabNumber].color = Color.white;
    }

}
