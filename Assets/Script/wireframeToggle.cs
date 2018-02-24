using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wireframeToggle : MonoBehaviour {

	private bool wireframe;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown("space"))
			wireframe = !wireframe;
	}

	void OnPreRender() {
		if(wireframe)
       		GL.wireframe = true;
    }
    void OnPostRender() {
    	if(wireframe)
       		GL.wireframe = false;
    }
}
