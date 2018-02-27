using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MappingSetup : MonoBehaviour {

	private bool mappingMode;
	public Material setupMat;
	private Renderer rend;    

	// Use this for initialization
	void Start () {

		rend = GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void LateUpdate () {

		// mapping mode for setup
		if(Input.GetKeyDown(KeyCode.Space))
			mappingMode = !mappingMode;
		if(mappingMode){

			Material[] mats = new Material[rend.materials.Length];
			for (int i=0; i<rend.materials.Length; i++)
				mats[i] = setupMat;
			rend.materials = mats;
		}
	}
}
