using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {
//	var bottomLeft
//	var topright;
//	var cameraRect;
	float speed = 2f;
	float MIN_X = -3.8f;
	float MAX_X = 4.2f;
	float MIN_Y = -6f;
	float MAX_Y = 5.9f;


	// Use this for initialization
	void Start () {
//		cameraRect = new Rect (bottomLeft.x, bottomLeft.y, topright.
	
	}
	
	// Update is called once per frame
	void Update () {
//		transform.position= new Vector3(
//			Mathf.Clamp(transform.position.x, camera


//		if (transform.position.x < -3.8) {
//			print ("Reached negative four");
//			if (Input.GetKey (KeyCode.LeftArrow)) {
//				//
//			}
//
//			if (Input.GetKey (KeyCode.RightArrow)) {
//				transform.Translate (new Vector3 (speed * Time.deltaTime, 0, 0));
//			}
//
//			if (Input.GetKey (KeyCode.DownArrow)) {
//				transform.Translate (new Vector3 (0, -speed * Time.deltaTime, 0));
//			}
//			if (Input.GetKey (KeyCode.UpArrow)) {
//				transform.Translate (new Vector3 (0, speed * Time.deltaTime, 0));
//			}
//		}
//		if (transform.position.x > 4.2) {
//
//			if (Input.GetKey (KeyCode.LeftArrow)) {
//				transform.Translate (new Vector3 (-speed * Time.deltaTime, 0, 0));
//			}
//
//			if (Input.GetKey (KeyCode.DownArrow)) {
//				transform.Translate (new Vector3 (0, -speed * Time.deltaTime, 0));
//			}
//			if (Input.GetKey (KeyCode.UpArrow)) {
//				transform.Translate (new Vector3 (0, speed * Time.deltaTime, 0));
//			}
//
//		}
//
//		else {
			transform.position = new Vector3(
			Mathf.Clamp(transform.position.x, MIN_X, MAX_X),
			Mathf.Clamp(transform.position.y, MIN_Y, MAX_Y),
			Mathf.Clamp(transform.position.z, -10, -10));
		

			if (Input.GetKey (KeyCode.RightArrow)) {
				transform.Translate (new Vector3 (speed * Time.deltaTime, 0, 0));
			}
			if (Input.GetKey (KeyCode.LeftArrow)) {
				transform.Translate (new Vector3 (-speed * Time.deltaTime, 0, 0));
			}
			if (Input.GetKey (KeyCode.DownArrow)) {
				transform.Translate (new Vector3 (0, -speed * Time.deltaTime, 0));
			}
			if (Input.GetKey (KeyCode.UpArrow)) {
				transform.Translate (new Vector3 (0, speed * Time.deltaTime, 0));
			}
		}

}
