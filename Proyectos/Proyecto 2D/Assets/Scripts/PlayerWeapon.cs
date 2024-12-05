using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public new Camera camera;
    public Transform spawner;
    public GameObject bulletPrefab;
     private Vector3 originalSpawnerLocalPosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        // Asignar la referencia al Main Camera desde el Tag
        camera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();  // Aquí se asigna la cámara por el tag

         // Guardamos la posición local original del spawner
        originalSpawnerLocalPosition = spawner.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        RotateTowardsMouse();
        CheckFiring();
    }

    private void RotateTowardsMouse()
    {
        float angle = GetAngleTowardsMouse();
        transform.rotation = Quaternion.Euler(0, 0, angle);
        
        bool shouldFlipY = angle >= 90 && angle <= 270;
        spriteRenderer.flipY = shouldFlipY;

        // Ajustamos la posición del spawner si ocurre un flip
        spawner.localPosition = shouldFlipY 
            ? new Vector3(originalSpawnerLocalPosition.x, -originalSpawnerLocalPosition.y, originalSpawnerLocalPosition.z)
            : originalSpawnerLocalPosition;
    }

    private float GetAngleTowardsMouse()
    {
        Vector3 mouseWorldPosition = camera.ScreenToWorldPoint(Input.mousePosition);

        Vector3 mouseDirection = mouseWorldPosition - transform.position;
        mouseDirection.z = 0; //no necesitamos esta coordenada

        float angle = (Vector3.SignedAngle(Vector3.right, mouseDirection, Vector3.forward) + 360) % 360;

        return angle;
    }


    private void CheckFiring()
    {
        if(Input.GetMouseButtonDown(0))
        {
            GameObject bullet = Instantiate(bulletPrefab); //instanciamos la bala en la escena
            bullet.transform.position = spawner.position; //para que aparezca enfrente del canon
            bullet.transform.rotation = transform.rotation;
            Destroy(bullet, 2f); //Hace que se destruya luego de 2 segundos
        }
    }

    
}
