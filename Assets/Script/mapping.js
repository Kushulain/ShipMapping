// VIDEO MAPPING TOOL ----------------
// hold space bar to go to mapping mode
// while maintinh space bar :
// - left click : move closest vertex
// - left click + left shift : move vertex with soft radius
// - ctrl + R : reset saved file
// - mouse wheel : change soft radius


#pragma strict
import System;
import System.IO;
import System.Collections.Generic;

var obj : GameObject;
var cam : Transform;  

private var mappingMode = false;

var sensitivity : float = 500;
var softRadius : float = 500;

var mappingMaterial : Material;

private var lastPos : Vector3;
private var CurrentVertices = new Array ();
private var ratios = new Array ();
private var initialMat : Material;
private var highlightVertices = new List.<GameObject>();
private var originalMesh : Mesh;
private var originalCam : GameObject;
 
function Start(){
	originalMesh = new Mesh();
	
	var mesh = obj.GetComponent.<MeshFilter>().mesh;
	originalCam = Instantiate(cam.gameObject);
	originalCam.GetComponent.<Camera>().enabled = false;
	//originalCam.tag = "Untagged";
	Destroy( originalCam.GetComponent("camControl") );
	originalMesh.vertices = mesh.vertices;
	readMapping();	
}

function Update () {

	var mesh : Mesh = obj.GetComponent.<MeshFilter>().mesh;
	var vertices : Vector3[] = mesh.vertices;

	// DELETE HIGHLIGHT
	for (var toto in highlightVertices) 
	    Destroy(toto);
	highlightVertices.Clear();

	// SET SOFT RADIUS

	if (Input.GetAxis("Mouse ScrollWheel") < 0)
		softRadius *= 0.9;
	else if (Input.GetAxis("Mouse ScrollWheel") > 0)
		softRadius *= 1.1;
	
	// TOGGLE MAPPING MODE : initialize on activate, save on leave

	if(Input.GetKeyDown(KeyCode.Space)){
		mappingMode = !mappingMode;	

		if(mappingMode){
			initialMat = obj.GetComponent.<Renderer>().material;
			//obj.GetComponent.<Renderer>().material = mappingMaterial;
		}else{
			//obj.GetComponent.<Renderer>().material = initialMat;
			saveMapping();
		}
	}

	// DEFORM MODEL	

	if(mappingMode){
		

		if( Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2) )	
			lastPos = Input.mousePosition;

		// SELECT VERTICES MY MOUSE OVER
		
		if(!Input.GetMouseButton(0)){ // get closest vertex			
			
			var shortest : float = 10000;
			
			CurrentVertices.Clear();
			ratios.Clear();
			
			// find the closest vertex to put first in list
			for (var i = 0; i < vertices.Length; i++){
			
				var worldPos = obj.transform.TransformPoint(vertices[i]);
				var screenPos: Vector3 = cam.gameObject.GetComponent.<Camera>().WorldToScreenPoint(worldPos);
				screenPos.z = 0;
				
				var distance = ( screenPos - Input.mousePosition).magnitude;
						
				if (distance < shortest){ 
					CurrentVertices.Clear();
					ratios.Clear();
					shortest = distance;
					CurrentVertices.Add(i);
					ratios.Add(1);
				}
			}

			// than add all others vertices than enter in soft radius range
			for (i = 0; i < vertices.Length; i++){

				if(i != CurrentVertices[0]){

					var CenterWorldPos = obj.transform.TransformPoint(vertices[CurrentVertices[0]]);
					var currentWorldPos = obj.transform.TransformPoint(vertices[i]);

					var currentDistance = (currentWorldPos - CenterWorldPos).magnitude;
					if (currentDistance < softRadius){
						CurrentVertices.Add(i);
						ratios.Add(1 - (currentDistance / softRadius) );
					}
				}
			}
			
		}
		
		// DEFORM BY MOVING SELECTED VERTICES
		
		if(Input.GetMouseButton(0) && !Input.GetKey(KeyCode.LeftAlt) && !Input.GetKey(KeyCode.LeftControl) ){ // then move it
		
			var delta = Input.mousePosition - lastPos;
			
			mesh = obj.GetComponent.<MeshFilter>().mesh;
			vertices = mesh.vertices;		
			
			if(Input.GetKey(KeyCode.LeftShift)){ // if in soft radius mode			
				for (var k = 0; k < CurrentVertices.length; k++){ //soft radius
					var ratio : float = ratios[k];
					vertices[CurrentVertices[k]] +=  Mathf.SmoothStep(0,1,ratio) * (delta.x/sensitivity * cam.transform.right +  delta.y/sensitivity * cam.transform.up);	
				}
				
			}else{ // move only selected vertex
				for (var j = 0; j < CurrentVertices.length; j++){
					if(ratios[j] == 1)
						vertices[CurrentVertices[j]] += delta.x/sensitivity * cam.transform.right +  delta.y/sensitivity * cam.transform.up;
				}
			}		
			mesh.vertices = vertices;
			mesh.RecalculateBounds();
			lastPos = Input.mousePosition;	
		}


		// HIGHLIGHT SELECTED VERTICES
		
		for (var toto in highlightVertices)
        {
            Destroy(toto);
        }
        highlightVertices.Clear();

        for (var l = 0; l < CurrentVertices.length ; l++){

	        var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
	        var vertexPos : Vector3 = mesh.vertices[CurrentVertices[l]];
	        vertexPos = obj.transform.TransformPoint(vertexPos); 
	        cube.transform.position = vertexPos;
	        highlightVertices.Add(cube);
	        cube.transform.localScale = Vector3(0.05,0.05,0.05);
	        cube.GetComponent.<Renderer>().material.shader = Shader.Find("Unlit/Color");
	        cube.GetComponent.<Renderer>().material.color = Color.Lerp(Color.red,Color.green, ratios[l] );

	        if(!Input.GetKey(KeyCode.LeftShift))
	        	break;
	    }

	    // RESET
		if( Input.GetKey(KeyCode.R)){
			Debug.Log("reset mapping");
			var filePath = Application.persistentDataPath + "/mapping.txt";
			if (File.Exists(filePath))
				File.Delete(filePath);
			obj.GetComponent.<MeshFilter>().mesh.vertices = originalMesh.vertices;
			cam.transform.position = originalCam.transform.position;
			cam.transform.rotation = originalCam.transform.rotation;
			cam.GetComponent.<Camera>().fieldOfView = originalCam.GetComponent.<Camera>().fieldOfView;
			cam.GetComponent.<Camera>().orthographicSize = originalCam.GetComponent.<Camera>().orthographicSize;
		}
	}
}


