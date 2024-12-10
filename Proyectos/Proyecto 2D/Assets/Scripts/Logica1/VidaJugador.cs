using UnityEngine;

public class VidaJugador : MonoBehaviour
{
    public int cantidadDeVida;
    
    public void tomarDamage(int damage){
        cantidadDeVida -= damage;

        if(cantidadDeVida <= 0)
        {
            Destroy(gameObject);
        }
    }
}
