using UnityEngine;
using System.Collections;

public class shiftTexture : MonoBehaviour {
    public float speed = 4f;
    public float continuousSpeed = 0.1f;
    public float movement = 4f;
    private Renderer rend;
    private float timer = 1;
    private Vector2 UVoffset;

    void Start() {
        rend = GetComponent<Renderer>();
    }
    void Update() {
    	timer -= Time.deltaTime;
    	UVoffset.y -= Time.deltaTime * continuousSpeed;
    	if(timer<0){
    		timer = Random.Range(0,speed);
    		UVoffset.x += Random.value/movement;
    		UVoffset.y += Random.value/movement;
    		
    	}
    	rend.material.SetTextureOffset("_MainTex", UVoffset);
    }
}
