using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class SpawnBullet : MonoBehaviour
{
    public GameObject bulletPrefab;
    public int ammo = 0;
    public TextMeshProUGUI bullet_value_textHolder;

    public GameObject gameOver_panel;
    public TextMeshProUGUI sorryText_textHolder;
    String sorry_message = "Sorry, you are out of bullets";
    public GameObject game_panel;

    // Update is called once per frame
    void Update()
    {
        this.bullet_value_textHolder.text = ammo.ToString(); //to update the number of bullets
        
    }

    public void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, this.transform.position, transform.rotation, transform);
        this.ammo --;

        if(ammo == 0)
        {
            Time.timeScale = 0; //Con esto pauso el juego
            sorryText_textHolder.text = sorry_message;
            gameOver_panel.SetActive(true);
            game_panel.SetActive(false);
        }

        //to destroy the bullet after being fired
        Destroy(bullet, 1.5f);
    }
}
