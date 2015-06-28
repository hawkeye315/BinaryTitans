using UnityEngine;
using System.Collections;

public class LevelLoader : MonoBehaviour {
	private int[] floor = {0,0,0,0,0,0,1,1,1,1,2,2,2,2,-1,-1,0,0,0};
	
	void Start() {
		for (int x = 0; x < floor.Length; x++) {
			if (floor[x] >= 0)
				Instantiate(Resources.Load("Floor1_1x4"), new Vector3(x*4, floor[x], 2), Quaternion.Euler(new Vector3(270,0,0)));
			for (int y=-2; y<4; y++)
				Instantiate(Resources.Load("Wall1_4x4"), new Vector3(x*4, y*4, 2), Quaternion.Euler(new Vector3(270,0,0)));
		}
	}
}
