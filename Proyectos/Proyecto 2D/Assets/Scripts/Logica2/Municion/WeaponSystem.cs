using UnityEngine;

public class WeaponSystem 
{
    public int MaxAmmo { get; private set; }
    public int CurrentAmmo { get; private set; }

    public WeaponSystem(int maxAmmo)
    {
        MaxAmmo = maxAmmo;
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

    public void Reload()
    {
        CurrentAmmo = MaxAmmo;
        Debug.Log("¡Recarga completada!");
    }
}
