using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

    public float fireRate = 0;
    public float damage = 10;

    public Transform BulletTrailPrefab;
    public Transform MuzzleFlashPrefab;
    float timeToSpawnEffect = 0;
    public float effectSpawnRate = 10;

    float timeToFire = 0;
    Transform firePoint;
    public AudioSource gunSound;
	private float mouseZ;
    // Add enemy hit sound
    void Start()
    {
        gunSound = GetComponent<AudioSource>();
		mouseZ = -(GameObject.FindObjectOfType<Camera>().transform.position.z - GameObject.FindObjectOfType<Player> ().transform.position.z);
		Debug.Log (mouseZ);
    }

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
        //Shoot();
        if (fireRate == 0)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
                gunSound.Play();
            }
            else
            {
                gunSound.Pause();
            }
        }
        else
        {
            if (Input.GetButton("Fire1") && Time.time > timeToFire)
            {
                timeToFire = Time.time + 1 / fireRate;
                Shoot();
                gunSound.Play();
            }
            else
            {
                gunSound.Pause();
            }
        }
	}

   void Shoot()
    {
        Vector3 mouseposZ = Input.mousePosition;
        RaycastHit rHit;
        mouseposZ.z = mouseZ;
        Vector3 mousePosition = new Vector3(Camera.main.ScreenToWorldPoint(mouseposZ).x, Camera.main.ScreenToWorldPoint(mouseposZ).y);

        Vector3 firePointPosition = new Vector3(firePoint.position.x, firePoint.position.y, firePoint.position.z);
        Vector3 mousePos2 = new Vector3(mousePosition.x, mousePosition.y, firePoint.position.z);

//		Ray shootRay = new Ray(firePointPosition, mousePos2);
		Ray shootRay = new Ray(firePointPosition, (mousePos2 - firePointPosition) * 100);
		Debug.DrawRay(firePointPosition, (mousePos2 - firePointPosition) * 100, Color.cyan);
        float distance = 100f;
       
        Vector2 effectDirection = (mousePos2 - firePointPosition) * 100;
        if (Time.time >= timeToSpawnEffect)
        {
			Effect(mouseposZ);
            timeToSpawnEffect = Time.time + 1 / effectSpawnRate;
        }

        if (Physics.Raycast(shootRay, out rHit, distance))
        {
            if (rHit.collider.tag== "Enemy")
            {
                
				if (rHit.transform.GetComponent<EnemyAI>().visible)
				{

					rHit.transform.GetComponent<EnemyAI>().Kill();
					Debug.Log("hit enemy.");
				}
            }
            else if(rHit.collider.tag == "Platform")
            {
                Debug.Log("Hit Platform.");
            }
        }
    }

    void Effect(Vector3 mousePosZ)
    {
        // create quaternion based on mousePosition
//        Vector3 mousePosZ = Input.mousePosition;
//        mousePosZ.z = 41;
        Vector3 difference = Camera.main.ScreenToWorldPoint(mousePosZ) - transform.position;
        difference.Normalize();
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        Quaternion newQuat = Quaternion.Euler(0f, 0f, rotZ +0);
       
        // Create Bullet Trail
        Instantiate(BulletTrailPrefab, firePoint.position, newQuat);
        // Create Muzzle Flash
        Transform clone = (Transform)Instantiate(MuzzleFlashPrefab, firePoint.position, newQuat);
        clone.parent = firePoint;
        //float size = Random.Range(0.9f, 1.3f);
        //clone.localScale = new Vector3(size, size, 9.58f);
        Destroy(clone.gameObject, 0.5f);
    }
}
