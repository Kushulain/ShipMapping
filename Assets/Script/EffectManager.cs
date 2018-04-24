using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour {

	public float cycleMin = 10f;
	public float cycleMax = 60f;
	public bool isBen = true;

	public List<GameObject> Bens_GOs;
	public List<MonoBehaviour> Bens_Components;

	public List<GameObject> Vics_GOs;
	public List<VictorEffect> Vics_Components;

	private CoolDownEvent cycle = new CoolDownEvent(1f);
	// Use this for initialization
	void Start () {
		cycle.Go(Random.Range(cycleMin,cycleMax));

	}
	
	// Update is called once per frame
	void Update () {

		if (cycle.TryGo(Random.Range(cycleMin,cycleMax)))
		{
			if (isBen)
			{
				foreach (GameObject go in Bens_GOs)
					go.SetActive(false);
				foreach (MonoBehaviour comp in Bens_Components)
					comp.enabled = false;
				foreach (GameObject go in Vics_GOs)
					go.SetActive(true);
				foreach (VictorEffect comp in Vics_Components)
					comp.enabled = true;

				isBen = false;
			}
			else
			{
				foreach (GameObject go in Vics_GOs)
					go.SetActive(false);
				foreach (MonoBehaviour comp in Vics_Components)
					comp.enabled = false;
				foreach (GameObject go in Bens_GOs)
					go.SetActive(true);
				foreach (MonoBehaviour comp in Bens_Components)
					comp.enabled = true;
				
				isBen = true;
			}
		}
	}
}
