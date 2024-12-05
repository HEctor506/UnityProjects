using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [Header("Weapon")]
    public GameObject weaponItemPrefab;
    public float maxCooldownTime = 1f;  
    private float cooldownTime; 
    public float damage = 1;
    public bool IsReady => this.cooldownTime >= this.maxCooldownTime; //para saber si esta arma esta preparada para volver a ser utilizada

    [Header("Camera")]
    public new Camera camera;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        // Asignar la referencia al Main Camera desde el Tag
        camera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();  // Aquí se asigna la cámara por el tag

    }


    void Awake()
    {
        this.cooldownTime = this.maxCooldownTime;
    }

   
    void Update()
    {
        if(this.IsReady == false)
        {
            this.cooldownTime += Time.deltaTime;
        }

        RotateTowardsMouse();
        
    }

    public void Activate()
    {
        if(this.IsReady)
        {
            //shoot, attack and stuff
            this.OnActivate();

            this.cooldownTime = 0;
        }
    }

    // public virtual void OnHit(Health health)
    // {
    //     health.Damage(this.damage);
    // }


    public void Throw()
    {
        Instantiate(this.weaponItemPrefab, this.transform.position, Quaternion.identity); 
        Destroy(this.gameObject); //destruimos el arma que tengamos en la mano
        //Cuando tiremos el armar al suelo crearemos el prefab del arma en la posicion en la que estemos
    }

    private void RotateTowardsMouse()
    {
        float angle = GetAngleTowardsMouse();
        transform.rotation = Quaternion.Euler(0, 0, angle);
        
        bool shouldFlipY = angle >= 90 && angle <= 270;
        spriteRenderer.flipY = shouldFlipY;
    }

    private float GetAngleTowardsMouse()
    {
        Vector3 mouseWorldPosition = camera.ScreenToWorldPoint(Input.mousePosition);

        Vector3 mouseDirection = mouseWorldPosition - transform.position;
        mouseDirection.z = 0; //no necesitamos esta coordenada

        float angle = (Vector3.SignedAngle(Vector3.right, mouseDirection, Vector3.forward) + 360) % 360;

        return angle;
    }


    protected abstract void OnActivate();

}
