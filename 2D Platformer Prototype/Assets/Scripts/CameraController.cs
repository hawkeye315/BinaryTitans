using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
	public Transform player;

	private Vector3 cameraVector;
	private float[] cameraPosition = new float[3];
	public float forwardCameraBuffer, rearCameraBuffer, yOffset;

	// Use this for initialization
	void Start () {
		player = GameObject.FindObjectOfType<Player>().transform;

		cameraVector = transform.position;
		cameraPosition [0] = player.transform.position.x + rearCameraBuffer;
		cameraPosition [1] = player.transform.position.y + yOffset;
		cameraPosition [2] = transform.position.z;
	}
	
	// Update is called once per frame
	void Update () {
		if ((player.transform.position.x - transform.position.x) > forwardCameraBuffer)
			cameraPosition [0] = player.transform.position.x - forwardCameraBuffer;
		else if ((player.transform.position.x - transform.position.x) < -rearCameraBuffer)
			cameraPosition [0] = player.transform.position.x + rearCameraBuffer;
        //if ((player.transform.position.y - transform.position.y) > 5)
        //    cameraPosition [1] = player.transform.position.y - 5;
        //else if ((player.transform.position.y - transform.position.y) < 0 && player.transform.position.y > -3)
        //    cameraPosition [1] = player.transform.position.y;

        cameraPosition[1] = player.transform.position.y + yOffset;
		//cameraPosition [1] += 5;
		SetCameraPosition (cameraPosition);
	}

	public void SetCameraPosition(float[] array)
	{
		transform.position = new Vector3 (array[0], array[1], array[2]);
	}
}
