using System;
using UnityEngine;

public class GunWeapon : Weapon
{
    public WeaponSystem weaponSystem;

    private SpriteRenderer spriteRenderer;
    public new Camera camera;
    public Transform spawner;
    public GameObject bulletPrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Configurar el sistema de armas con munición inicial
        weaponSystem = new WeaponSystem(10); // Máximo de 10 balas
    }

    // Update is called once per frame
    void Update()
    {

        HandleShooting();
        ReloadWeapon();
    }



    public void HandleShooting()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            if (weaponSystem.CanShoot())
            {
                Shoot();
            }
            else
            {
                Debug.Log("¡Recarga para disparar!");
            }
        }
    }

    public void Shoot()
    {
        weaponSystem.Shoot();

        GameObject bullet = Instantiate(bulletPrefab);
        bullet.transform.position = spawner.position;
        bullet.transform.rotation = transform.rotation;
        Destroy(bullet, 4f);
    }

    public void ReloadWeapon()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            weaponSystem.Reload();
        }
    }


    //necesito la logica de agarrar el arma con el teclado flecha abajo
    //tambien tener un arreglo de armas con el que se especifique que solo puedo tener 2 armas
    //con el teclado x poder cambiar de armas si es que el arreglo armas es mayor a 1


    //necesitamos crear un ammoItem en unity como prefab que por default 
    //tenga 10 de municion, que tenga un ontriger2d para poder propiciar
    //esas 10 balas al currentAmmo y luego destruir el gameobject
}
