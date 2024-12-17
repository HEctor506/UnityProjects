using Unity.VisualScripting;
using UnityEngine;

public class MovimientoEnemigo : MonoBehaviour
{
    private Rigidbody2D rb2D;
    [SerializeField] private float velocidadMovimiento;
    [SerializeField] private float distancia_pared;
    [SerializeField] private float distancia_jugador;
    [SerializeField] private LayerMask queEsSuelo;
    [SerializeField] private LayerMask queEsJugador;
    [SerializeField] private Transform enFrente_jugador;

    [Header("Pasar muros")]
    private Transform distanciaEnemigo;
   
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>(); 
        distanciaEnemigo = GetComponent<Transform>(); 
    }

    // Update is called once per frame
    void Update()
    {
        rb2D.linearVelocity = new Vector2(velocidadMovimiento * transform.right.x, rb2D.linearVelocity.y);

        RaycastHit2D informacionSuelo = Physics2D.Raycast(transform.position, transform.right, distancia_pared, queEsSuelo);

        RaycastHit2D informacionJugador = Physics2D.Raycast(enFrente_jugador.position, enFrente_jugador.right, distancia_jugador, queEsJugador);

        if(informacionSuelo)
        {
            Girar();
        }   
        pasoElMuero();
    }

    private void Girar()
    {
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180, 0);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.right * distancia_pared);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(enFrente_jugador.position, enFrente_jugador.position + enFrente_jugador.right * distancia_jugador);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Player")){
            foreach(ContactPoint2D punto in other.contacts){
                if(punto.normal.y <= -0.9){
                    other.gameObject.GetComponent<PlayerScript>().Rebote();
                    vidaEnemigo vida_enemigo = gameObject.GetComponent<vidaEnemigo>();
                    Destroy(gameObject);
                    Instantiate(vida_enemigo.getPrefab(), gameObject.transform.position, Quaternion.identity);
                }
            }
        }
    }

    private void pasoElMuero()
    {
        float ubicacion = distanciaEnemigo.position.x;
        // Verifica si la posición X del GameObject es mayor a 25
        if(ubicacion > 53)
        {
            // Cambia su posición a (0, y, z), manteniendo las coordenadas actuales de Y y Z
            distanciaEnemigo.position = new Vector3(-53f, distanciaEnemigo.position.y, distanciaEnemigo.position.z);          

        }else if(ubicacion < -53){
            distanciaEnemigo.position = new Vector3(53, distanciaEnemigo.position.y, distanciaEnemigo.position.z); 
        }
    }

}
