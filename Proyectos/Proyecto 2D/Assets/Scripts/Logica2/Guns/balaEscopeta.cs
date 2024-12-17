using UnityEngine;

public class balaEscopeta : MonoBehaviour
{
    public float velocidad = 10f;
    public int damage;
    private Vector2 direction;


    // Configurar la dirección de la bala
    public void SetDirection(Vector2 newDirection)
    {
        direction = newDirection; // Establece la dirección de la bala
    }

    // Update es llamado cada frame
    void Update()
    {
        // Mover la bala
        transform.Translate(direction * velocidad * Time.deltaTime);
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
    } 
}
