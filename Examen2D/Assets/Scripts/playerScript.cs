using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class playerScript : MonoBehaviour
{
    public Rigidbody2D rb2D;
    private float moveHorizontal;
    public float velocidadMovimiento;
    [Range(0, 0.3f)] public float suavizadoMovimiento;
    private Vector3 velocidad = Vector3.zero;
    private bool mirandoDerecha = true;



    [Header("Animacion")]
    public Animator animator;

    [Header("Score")]
    public int playerScore = 0;
    public Text scoreText;

    public GameObject gameOverScreen;
    public Transform miTransform;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        
    }

    // Update is called once per frame
        void Update()
    {
        // Detectar entrada de teclas A y D
        if (Input.GetKey(KeyCode.A))
        {
            moveHorizontal = -velocidadMovimiento; // Movimiento hacia la izquierda
        }
        else if (Input.GetKey(KeyCode.D))
        {
            moveHorizontal = velocidadMovimiento; // Movimiento hacia la derecha
        }
        else
        {
            moveHorizontal = 0; // Detener movimiento
        }

        // Actualizar la animación
        animator.SetFloat("Horizontal", Mathf.Abs(moveHorizontal));

        // Verificar condiciones de Game Over
        // gameOver();
    }

    public void FixedUpdate()
    {
        // Llamar al método mover con la entrada horizontal
        mover(moveHorizontal * Time.fixedDeltaTime);
    }

    public void mover(float mover)
    {
        // Aplicar movimiento aditivo al Rigidbody2D
        Vector2 fuerza = new Vector2(mover, 0f);
        rb2D.AddForce(fuerza);

        // Girar el personaje según la dirección del movimiento
        if (mover > 0 && !mirandoDerecha)
        {
            Girar();
        }
        else if (mover < 0 && mirandoDerecha)
        {
            Girar();
        }
    }

    private void Girar()
    {
        // Cambiar la orientación del personaje
        mirandoDerecha = !mirandoDerecha;
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180, 0);
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Coin"){
            addScore();
            Debug.Log(other.transform.name);
            other.gameObject.GetComponent<Animator>().SetTrigger("Collected");
            Destroy(other.gameObject, 1f);
            
        }

    }

    public void addScore(){
        playerScore+=1;
        scoreText.text = playerScore.ToString();
    }

    public void restarGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    public void gameOver()
    {
        if(miTransform.position.y < -10)
        {
            gameOverScreen.SetActive(true);
        }

    }

    // private void OnCollisionEnter2D(Collision2D collision){

    //     //Verifica si el objecto con el que colisionanste es el de interes
    //     if  (collision.gameObject == this.platform)
    //     {
    //             Debug.Log("Colisión! Listo para bajar");
    //     }
    // }


}