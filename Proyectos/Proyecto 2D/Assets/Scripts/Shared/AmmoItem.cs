using Unity.VisualScripting;
using UnityEngine;

public class AmmoItem : MonoBehaviour
{
    public int ammoAmount = 5; // Cantidad de munición que este ítem proporciona

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Buscar el Weapon Manager del jugador
        Weapon_Manager2 manager = other.GetComponent<Weapon_Manager2>();

        if (manager == null){
            Debug.Log("no fue encontrada");
            return;
        }

        // Verificar si el jugador tiene un arma equipada
        Weapon currentWeapon = manager.GetCurrentWeapon();
        if (currentWeapon == null){
            Debug.Log("Verificar si el jugador tiene un arma equipada");
            return;
        }
        // Verificar si el arma equipada puede recargar munición
        GunWeapon gunWeapon = currentWeapon as GunWeapon;
        if (gunWeapon == null || gunWeapon.weaponSystem.isAmmoAtMax){
            Debug.Log("Verificar si el arma equipada puede recargar munición");
            return;
        }
        // Recargar munición y destruir el ítem
        if (gunWeapon.weaponSystem.tryToRecharge(ammoAmount))
        {
            Debug.Log($"Recargada munición con {ammoAmount} balas.");
            Destroy(this.gameObject); // Eliminar el ítem después de usarlo
        }
    }
}