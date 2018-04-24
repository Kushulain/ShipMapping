using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CometGenerator : MonoBehaviour {

	public float speedMin = 50f;
	public float speedMax = 200f;
	public CoolDownEvent nextComet;

	public float timerGeneratorMin = 0.5f;
	public float timerGeneratorMax = 10f;

	public GameObject cometGO;

	public float positionGeneratorRadius = 10f;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

		if (nextComet.TryGo(Random.Range(timerGeneratorMin,timerGeneratorMax)))
		{
			Vector3 offset = new Vector3(Random.Range(-positionGeneratorRadius,positionGeneratorRadius),
										Random.Range(-positionGeneratorRadius,positionGeneratorRadius),
										Random.Range(-positionGeneratorRadius,positionGeneratorRadius));

			Instantiate(cometGO,transform.position+ offset,transform.rotation);
		}
	}
}
