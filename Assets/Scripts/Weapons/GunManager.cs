using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunManager : MonoBehaviour
{

	[Header("Gun Prefab")]
	public GameObject[] gunList;

	[Header("Gun Recoil")]
	public float[] recoilList;

	[Header("Gun Key Code")]
	public KeyCode[] keyCodeList;

	[Header("Gun Shells")]
	public GameObject[] gunShellList;

	[Header("Shell Eject Position")]
	public Transform shellEjectPosition;

	[Header("Shell Eject Physics")]
	public float shellEjectForce;
	public float shellForceRandom;
	public float shellEjectTorqueX;
	public float shellEjectTorqueY;
	public float shellTorqueRandom;

	private float gunRecoilFactor = 0.0f;
	private float recoil = 0.0f;
	private float maxRecoil_x = -20f;
	private float maxRecoil_y = 20f;
	private float recoilSpeed = 2f;
	private GameObject muzzle;
	private int gunId = 0;

	[HideInInspector]
	public int bulletCount = 0;

	public GameObject Audio { get; set; }

	public Text Guns;

	// Use this for initialization
	private void Start()
	{
		//PlayerPrefs.DeleteAll();
		assignObjectVariables();
		setActiveGun(gunList[gunId], recoilList[gunId]);
	}

	private void assignObjectVariables()
	{
		Audio = GameObject.Find("Audio");
		muzzle = GameObject.Find("Muzzle");
	}

	public void getKeyInputChange()
	{
		for (int i = 0; i < gunList.Length; i++)
		{
			if (Input.GetKeyDown(keyCodeList[i]))
			{
				gunId = i;

				setActiveGun(gunList[i], recoilList[i]);
			}
		}
	}

	private void setActiveGun(GameObject gun, float factor)
	{
		gunRecoilFactor = factor;

		Transform[] ta = gameObject.GetComponentsInChildren<Transform>();

		foreach (Transform t in ta)
		{
			//Guns.text += "\n game object tag: " + t.gameObject.tag;
			if (t.gameObject.tag == "Gun")
			{
				t.gameObject.SetActive(false);

				//Guns.text += "\n gun hidden: " + t.gameObject.name;
			}
		}

		//Guns.text += "\n gun active: " + gun.name;
		gun.SetActive(true);
	}

	public void fireActiveGun()
	{
		ejectBulletShells();
		setRecoilValues(0.2f, gunRecoilFactor, 10.0f);
		Audio.GetComponent<AudioManager>().playAudio("gun");
	}

	public int getGunID()
	{
		return gunId;
	}

	private void setRecoilValues(float recoilParam, float maxRecoil_xParam, float recoilSpeedParam)
	{
		recoil = recoilParam;
		maxRecoil_x = maxRecoil_xParam;
		recoilSpeed = recoilSpeedParam;
		maxRecoil_y = Random.Range(-maxRecoil_xParam, maxRecoil_xParam);
	}

	public void applyRecoilToGun()
	{
		if (recoil > 0.0f)
		{
			Quaternion maxRecoil = Quaternion.Euler(maxRecoil_x, maxRecoil_y, 0.0f);

			transform.localRotation = Quaternion.Slerp(transform.localRotation, maxRecoil, Time.deltaTime * recoilSpeed);
			recoil -= Time.deltaTime;

			muzzle.GetComponent<MuzzleManager>().setMuzzleFlash(true);
		}
		else
		{
			transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.identity, Time.deltaTime * recoilSpeed / 2);
			recoil = 0.0f;

			muzzle.GetComponent<MuzzleManager>().setMuzzleFlash(false);
		}
	}

	private void ejectBulletShells()
	{
		switch (gunId)
        {
			case 0:
			case 8:
				GameObject shell = Instantiate(gunShellList[gunId], shellEjectPosition.position, new Quaternion(Random.Range(0.0f, 180.0f), Random.Range(0.0f, 180.0f), Random.Range(0.0f, 180.0f), 0.0f)) as GameObject;
				shell.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(shellEjectForce + Random.Range(0, shellForceRandom), 0, 0), ForceMode.Impulse);
				shell.transform.parent = muzzle.transform;
				break;

			default:
				bulletCount++;

				if (bulletCount % 10 == 0)
				{
					GameObject shell2 = Instantiate(gunShellList[gunId], shellEjectPosition.position, new Quaternion(Random.Range(0.0f, 180.0f), Random.Range(0.0f, 180.0f), Random.Range(0.0f, 180.0f), 0.0f)) as GameObject;
					shell2.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(shellEjectForce + Random.Range(0, shellForceRandom), 0, 0), ForceMode.Impulse);
					shell2.transform.parent = muzzle.transform;
				}
				break;
        }
	}
}
