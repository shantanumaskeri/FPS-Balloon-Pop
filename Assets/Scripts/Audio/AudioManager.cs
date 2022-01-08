using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

	[Header("Gun SFX")]
	public AudioClip[] gunSfxList;

	private AudioSource gunAudioSource;

	private GameObject guns;

	// Use this for initialization
	private void Start()
	{
		assignObjectVariables();
		getAudioSource();
	}

	private void assignObjectVariables()
	{
		guns = GameObject.Find("Guns");
	}

	private void getAudioSource()
	{
		gunAudioSource = guns.GetComponent<AudioSource>();
	}

	public void playAudio(string type)
	{
		switch (type)
		{
			case "gun":
				switch (guns.GetComponent<GunManager>().getGunID())
				{
					case 0:
					case 8:
						startSinglePlayback(gunAudioSource, gunSfxList[guns.GetComponent<GunManager>().getGunID()]);
						break;

					default:
						startLoopPlayback(gunAudioSource, gunSfxList[guns.GetComponent<GunManager>().getGunID()]);
						break;
				}
				break;
		}
	}

	private void startSinglePlayback(AudioSource source, AudioClip clip)
	{
		source.clip = clip;
		source.Play();
		source.loop = false;
	}

	private void startLoopPlayback(AudioSource source, AudioClip clip)
	{
		if (!source.isPlaying)
		{
			source.clip = clip;
			source.Play();
			source.loop = false;
		}
	}

	public void stopAudio()
	{
		stopPlayback(gunAudioSource, gunSfxList[guns.GetComponent<GunManager>().getGunID()]);
	}

	private void stopPlayback(AudioSource source, AudioClip clip)
    {
		source.clip = clip;
		source.Stop();
	}

}
