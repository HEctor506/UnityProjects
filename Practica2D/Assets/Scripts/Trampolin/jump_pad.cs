using Unity.VisualScripting;
using UnityEngine;

public class jump_pad : MonoBehaviour
{
    public float bounce = 20f; // the higher the value, the higher our player get acccelerated upwards


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * bounce, ForceMode2D.Impulse);
            //Here we access our player's rigidbody component
        }
    }
    
}
