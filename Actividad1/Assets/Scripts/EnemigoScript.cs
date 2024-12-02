using UnityEngine;

public class EnemigoScript : MonoBehaviour
{

    [SerializeField] private float vida;
    [SerializeField] private GameObject efectoMuerte;
    
    public void TomarDanger(float danger)
    {
        vida-= danger;
        if (vida <= 0)
        {
            Muerte();
        }
    }

    private void Muerte()
    {
        Instantiate(efectoMuerte, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
