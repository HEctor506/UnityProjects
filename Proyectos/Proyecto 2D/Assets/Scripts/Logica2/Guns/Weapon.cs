using UnityEngine;

public class Weapon : MonoBehaviour
{
    public WeaponType weaponType;

    [Header("Weapon")]
    public GameObject weaponItemPrefab;
    public float maxCooldownTime = 0.4f;  
    private float cooldownTime; 
    public WeaponSystem weaponSystem;

    public Transform spawner;
    public GameObject bulletPrefab;

    [Header("Sound")]
    [SerializeField] private AudioClip fireShootSound;
    [SerializeField] private AudioClip empyGunSound;

    void Awake()
    {
        this.cooldownTime = this.maxCooldownTime;
    }

    void Start()
    {
        // Configurar el sistema de armas con munición inicial
        weaponSystem = new WeaponSystem(10); // Máximo de 10 balas
    }

    public void Throw()
    {
        Instantiate(this.weaponItemPrefab, this.transform.position, Quaternion.identity); 
        Destroy(this.gameObject); //destruimos el arma que tengamos en la mano
        //Cuando tiremos el armar al suelo crearemos el prefab del arma en la posicion en la que estemos
        
    }

    void Update()
    {

        HandleShooting();
        // ReloadWeapon();
    }


    public void HandleShooting()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            switch (weaponType)
            {
            case WeaponType.Gun:
                ShootWithGun();
                SoundManager.instance.PlaySound(fireShootSound);
                break;
            case WeaponType.Escopeta:
                ShootWithEscopeta();
                SoundManager.instance.PlaySound(fireShootSound);
                break;
            case WeaponType.Bazooka:
                ShootWithBazooka();
                SoundManager.instance.PlaySound(fireShootSound);
                break;
            case WeaponType.GrenadeLauncher:
                ShootWithGrenadeLauncher();
                SoundManager.instance.PlaySound(fireShootSound);
                break;
            case WeaponType.DynamiteLauncher:
                ShootWithDynamiteLauncher();
                SoundManager.instance.PlaySound(fireShootSound);
                break;
            default:
                Debug.Log("Tipo de arma no soportado");
                break;
            }

        }
        
    }

    // Métodos específicos para cada tipo de arma:
    private void ShootWithGun()
    {
        if (weaponSystem.CanShoot())
        {
            weaponSystem.Shoot();
            balaPistola();
            Debug.Log("¡Disparando con la pistola!");
        }
        else
        {
            Debug.Log("¡Sin munición en la pistola!");
            SoundManager.instance.PlaySound(empyGunSound);
        }
    }

    private void ShootWithEscopeta()
    {
        if (weaponSystem.CanShoot())
        {
            weaponSystem.Shoot();
            Debug.Log("¡Disparando con la escopeta!");
            // Dispersión de balas o lógica específica de escopeta
            balaEscopeta();
        }
        else
        {
            Debug.Log("¡Sin munición en la escopeta!");
            SoundManager.instance.PlaySound(empyGunSound);
        }
    }

    private void ShootWithBazooka()
    {
        if (weaponSystem.CanShoot())
        {
            weaponSystem.Shoot();
            Debug.Log("¡Disparando con la bazooka!");
            // Lógica de bazooka, por ejemplo, proyectiles que explotan
        }
        else
        {
            Debug.Log("¡Sin munición en la bazooka!");
            SoundManager.instance.PlaySound(empyGunSound);
        }
    }

    private void ShootWithGrenadeLauncher()
    {
        if (weaponSystem.CanShoot())
        {
            weaponSystem.Shoot();
            Debug.Log("¡Disparando con el lanzador de granadas!");

            GameObject grenade = Instantiate(bulletPrefab, spawner.position, Quaternion.identity);
            ArmasExplosivas grenadeScript = grenade.GetComponent<ArmasExplosivas>();

            if (grenadeScript != null)
            {
                grenadeScript.tipoExplosivo = ArmasExplosivas.TipoExplosivo.Granada;
            }
        }
        else{
            Debug.Log("¡Sin munición en el lanzador de granadas!");
            SoundManager.instance.PlaySound(empyGunSound);
            }
    }

    private void ShootWithDynamiteLauncher()
    {
        if (weaponSystem.CanShoot())
        {
            weaponSystem.Shoot();
            Debug.Log("¡Disparando con el lanzador de dinamitas!");

            // Determina la dirección basándote en la rotación del jugador
            PlayerScript jugador = transform.root.GetComponentInParent<PlayerScript>();
            int direccion = 0;
            if (jugador != null){
                direccion = jugador.getDireccion();
            }

            // Instancia la dinamita
            GameObject dynamite = Instantiate(bulletPrefab, spawner.position, Quaternion.identity);
            ArmasExplosivas dynamiteScript = dynamite.GetComponent<ArmasExplosivas>();
            dynamiteScript.tipoExplosivo = ArmasExplosivas.TipoExplosivo.DinamitaTerrestre;
            dynamiteScript.num = direccion;
        }
        else
        {
            Debug.Log("¡Sin munición en el lanzador de dinamitas!");
            SoundManager.instance.PlaySound(empyGunSound);
        }
    }

    public void balaPistola()
    {
        GameObject bullet = Instantiate(bulletPrefab);
        bullet.transform.position = spawner.position;
        bullet.transform.rotation = transform.rotation;
        Destroy(bullet, 4f);
    }


    public void balaEscopeta()
    {

        // Ángulo de dispersión para las balas (ajústalo como desees)
        float spreadAngle = 7f;

        // Disparar 4 balas con diferentes ángulos de dispersión
        for (int i = 0; i < 4; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab);
            bullet.transform.position = spawner.position;

            // Calcular el ángulo de dispersión para cada bala
            float angleOffset = (i - 1.5f) * spreadAngle;  // Esto distribuye las balas a la izquierda y derecha

            // Aplica la dispersión pero mantiene la dirección general de la escopeta
            Vector3 shootDirection = (transform.right).normalized; // Dirección en la que apunta la escopeta
            shootDirection = Quaternion.Euler(0, 0, angleOffset) * shootDirection; // Agregar dispersión al vector de dirección

            // Asignar la dirección calculada a la bala
            balaEscopeta bulletScript = bullet.GetComponent<balaEscopeta>();
            bulletScript.SetDirection(shootDirection);  // Pasa la dirección con dispersión
            Destroy(bullet, 3f);
        }
    }


    



    //necesito la logica de agarrar el arma con el teclado flecha abajo
    //tambien tener un arreglo de armas con el que se especifique que solo puedo tener 2 armas
    //con el teclado x poder cambiar de armas si es que el arreglo armas es mayor a 1


    //necesitamos crear un ammoItem en unity como prefab que por default 
    //tenga 10 de municion, que tenga un ontriger2d para poder propiciar
    //esas 10 balas al currentAmmo y luego destruir el gameobject







}
