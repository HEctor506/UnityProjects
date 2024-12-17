using UnityEngine;

public class vidaEnemigo : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;
    public GameObject prefabMuerte;

    void Start()
    {
        currentHealth = maxHealth;
    }

    
    public void tomarDamage(int damage){
        currentHealth -= damage;

        if(currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    public GameObject getPrefab(){
        return prefabMuerte;
    }

}
