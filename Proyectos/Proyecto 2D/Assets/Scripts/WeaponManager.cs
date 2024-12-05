using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public GameObject[] weapons; //Array de las armas disponibles
    private int currentWeaponIndex = 0;  //Indice de la arma activa

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Asegurate de que el jugador empieze con el primer arma activa
        ActivateWeapon(currentWeaponIndex);
        
    }

    // Update is called once per frame
    void Update()
    {
        //cambiar de arma con la tecla x
        if(Input.GetKeyDown(KeyCode.X))
        {
            currentWeaponIndex = (currentWeaponIndex + 1) % weapons.Length; //cambiar el indice
            ActivateWeapon(currentWeaponIndex);
        }

        //Agarrar una nueva arma con la tecla hacia abajo
        if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            //Aqui puedes instancar un nuevo arma desde el suelo si lo deseas
            GrabNewWeapon();
        }
        
    }

    //Activar la arma actual
    void ActivateWeapon(int weaponIndex)
    {
        //Desactivar todas las armas
        foreach(var weapon in weapons)
        {
            weapon.SetActive(false);
        }
        //Activar solo la arma seleccionada
        weapons[weaponIndex].SetActive(true);   
    }

    //Agregar un nuveo arma a la lista
    void GrabNewWeapon()
    {
        //Aqui escribir como agregar nuevas armas desde el suelo
        //por ejemplo: instanciando un arma en el mundo
        GameObject newWeapon = Instantiate(weapons[0], transform.position, Quaternion.identity);
        newWeapon.transform.SetParent(transform); //Hacer que la nueva arma sea hijo del jugador.
        weapons = new GameObject[weapons.Length + 1]; //Redimensionar el array de armas
        weapons[weapons.Length -1] = newWeapon; //Agregar la nueva arma al array
        ActivateWeapon(weapons.Length -1);
    }
    
}
