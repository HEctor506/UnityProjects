using UnityEngine;
using System;

public class Health : MonoBehaviour
{
    public float maxHealth; //salud maxima
    public float percent{
        get { return this.currentHealth / this.maxHealth; } // osea divide la salud actual con la salud maxima
    }

    protected float currentHealth; //salud actual
    public Action<float, Vector3> OnDamage;
    public Action OnDeath;


    protected virtual void Awake()
    {
        this.currentHealth = this.maxHealth;
    }

    public virtual void Restore(float amount)
    {
        this.currentHealth = Mathf.Clamp(this.currentHealth + amount, 0, this.maxHealth);
    }


    public virtual void Damage(float amount, Vector3 instigatorLocation)
    {
        if (this.OnDamage != null)
        {
            this.OnDamage(amount, instigatorLocation);
        }

        this.currentHealth = Mathf.Clamp(this.currentHealth - amount, 0, this.maxHealth);

        if (this.currentHealth == 0)
        {
            this.Die();
        }
    }


    public virtual void Die()
    {
        if (this.OnDeath != null)
        {
            this.OnDeath();
        }

        Destroy(this.gameObject);
    }
}
