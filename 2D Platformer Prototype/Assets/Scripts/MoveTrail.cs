using UnityEngine;
using System.Collections;

public class MoveTrail : MonoBehaviour {

    public int moveSpeed = 100;

	// Update is called once per frame
	void Update () {
        //Vector3 mouseposZ = Input.mousePosition;
        //mouseposZ.z = 10;
        //Vector3 mousePosition = new Vector3(Camera.main.ScreenToWorldPoint(mouseposZ).x, Camera.main.ScreenToWorldPoint(mouseposZ).y, Camera.main.ScreenToWorldPoint(mouseposZ).z);
        //Vector2 mousePos2 = new Vector2(mousePosition.x, mousePosition.y);
        Vector3 rightV = new Vector3(1, 0, 0);
        transform.Translate(Vector3.right * Time.deltaTime * moveSpeed);
//        Debug.Log("Translate: " + Vector3.right * Time.deltaTime * moveSpeed);
        Destroy(gameObject, 1);
	}
}
