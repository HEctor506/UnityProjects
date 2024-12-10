using UnityEngine;

public class MovimientoEnemigo : MonoBehaviour
{
    private Rigidbody2D rb2D;
    [SerializeField] private float velocidadMovimiento;
    [SerializeField] private float distancia;
    [SerializeField] private LayerMask queEsSuelo;

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

        RaycastHit2D informacionSuelo = Physics2D.Raycast(transform.position, transform.right, distancia, queEsSuelo);

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
        Gizmos.DrawLine(transform.position, transform.position + transform.right * distancia);
    }

    private void pasoElMuero()
    {
        float ubicacion = distanciaEnemigo.position.x;
        // Verifica si la posición X del GameObject es mayor a 25
        if(ubicacion > 39)
        {
            // Cambia su posición a (0, y, z), manteniendo las coordenadas actuales de Y y Z
            distanciaEnemigo.position = new Vector3(-39f, distanciaEnemigo.position.y, distanciaEnemigo.position.z);          

        }else if(ubicacion < -39){
            distanciaEnemigo.position = new Vector3(39, distanciaEnemigo.position.y, distanciaEnemigo.position.z); 
        }
    }

}
