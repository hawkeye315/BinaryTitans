using UnityEngine;
using System.Collections;

public class CheckpointManager : MonoBehaviour {

    public GameManager gameManager;

	// Assign Game Manager script
	void Start () {
        gameManager = FindObjectOfType<GameManager>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    // Any game object that has a trigger collider set then this function will fire when 
    // any game object touches it. 
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            // Player found checkpoint and now set the current checkpoint to this object.
            gameManager.currentCheckpoint = gameObject;
            // Show location in Console window.
            Debug.Log("Checkpoint reached at " + gameManager.currentCheckpoint.transform.position.x);
        }
    }
}
