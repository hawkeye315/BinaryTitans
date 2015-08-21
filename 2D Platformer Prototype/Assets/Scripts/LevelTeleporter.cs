using UnityEngine;
using System.Collections;

public class LevelTeleporter : MonoBehaviour {

    public string levelName;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("object is: " + other.name);
        if (other.name == "Body")
        {
            Debug.Log("Trigger entered.");
            Application.LoadLevel(levelName);
        }
    }
}
