using UnityEngine;

public class autoDestroy : MonoBehaviour
{
    public float tiempo_vida;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, tiempo_vida);
    }
}
