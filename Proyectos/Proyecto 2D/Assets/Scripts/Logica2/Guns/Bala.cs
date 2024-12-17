using Unity.VisualScripting;
using UnityEngine;

public class Bala : MonoBehaviour
{
    public float velocidad;
    public int damage;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Time.deltaTime * velocidad * Vector2.right);
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //esto es lo que la bala hace cuando colisiona con un objeto con el script de vida
        if(other.TryGetComponent(out vidaEnemigo vidaEnemigo))
        {
            vidaEnemigo.tomarDamage(damage);
            //cuando la bala colisione con un gameObject con vida, la bala se destruye
            Destroy(gameObject);
        }

        else if(other.TryGetComponent(out caja caja)){
            caja.Destruir();   
        }
    } 

}
