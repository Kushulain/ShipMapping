using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Comet : MonoBehaviour {

	float speed = 50f;

	public Color ColorA;
	public Color ColorB;

	// Use this for initialization
	void Start () {
		GetComponent<Light>().color = Color.Lerp(ColorA,ColorB,Random.Range(0f,1f));
	}
	
	// Update is called once per frame
	void Update () {

		transform.position = transform.position + transform.forward * speed * Time.deltaTime;

		GetComponent<Light>().intensity = (1f-2f*Mathf.Abs(transform.position.y * 0.01f)) * 1.5f;

		if (transform.position.y < -50f)
		{
			Destroy(gameObject);
		}
	}
}
