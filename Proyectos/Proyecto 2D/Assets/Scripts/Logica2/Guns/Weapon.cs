using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Weapon")]
    public GameObject weaponItemPrefab;
    public float maxCooldownTime = 0.4f;  
    private float cooldownTime; 
    public float damage = 1;


    void Awake()
    {
        this.cooldownTime = this.maxCooldownTime;
    }

    public void Throw()
    {
        Instantiate(this.weaponItemPrefab, this.transform.position, Quaternion.identity); 
        Destroy(this.gameObject); //destruimos el arma que tengamos en la mano
        //Cuando tiremos el armar al suelo crearemos el prefab del arma en la posicion en la que estemos
    }



}
