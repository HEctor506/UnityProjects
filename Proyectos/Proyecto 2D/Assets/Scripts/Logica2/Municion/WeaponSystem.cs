using UnityEngine;

public class WeaponSystem 
{
    public int MaxAmmo { get; private set; }
    public int CurrentAmmo { get; private set; }

    public bool isAmmoAtMax
    {
        get => this.CurrentAmmo == this.MaxAmmo;
    }

    public WeaponSystem(int maxAmmo)
    {
        this.MaxAmmo = maxAmmo;
        CurrentAmmo = maxAmmo; // Se inicia con munición llena
    }

    public bool CanShoot()
    {
        return CurrentAmmo > 0;
    }

    public void Shoot()
    {
        if (CanShoot())
        {
            CurrentAmmo--;
            Debug.Log($"Disparos restantes: {CurrentAmmo}");
        }
        else
        {
            Debug.Log("¡Sin munición! Recarga.");
        }
    }

    public bool tryToRecharge(int amount)
    {
        if(isAmmoAtMax)
            return false;   

        this.CurrentAmmo += amount;
        Debug.Log("¡Recarga completada!");
        return true;
    }

    public void Reload()
    {
        CurrentAmmo = MaxAmmo;
        Debug.Log("¡Recarga completada!");
    }
}
