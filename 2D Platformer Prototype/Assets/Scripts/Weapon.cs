using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

    public float fireRate = 0;
    public float damage = 10;
    public LayerMask whatToHit;

    float timeToFire = 0;
    Transform firePoint;

    void Awake()
    {
        firePoint = transform.FindChild("FirePoint");
        if (firePoint == null)
        {
            Debug.LogError("No firepoint WHAT?!");
        }
    }
	
	// Update is called once per frame
	void Update () {
        Shoot();
        if (fireRate == 0)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
            }
        }
        else
        {
            if (Input.GetButton("Fire1") && Time.time > timeToFire)
            {
                timeToFire = Time.time + 1 / fireRate;
                Shoot();
            }
        }
	}

    void Shoot()
    {
        //Camera.main.WorldToScreenPoint
        //Camera.main.ViewportToScreenPoint
        //Camera.main.ScreenToWorldPoint
        Vector2 mousePosition = new Vector2(Camera.main.ViewportToScreenPoint(Input.mousePosition).x, Camera.main.ViewportToScreenPoint(Input.mousePosition).y);
        Vector2 firePointPosition = new Vector2(firePoint.position.x, firePoint.position.y);

        RaycastHit2D hit = Physics2D.Raycast(firePointPosition, mousePosition - firePointPosition, 100, whatToHit);
        Debug.DrawLine(firePointPosition, (mousePosition - firePointPosition)*100, Color.cyan);
        if (hit.collider != null)
        {
            Debug.DrawLine(firePointPosition, hit.point, Color.red);
            Debug.Log("We Hit " + hit.collider.name + " and did " + damage + " damage.");
        }
    }
}
