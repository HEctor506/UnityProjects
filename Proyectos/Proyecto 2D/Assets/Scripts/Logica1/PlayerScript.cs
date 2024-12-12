using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public Rigidbody2D rb2D;
    private float moveHorizontal;
    public float velocidadMovimiento;
    [Range(0, 0.3f)] public float suavizadoMovimiento;
    private Vector3 velocidad = Vector3.zero;
    private bool mirandoDerecha = true;

    [Header("Salto")]

    public float fuerzaSalto;
    public LayerMask queEsSuelo; //identifcar las superficies
    public Transform controladorSuelo;
    public Vector3 dimensionesCaja; //nos da la informacion si estamos sobre el suelo o no
    public bool enSuelo; //si estamos en el suelo
    public bool salto = false; //si es que podemos saltar
    [SerializeField] private int saltosExtraRestantes;
    [SerializeField] private int saltosExtra; 

    [Header("Pasar muros")]

    private Transform distanciaJugador;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        distanciaJugador = GetComponent<Transform>();   
    }

    // Update is called once per frame
    void Update()
    {
        moveHorizontal = Input.GetAxis("Horizontal") * velocidadMovimiento;
        
        if(enSuelo){
            saltosExtraRestantes = saltosExtra;
        }

        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            salto = true;
        } 
        pasoElMuero();
        
    }

    public void FixedUpdate()
    {
        enSuelo = Physics2D.OverlapBox(controladorSuelo.position, dimensionesCaja, 0f, queEsSuelo);
        //mover 
        mover(moveHorizontal * Time.fixedDeltaTime, salto);

        salto  = false;
    }
    public void mover(float mover, bool saltar)
    {
        // Movimiento horizontal
        Vector3 velocidadObjetivo = new Vector2(mover, rb2D.linearVelocity.y);
        rb2D.linearVelocity = Vector3.SmoothDamp(rb2D.linearVelocity, velocidadObjetivo,
                                            ref velocidad, suavizadoMovimiento);

        if (mover > 0 && !mirandoDerecha)
        {
            Girar();
        }
        else if (mover < 0 && mirandoDerecha)
        {
            Girar();
        }

        // Saltar
        if (saltar)
        {
            if (enSuelo)
            {
                Salto(); // Salto desde el suelo
            }
            else if (saltosExtraRestantes > 0)
            {
                Salto(); // Salto adicional
                saltosExtraRestantes--;
            }
        }
    }
    
    private void Salto()
    {
        rb2D.linearVelocity = new Vector2(rb2D.linearVelocity.x, 0f); // Resetear la velocidad vertical
        rb2D.AddForce(new Vector2(0f, fuerzaSalto), ForceMode2D.Impulse); // Añadir fuerza al salto
    }



    private void Girar()
    {
        mirandoDerecha = !mirandoDerecha;
        // Vector3 escala = transform.localScale;
        // escala.x *= -1;
        // transform.localScale = escala; 
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180, 0);
    }

    private void pasoElMuero()
    {
        float ubicacion = distanciaJugador.position.x;
        // Verifica si la posición X del GameObject es mayor a 25
        if(ubicacion > 39)
        {
            // Cambia su posición a (0, y, z), manteniendo las coordenadas actuales de Y y Z
            distanciaJugador.position = new Vector3(-39f, distanciaJugador.position.y, distanciaJugador.position.z);          

        }else if(ubicacion < -39){
            distanciaJugador.position = new Vector3(39, distanciaJugador.position.y, distanciaJugador.position.z); 
        }
    }


    public void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(controladorSuelo.position, dimensionesCaja);

    }
}
