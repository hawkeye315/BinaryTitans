using UnityEngine;
using System.Collections;

public class PlatformRotation : MonoBehaviour {
	public float angle;
	void Update () {
		transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0,0,0), Time.deltaTime * 2);
	}
}
