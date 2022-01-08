using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateManager : MonoBehaviour 
{

	private GameObject reticle;
	private GameObject guns;

	private ReticleManager reticleManager;
	private GunManager gunManager;

	// Use this for initialization
	private void Start()
	{
		assignObjectVariables();
		getScriptComponents();
	}

	// Update is called once per frame
	private void Update() 
	{
		reticleManager.updateReticleAgentCollision();
		reticleManager.getActiveGun();

		gunManager.getKeyInputChange();
		gunManager.applyRecoilToGun();
	}

	private void assignObjectVariables()
	{
		reticle = GameObject.Find("Reticle");
		guns = GameObject.Find("Guns");
	}

	private void getScriptComponents()
	{
		reticleManager = reticle.GetComponent<ReticleManager>();
		gunManager = guns.GetComponent<GunManager>();
	}
}
