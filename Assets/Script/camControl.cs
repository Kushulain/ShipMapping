using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camControl : MonoBehaviour {

	private Vector3 lastMousePos;

	void Update () {
		 if( Input.GetKey( KeyCode.LeftAlt ) ){
		 	if (Input.GetMouseButton(2)){
		 		transform.position -= 0.01f * transform.right * (Input.mousePosition-lastMousePos).x;
		 		transform.position -= 0.01f * transform.up * (Input.mousePosition-lastMousePos).y;
		 	}
		 	if (Input.GetMouseButton(1)){
		 		transform.position += 0.01f * transform.forward * (Input.mousePosition-lastMousePos).x;		 		
		 	}
		 	if (Input.GetMouseButton(0)){
		 		transform.RotateAround(Vector3.zero, Vector3.up, 0.1f * (Input.mousePosition-lastMousePos).x );
		 		transform.RotateAround(Vector3.zero, transform.right, -0.1f * (Input.mousePosition-lastMousePos).y );		 		
		 	}
		 }else if( Input.GetKey( KeyCode.LeftControl ) ){
		 	if (Input.GetMouseButton(1))
		 		transform.RotateAround(transform.position, transform.forward, 0.1f * (Input.mousePosition-lastMousePos).x );
		 	if (Input.GetMouseButton(0))
		 		GetComponent<Camera>().fieldOfView += 0.1f * (Input.mousePosition-lastMousePos).x;
		 }
		 lastMousePos = Input.mousePosition;
	}
}
