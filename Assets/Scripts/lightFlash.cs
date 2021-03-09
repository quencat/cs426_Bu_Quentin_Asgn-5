using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightFlash : MonoBehaviour
{
	Light myLight;
	public float minWaitTime = 0.1f;
	public float maxWaitTime = 1.0f;

	void Start()
	{
		myLight = GetComponent<Light>();
		StartCoroutine(Flashing());
	}

	IEnumerator Flashing()
	{
		while (true)
		{
			yield return new WaitForSeconds(Random.Range(minWaitTime, maxWaitTime));
			myLight.enabled = !myLight.enabled;

		}
	}
}

//Credits: https://www.youtube.com/watch?v=WRVETgdB-qw
