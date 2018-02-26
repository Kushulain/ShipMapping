using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wireframeToggle : MonoBehaviour {

	public bool autoChange;
	public float autoChangeSpeed = 10;
	private bool wireframe;
	private float timer;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.RightControl))
			wireframe = !wireframe;
		if(autoChange){
			timer -= Time.deltaTime;
			if(timer<0){			
				timer+= Random.Range(autoChangeSpeed/2,autoChangeSpeed);
				if(!wireframe)
					timer /= 3; // less time with wireframe on
				wireframe = !wireframe;
			}
		}
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
