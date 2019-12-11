using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanController : MonoBehaviour {
	public Transform fan;
	private float speed = 1000f;

	private IEnumerator SelfRotation() {
		while (true) {
			fan.Rotate(Vector3.up, speed * Time.deltaTime);
			yield return 0;
		}
	}

	public void TurnOn() {
		StopCoroutine("SelfRotation");
		StartCoroutine("SelfRotation");
	}
	
	public void TurnOFF() {
		StopCoroutine("SelfRotation");
	}

}
