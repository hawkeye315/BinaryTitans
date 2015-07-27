using UnityEngine;
using System.Collections;

public class ArmRotation : MonoBehaviour {

	// Update is called once per frame
	void Update () {
        Vector3 mousePosZ = Input.mousePosition;
        mousePosZ.z = 10;
        Vector3 difference = Camera.main.ScreenToWorldPoint(mousePosZ) - transform.position;
        difference.Normalize();

        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + 90);
	}
}
