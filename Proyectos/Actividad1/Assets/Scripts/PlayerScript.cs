using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
        moveHorizontal = Input.GetAxis("Horizontal") * velocidadMovimiento;
        
        animator.SetFloat("Horizontal", Mathf.Abs(moveHorizontal));

        if(Input.GetButtonDown("Jump")) 
        {
            salto = true;
        } 
        gameOver();
        
    }

    public void FixedUpdate()
    {
        enSuelo = Physics2D.OverlapBox(controladorSuelo.position, dimensionesCaja, 0f, queEsSuelo);
        animator.SetBool("enSuelo",enSuelo);
        //mover 
        mover(moveHorizontal * Time.fixedDeltaTime, salto);

        salto  = false;
    }

    public void mover(float mover, bool saltar)
    {
        Vector3 velocidadObjectivo = new Vector2(mover, rb2D.linearVelocity.y);
        rb2D.linearVelocity = Vector3.SmoothDamp(rb2D.linearVelocity, velocidadObjectivo,
                            ref velocidad, suavizadoMovimiento); 

        if(mover > 0 && !mirandoDerecha){
            Girar();

        }else if(mover < 0 && mirandoDerecha){
            Girar();
        }

        if(enSuelo && saltar)
        {
            enSuelo = false;
            rb2D.AddForce(new Vector2(0f, fuerzaSalto));
            
        }
    }

    private void Girar()
    {
        mirandoDerecha = !mirandoDerecha;
        // Vector3 escala = transform.localScale;
        // escala.x *= -1;
        // transform.localScale = escala; 
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

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(controladorSuelo.position, dimensionesCaja);

    }
}
