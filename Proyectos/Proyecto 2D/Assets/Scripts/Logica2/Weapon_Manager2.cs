using UnityEngine;

public class Weapon_Manager2 : MonoBehaviour
{
    public Weapon currentWeapon;
    public Transform weaponHolder;
    public bool HasRoomForWeapon => this.currentWeapon == null; 

    void Start()
    {
        Weapon[] weapons = this.GetComponentsInChildren<Weapon>();
        foreach(var w in weapons)
        {
            this.PickUpWeapon(w);
        }
        
    }

   
    void Update()
    {
        if(Input.GetButton("Fire1"))
        {
            if(this.currentWeapon != null)
            {
                this.currentWeapon.Activate();
            }
        }

        if(Input.GetButton("Fire2"))
        {
            if(this.currentWeapon != null)
            {
                this.currentWeapon.Throw();
                this.currentWeapon = null;

                //this.amoUI.Display(false);
            }
        }
        
    }

    public void PickUpWeapon(Weapon weapon)
    {
        weapon.transform.position = this.weaponHolder.position;
        weapon.transform.rotation = this.weaponHolder.rotation;
        weapon.transform.SetParent(this.weaponHolder);

        this.currentWeapon = weapon;
    }
}
