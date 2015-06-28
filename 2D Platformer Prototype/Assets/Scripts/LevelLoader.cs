using UnityEngine;
using System.Collections;

public class LevelLoader : MonoBehaviour {
	private int[] floor = {0,0,0,0,0,0,1,1,1,1,2,2,2,2,-1,-1,0,0,0};
	
	void Start() {
		for (int x = 0; x < floor.Length; x++) {
			if (floor[0] >= 0)
				Instantiate(Resources.Load("Floor1_1x4"), new Vector3(x*4, floor[x], 2), Quaternion.Euler(new Vector3(270,0,0)));
		}
	}
}
