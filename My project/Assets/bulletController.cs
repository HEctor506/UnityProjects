using UnityEngine;

public class bulletController : MonoBehaviour
{
    public Rigidbody2D rb;
    public int speed;
    public GameObject explosion;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb.linearVelocity = Vector2.up * speed;
        
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy"){ //si colisiona con el enemigo
            ScoreManager.score +=10;
            Destroy(collision.gameObject); //destruyo el enemigo
            playExplosion();
        }
    }

    private void playExplosion()
    {
        GameObject e = Instantiate(explosion) as GameObject;
        e.transform.position = transform.position;

        Destroy(e, 0.35f);
    }


}