function saveMapping(){

	var mesh = obj.GetComponent.<MeshFilter>().mesh;
	var vertices = mesh.vertices;		
	
	var filePath = Application.persistentDataPath + "/mapping.txt";
	
	if (File.Exists(filePath))
		File.Delete(filePath);
		
	var sr = File.CreateText(filePath);	
	sr.WriteLine (cam.position.x);
	sr.WriteLine (cam.position.y);
	sr.WriteLine (cam.position.z);
	sr.WriteLine (cam.gameObject.GetComponent.<Camera>().orthographicSize);
	sr.WriteLine (cam.eulerAngles.x);
	sr.WriteLine (cam.eulerAngles.y);
	sr.WriteLine (cam.eulerAngles.z);
	sr.WriteLine (cam.gameObject.GetComponent.<Camera>().fieldOfView);
	for (var k = 0; k < vertices.Length; k++){
		sr.WriteLine (vertices[k].x+","+vertices[k].y+","+vertices[k].z);
	}
	
	sr.Close();	
	Debug.Log("saved file to "+filePath);

}

function readMapping(){

	var filePath = Application.persistentDataPath + "/mapping.txt";
	if (File.Exists(filePath)){
	
		var sr = new StreamReader(filePath);
		
		var line = sr.ReadLine();
			cam.position.x = parseFloat(line);
		line = sr.ReadLine();
			cam.position.y = parseFloat(line);
		line = sr.ReadLine();
			cam.position.z = parseFloat(line);
		line = sr.ReadLine();
			cam.gameObject.GetComponent.<Camera>().orthographicSize = parseFloat(line);
		line = sr.ReadLine();
			var camRx : float = parseFloat(line);
		line = sr.ReadLine();
			var camRy : float = parseFloat(line);
		line = sr.ReadLine();
			var camRz : float = parseFloat(line);
		cam.rotation = Quaternion.Euler(camRx,camRy,camRz);
		line = sr.ReadLine();
			cam.gameObject.GetComponent.<Camera>().fieldOfView = parseFloat(line);
			
		var mesh = obj.GetComponent.<MeshFilter>().mesh;
		var vertices = mesh.vertices;
		var i = 0;
		
		line = sr.ReadLine();
		while (line != null) {            
            var position = line.Split(","[0]);
            if ( vertices[i] != null )
            	vertices[i] = new Vector3(parseFloat(position[0]),parseFloat(position[1]),parseFloat(position[2]));
            i ++;
            line = sr.ReadLine();
        }
        
        mesh.vertices = vertices;
		mesh.RecalculateBounds();
        
		sr.Close();		
	}
}