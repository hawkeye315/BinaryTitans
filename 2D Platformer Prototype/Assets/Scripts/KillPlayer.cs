﻿using UnityEngine;
using System.Collections;

public class KillPlayer : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
			GameObject.FindObjectOfType<Player>().ChangeHealth(-20);
        }
    }
}
