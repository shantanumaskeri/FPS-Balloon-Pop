using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReticleManager : MonoBehaviour
{

	[Header("Reticle Sprites")]
	public Sprite reticleOnTarget;
	public Sprite reticleNotOnTarget;

	private bool isTargetLocked;
	private GameObject guns;
	private GameObject target;

	public GameObject Audio { get; set; }

	public Text Score;

	private int scoreValue = 0;

	// Use this for initialization
	private void Start()
	{
		assignObjectVariables();
	}

	private void assignObjectVariables()
	{
		Audio = GameObject.Find("Audio");
		guns = GameObject.Find("Guns");
	}

	public void updateReticleAgentCollision()
	{
		RaycastHit hit;
		Ray ray = new Ray(transform.position, transform.forward);

		if (Physics.Raycast(ray, out hit, Mathf.Infinity))
		{
			switch (hit.collider.tag)
			{
				case "Target":
					GetComponent<SpriteRenderer>().sprite = reticleOnTarget;

					isTargetLocked = true;
					target = hit.collider.gameObject;
					break;

				default:
					GetComponent<SpriteRenderer>().sprite = reticleNotOnTarget;

					isTargetLocked = false;
					target = null;
					break;
			}
		}
	}

	public void getActiveGun()
	{
		switch (guns.GetComponent<GunManager>().getGunID())
		{
			case 0:
			case 8:
				if (Input.GetMouseButtonDown(0))
				{
					guns.GetComponent<GunManager>().fireActiveGun();
					checkIfAgentInFiringLine();
				}
				break;

			default:
				if (Input.GetMouseButton(0))
				{
					guns.GetComponent<GunManager>().fireActiveGun();
					checkIfAgentInFiringLine();
				}
				break;
		}

		if (Input.GetMouseButtonUp(0))
        {
			guns.GetComponent<GunManager>().bulletCount = 0;
			Audio.GetComponent<AudioManager>().stopAudio();
		}
	}

	private void checkIfAgentInFiringLine()
	{
		if (isTargetLocked)
        {
			Destroy(target);

			scoreValue += 1;
			Score.text = "Score: " + scoreValue;

			isTargetLocked = false;

			GetComponent<SpriteRenderer>().sprite = reticleNotOnTarget;
		}
	}

	public void disableReticle()
	{
		this.enabled = false;
		gameObject.SetActive(false);
	}
}
