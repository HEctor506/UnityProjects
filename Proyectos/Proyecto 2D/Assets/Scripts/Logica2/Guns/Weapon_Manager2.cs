using System.Collections.Generic;
using UnityEngine;

public class Weapon_Manager2 : MonoBehaviour
{
    public List<Weapon> weapons = new List<Weapon>(2); // Lista para almacenar hasta dos armas.
    public Transform weaponHolder;
    private int currentWeaponIndex = 0; // Índice del arma activa.

    public bool HasRoomForWeapon => this.weapons.Count < 2; // True si hay espacio para otra arma.

    void Start()
    {
        Weapon[] initialWeapons = this.GetComponentsInChildren<Weapon>();
        foreach (var weapon in initialWeapons)
        {
            this.PickUpWeapon(weapon);
        }

        if (this.weapons.Count > 0)
        {
            EquipWeapon(0); // Equipar la primera arma automáticamente al iniciar.
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && this.weapons.Count > 1)
        {
            // Cambiar entre las armas
            currentWeaponIndex = (currentWeaponIndex + 1) % this.weapons.Count;
            EquipWeapon(currentWeaponIndex);
        }

        if (Input.GetButtonDown("Fire2") && this.weapons.Count > 0)
        {
            // Lógica para tirar el arma activa
            Weapon weaponToThrow = this.weapons[currentWeaponIndex];
            weaponToThrow.Throw();
            this.weapons.RemoveAt(currentWeaponIndex);

            // Ajustar el índice si el arma lanzada era la última
            if (this.weapons.Count > 0)
            {
                currentWeaponIndex %= this.weapons.Count;
                EquipWeapon(currentWeaponIndex);
            }
        }
    }

    public void PickUpWeapon(Weapon weapon)
    {
        if (!HasRoomForWeapon) return; // No recoger si no hay espacio.

        // Configurar el arma y agregarla al inventario
        weapon.transform.position = this.weaponHolder.position;
        weapon.transform.rotation = this.weaponHolder.rotation;
        weapon.transform.SetParent(this.weaponHolder);

        this.weapons.Add(weapon);

        if (this.weapons.Count == 1)
        {
            EquipWeapon(0); // Equipar automáticamente si es la primera arma.
        }
    }

    private void EquipWeapon(int index)
    {
        for (int i = 0; i < this.weapons.Count; i++)
        {
            this.weapons[i].gameObject.SetActive(i == index);
        }
    }
}
