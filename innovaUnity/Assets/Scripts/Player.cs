using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	
	private int coinsCollected;
	private int timesTripped;
	private float move;
	
	// Use this for initialization
	void Start () {
		move = 0.0001f;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown("space") && transform.position.y > -10){
			rigidbody.AddForce(Vector3.up * 10000);
		}
		transform.Translate(move, 0, 0);
		move *= -1;
	}
	
	void OnTriggerEnter(Collider c){
		if(c.CompareTag("Obstacle")){
			Debug.Log("fuck");
		}
	}
}
