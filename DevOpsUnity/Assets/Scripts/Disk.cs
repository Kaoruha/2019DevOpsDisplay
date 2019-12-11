using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using Random = UnityEngine.Random;


//	硬盘类
public class Disk : MonoBehaviour {
	
	#region DiskProperty
//	列
	private int column = 0;
	public int GetColumn() {
		return column;
	}

	public void SetColumn(int column) {
		if (column >= 0 && column <= 6) {
			this.column = column;
		}
	} 
	
	
//	行
	private int row = 0;
	
	public int GetRow() {
		return row;
	}

	public void SetRow(int row) {
		if (row >= 0 && row <= 4) {
			this.row = row;
		}
	}
	

	
//	硬盘ID
	private int id = 0;
	public void UpdateID() {
		id = (row - 1) * 6 + column;
	}

	public int GetID() {
		return id;
	}

//	硬盘是否激活
	private bool isActive = false;

	public bool GetActive() {
		return isActive;
	}

	public void SetActive(int num) {
		if (num == 0) {
			if (isActive) {
				isActive = false;
			}
		}
		else if (num == 1) {
			if (!isActive) {
				isActive = true;
			}
		}
	}
	
	#endregion
	
	#region LightController

	public Material red;
	public Material green;
	public Transform errorBorder;
	private List<GameObject> lights = new List<GameObject>(2);

	public enum IORate {
		low,mediumn,high
	}

	private IORate rate = IORate.low;
	

	public void SetIO(IORate rate) {
		switch (rate) {
			case IORate.low:
				this.rate = IORate.low;
				if (this.blinRate != 2f) {
					this.blinRate = 2f;
					StopCoroutine("Working");
					StartCoroutine("Working",blinRate);
				}
				break;
			case IORate.mediumn:
				this.rate = IORate.mediumn;
				if (this.blinRate != 10f) {
					this.blinRate = 10f;
					StopCoroutine("Working");
					StartCoroutine("Working",blinRate);
				}
				break;
			case IORate.high:
				this.rate = IORate.high;
				if (this.blinRate != 20f) {
					this.blinRate = 20f;
					StopCoroutine("Working");
					StartCoroutine("Working",blinRate);
				}
				break;
		}
	}

	private void LightControllerInitialization() {
		for (int i = 0; i < transform.childCount; i++) {
			if (transform.GetChild(i).name =="Light" || transform.GetChild(i).name =="Light.001") {
				lights.Add(transform.GetChild(i).gameObject);
			}
		}
	}

	private bool isOn = true;
	private float blinRate = 2f;
	private IEnumerator Working(float rate) {
		while (true) {
			if (isOn) {
//				ON
				for (int i = 0; i < lights.Count; i++) {
//					lights[i].SetActive(isOn);
					lights[i].transform.localScale = new Vector3(0.01603432f,0.06339002f,0.0246345f);
				}
				isOn = !isOn;
				float time = Random.Range(1f,20f);
				yield return new WaitForSeconds(time/rate);
			}
			else {
//				OFF
				for (int i = 0; i < lights.Count; i++) {
//					lights[i].SetActive(isOn);
					lights[i].transform.localScale = Vector3.zero;
				}
				isOn = !isOn;
				yield return new WaitForSeconds(0.1f);
			}
		}


	}

	public void SetAbnormal() {
		SetState(false);
	}
	
	public void SetNormal() {
		print("mimi");
		SetState(true);
	}

	private void SetState(bool state) {
		if (state) {
			//正常
			for (int i = 0; i < lights.Count; i++) {
				lights[i].GetComponent<MeshRenderer>().material = green;
				errorBorder.localScale = Vector3.zero;
			}
		}
		else {
			//异常
			for (int i = 0; i < lights.Count; i++) {
				lights[i].GetComponent<MeshRenderer>().material = red;
				errorBorder.localScale = new Vector3(0.22f,1,0.06f);
			}
		}
	}
	
	#endregion
	

	private void Start() {
		LightControllerInitialization();
		StartCoroutine("Working", blinRate);
	}

}