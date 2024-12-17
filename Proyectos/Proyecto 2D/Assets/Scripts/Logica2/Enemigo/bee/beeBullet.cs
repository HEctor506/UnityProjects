using UnityEngine;

public class beeBullet : MonoBehaviour
{
    public float speed;
    public int damage;
    

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //esto es lo que la bala hace cuando colisiona con un objeto con el script de vida
        if(other.CompareTag("Player"))
        {
            Debug.Log("El pincho toco al jugador");
            //cuando la bala colisione con un gameObject con vida, la bala se destruye
            Destroy(gameObject);
        }
    } 
}
