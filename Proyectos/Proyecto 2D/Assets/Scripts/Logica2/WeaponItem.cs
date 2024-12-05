using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class WeaponItem : MonoBehaviour
{
    public GameObject weaponPrefab;
    private bool justDropped; //para evitar recojer un arma que acabamos de tirar

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.justDropped = true;

        Invoke("ActivatePickUpMode", 1f);
        
    }

    private void ActivatePickUpMode()
    {
        this.justDropped = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(this.justDropped)
            return;

        Weapon_Manager2 manager = other.GetComponent<Weapon_Manager2>();

        if(manager == null)
            return;

        if(manager.HasRoomForWeapon == false)
            return;

        var newWeapon = Instantiate(this.weaponPrefab);
        manager.PickUpWeapon(newWeapon.GetComponent<Weapon>());

        Destroy(this.gameObject);
        
    }
    
}
