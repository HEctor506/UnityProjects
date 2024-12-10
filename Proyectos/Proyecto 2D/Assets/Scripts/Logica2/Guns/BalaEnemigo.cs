using UnityEngine;

public class BalaEnemigo : MonoBehaviour
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
        if(other.TryGetComponent(out VidaJugador vidaJugador))
        {
            vidaJugador.tomarDamage(damage);
            Destroy(gameObject);
        }
    } 

}
