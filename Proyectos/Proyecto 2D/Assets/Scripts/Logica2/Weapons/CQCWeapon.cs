using Unity.VisualScripting;
using UnityEngine;

public class CQCWeapon : Weapon
{
    public Transform hitSpot;
    public float range = 1;

    protected override void OnActivate()
    {
        Collider2D[] targets = Physics2D.OverlapCircleAll(this.hitSpot.position, this.range);

        foreach (var target in targets)
        {
            //Health h = target.GetComponent<Health>();
            //if (h == null)
            //     continue;
            //this.OnHit;
        }
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(hitSpot.position, range); 
    }
}
