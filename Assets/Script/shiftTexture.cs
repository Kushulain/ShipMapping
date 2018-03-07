using UnityEngine;
using System.Collections;

public class shiftTexture : MonoBehaviour {
    public float speed = 4f;
    public float changeMatSpeed = 4f;
	public float continuousSpeed = 0.1f;
	public float changeListMat = 0.1f;
    public float movement = 4f;
    public Material[] allMaterials;
    public Material setupMat;


    private Renderer rend;    
    private Vector2[] UVoffset;
    private Material[] mats;
    private float[] timers;
    private float[] timerMat;
    private bool mappingMode;

    void Start() {
        rend = GetComponent<Renderer>();
        mats = rend.materials;
        timers = new float[mats.Length];
        timerMat = new float[mats.Length];
        UVoffset = new Vector2[mats.Length];

    }
    void Update() {
    	for(int i=0; i< mats.Length; i++){
	    	timers[i] -= Time.deltaTime;
	    	timerMat[i] -= Time.deltaTime;
	    	UVoffset[i].y -= Time.deltaTime * continuousSpeed * mats[i].mainTextureScale.x;
	    	UVoffset[i].x -= Time.deltaTime * continuousSpeed * mats[i].mainTextureScale.x;
	    	if(timers[i]<0){
	    		timers[i] = Random.Range(0,speed);
	    		UVoffset[i].x += Random.value/movement;
	    		UVoffset[i].y += Random.value/movement;	    		
	    	}
	    	if(timerMat[i]<0){
	    		timerMat[i] = Random.Range(changeMatSpeed/2,changeMatSpeed);
	    		mats[i] = allMaterials[ Random.Range(0,allMaterials.Length) ];
	    		rend.materials = mats;
	    	}
	    	mats[i].SetTextureOffset("_MainTex", UVoffset[i]);
	    	rend.materials = mats;
	    }

    }
}
