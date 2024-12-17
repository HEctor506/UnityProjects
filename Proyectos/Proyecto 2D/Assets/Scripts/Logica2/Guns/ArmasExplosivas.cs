using UnityEngine;

public class ArmasExplosivas : MonoBehaviour
{
    public enum TipoExplosivo { Granada, DinamitaTerrestre }
    public TipoExplosivo tipoExplosivo;
    public GameObject prefabExplosion;
    public bool esRodante; // Indica si puede rodar
    public float tiempoParaExplotar = 3f; // Tiempo antes de la explosión para granadas
    public LayerMask capaEnemigo; // Capa que activa la explosión (enemigos)
    public LayerMask queEsDañable; // Capa de objetos afectados por la explosión
    public Rigidbody2D rb;
    private bool explotó = false; // Asegura que solo explote una vez
    public float fuerzaDeTiro;
    public int num;

    [Header("PuntoExplosion")]
    public Transform punto;
    public float radioExplosion = 3f; // Radio de daño de la explosión

    [Header("Sonido")]
    public AudioClip explosion;

    void Start()
    {

        // if (tipoExplosivo == TipoExplosivo.Granada)
        // {
        //     esRodante = true; // La granada puede rodar
        //     Invoke(nameof(Explotar), tiempoParaExplotar); // Inicia el temporizador
        // }
        if (tipoExplosivo == TipoExplosivo.DinamitaTerrestre)
        {
            esRodante = false; // La dinamita no rueda
            // Asegúrate de que tenga un impulso inicial bajo
            if (rb == null)
                return;
            Debug.Log("algo entro");
            switch (num)
            {
                case 1: 
                     rb.AddForce(Vector2.right * fuerzaDeTiro, ForceMode2D.Impulse); 
                     break;
                case 2:
                     rb.AddForce(Vector2.left * fuerzaDeTiro, ForceMode2D.Impulse); 
                     break;
                default:
                    Debug.Log("no tiene valores");
                    break;
            }
            // Aplica la fuerza en la dirección correcta
               
        }
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Lógica para granada
        if (tipoExplosivo == TipoExplosivo.Granada)
        {
            if (capaEnemigo == (capaEnemigo | (1 << collision.gameObject.layer)))
            {
                Explotar(); // Explota al colisionar con un enemigo
            }
        }
        // Lógica para dinamita terrestre
        else if (tipoExplosivo == TipoExplosivo.DinamitaTerrestre)
        {
            if (capaEnemigo == (capaEnemigo | (1 << collision.gameObject.layer)))
            {
                Explotar(); // Explota al colisionar con un enemigo

            }
            else
            {
                // La dinamita se queda quieta tras el primer impacto con el suelo
                rb.linearVelocity = Vector2.zero; // Detiene el movimiento

                
            }
        }
    }

    void Explotar()
    {
        if (explotó) return; // Evita múltiples explosiones
        explotó = true;

        Instantiate(prefabExplosion, punto.transform.position, Quaternion.identity);
        SoundManager.instance.PlaySound(explosion);

        // ************************PARTE1*************************
        // Encuentra objetos dentro del radio de la explosión
        Collider2D[] objetosAfectados = Physics2D.OverlapCircleAll(transform.position, radioExplosion, queEsDañable);

        foreach (Collider2D objeto in objetosAfectados) //por ahora solo se ve afectado el jugador si esta cerca
        {
            if(objeto.CompareTag("Player")){
                PlayerScript player = objeto.GetComponent<PlayerScript>();
                player.currentHealth -= 10; //estar cerca de la explosion puede quitar mucha vida
            }else if(objeto.CompareTag("Enemigo")){
                vidaEnemigo vida_enemigo = objeto.GetComponent<vidaEnemigo>();
                Destroy(objeto.gameObject);
                Instantiate(vida_enemigo.getPrefab(), objeto.transform.position, Quaternion.identity);
            }
        }

        // ************************PARTE2*************************
        // Añade efectos visuales o de sonido aquí si es necesario

        Destroy(gameObject); // Destruye el explosivo después de explotar
    }

    private void OnDrawGizmosSelected()
    {
        // Dibuja el radio de explosión en el editor
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(punto.position, radioExplosion);
    }
}
