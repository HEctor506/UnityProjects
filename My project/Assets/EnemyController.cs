using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed =1;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        rb.linearVelocity = new Vector2(0f, -speed);   
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Bullet"){
            Destroy(collision.gameObject);

        }else if(collision.tag == "Player"){
            LiveManager.lives_count -=1;
            
            if(LiveManager.lives_count <= 0)
                Destroy(collision.gameObject);
        }
    }
}
