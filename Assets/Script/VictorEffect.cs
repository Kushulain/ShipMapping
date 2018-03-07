using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictorEffect : MonoBehaviour {

	public Material whiteMaterial;

	private Renderer rend;    

	void Start () {

		rend = GetComponent<Renderer>();
	}


	void OnEnable () {
		Start();
		Debug.Log("Enable");
		Material[] mats = new Material[rend.materials.Length];
		for (int i=0; i<rend.materials.Length; i++)
			mats[i] = whiteMaterial;
		rend.materials = mats;
	}
}
