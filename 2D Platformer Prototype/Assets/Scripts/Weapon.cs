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
        //Debug.Log("Shooting.");
        Vector3 mouseposZ = Input.mousePosition;
        mouseposZ.z = 10;
        Vector3 mousePosition = new Vector3(Camera.main.ScreenToWorldPoint(mouseposZ).x, Camera.main.ScreenToWorldPoint(mouseposZ).y, Camera.main.ScreenToWorldPoint(mouseposZ).z);
        //Debug.Log("mouseX: "+ mousePosition.x + " mouseY: " + mousePosition.y);
        //Debug.Log("Input mouseX: " + Input.mousePosition.x + " mouseY: " + Input.mousePosition.y);
        Vector2 firePointPosition = new Vector2(firePoint.position.x, firePoint.position.y);
        Vector2 mousePos2 = new Vector2(mousePosition.x, mousePosition.y);

        RaycastHit2D hitLC = Physics2D.Linecast(firePointPosition, mousePos2, whatToHit, 10);
        RaycastHit2D hit = Physics2D.Raycast(firePointPosition, mousePos2 - firePointPosition, 100, whatToHit);
        Debug.DrawLine(firePointPosition, (mousePos2 - firePointPosition) * 100, Color.cyan);
        //Debug.Log("mousepos x: " + mousePos2.x + " y: " + mousePos2.y + " firepointPos x: " + firePointPosition.x + " y: " + firePointPosition.y);
        //Debug.Log("whatToHit: " + whatToHit);
        //Debug.Log("hit.collider = " + hit.collider);
        if (hitLC.collider != null)
        {
            Debug.Log("LC: " + hitLC.collider.name);
        }

        if (hit.collider != null)
        {
            Debug.Log("testing collider hit");
            Debug.DrawLine(firePointPosition, hit.point, Color.red);
            Debug.Log("We Hit " + hit.collider.name + " and did " + damage + " damage.");
        }
    }
}
