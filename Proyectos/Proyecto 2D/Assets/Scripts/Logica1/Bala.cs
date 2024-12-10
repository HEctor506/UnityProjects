using Unity.VisualScripting;
using UnityEngine;

public class Bala : MonoBehaviour
{
    private new Rigidbody2D rigidbody;
    public float speed = 3;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //para que no colisione y rebote
        Destroy(gameObject);
        //solo conlisiona de manera invisible contra un objeto y luego pasa a ser destruida
    }

    void FixedUpdate()
    {
         //aqui usamos fixedUpdate ya que estamos usando fisicas para mover la bala
        rigidbody.MovePosition(transform.position + transform.right * speed * Time.fixedDeltaTime);
        //la posicion actual de bala + hacia donde quiero mover la bala multiplica por velocidad de movimiento y por 
        //ultimo multiplica por fixedDeltaTime para convertir estas unidades en unidades por segundo
    }
   
}
