using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WebcamManager : MonoBehaviour {


	public GameObject webcamPrefab;
	public Transform cameraHolder;

	private string[] nameOfCams;
	private List<WebCamTexture> webcamTextures = new List<WebCamTexture>();
	public List<Camera> displayCameras = new List<Camera>();

	void Start ()
	{
		int numOfCams = WebCamTexture.devices.Length;
		nameOfCams = new string[numOfCams];
		foreach(Transform child in cameraHolder)
		{
			Camera c = child.GetComponent<Camera>();
			displayCameras.Add(c);
		}

		for (int i = 0; i < numOfCams; i++)
		{
			nameOfCams[i] = WebCamTexture.devices[i].name;

			GameObject go = Instantiate(webcamPrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
			go.transform.SetParent(gameObject.transform);
			Canvas c = go.GetComponent<Canvas>();

			if(c.worldCamera == null)
			{
				c.worldCamera = GetAvailableDisplay();
			}
			else
			{
				return;
			}
			WebCamTexture webcamTexture = new WebCamTexture();
			webcamTexture.deviceName = nameOfCams[i];
			webcamTextures.Add(webcamTexture);

			go.transform.GetChild(0).GetComponent<Image>().material.mainTexture = webcamTextures[i];
			webcamTextures[i].Play();
		}
	}
	
	Camera GetAvailableDisplay()
	{
		for (int i = 0; i < displayCameras.Count; i++)
		{
			if(!displayCameras[i].isActiveAndEnabled)
			{
				displayCameras[i].enabled = true;
				return displayCameras[i];
			}
		}
		return null;
	}
}
