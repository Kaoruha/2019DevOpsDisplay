using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HelloMoto : MonoBehaviour {
	public Transform rittalList;

	public void DiskError(string str) {
		StartCoroutine("blinblin");
		string[] sArray = str.Split(';');
		int rittalID = int.Parse(sArray[0]);
		int serverID = int.Parse(sArray[1]);
		int diskID = int.Parse(sArray[2]);
		if (rittalID <= rittalList.childCount) {
			if (serverID <= rittalList.GetChild(rittalID - 1).GetChild(0).childCount) {
				rittalList.GetChild(rittalID - 1).GetChild(0).GetChild(serverID - 1).GetChild(0).GetChild(diskID - 1)
					.GetComponent<Disk>().SetAbnormal();
			}
			else {
				print("DiskPositionError");
			}
		}
		else {
			print("DiskPositionError");
		}
	}

	private bool isAlert = false;

	public Transform AlertMask;

	public void DiskNormal(string str) {
		currentAlpha = 0f;
		AlertMask.GetComponent<Image>().color = new Color(1, 0.3529412f, 0.3529412f, currentAlpha);
		StopCoroutine("blinblin");
		string[] sArray = str.Split(';');
		int rittalID = int.Parse(sArray[0]);
		int serverID = int.Parse(sArray[1]);
		int diskID = int.Parse(sArray[2]);
		if (rittalID <= rittalList.childCount) {
			if (serverID <= rittalList.GetChild(rittalID - 1).GetChild(0).childCount) {
				rittalList.GetChild(rittalID - 1).GetChild(0).GetChild(serverID - 1).GetChild(0).GetChild(diskID - 1)
					.GetComponent<Disk>().SetNormal();
			}
			else {
				print("DiskPositionError");
			}
		}
		else {
			print("DiskPositionError");
		}
	}

  	public void Test() {
  		DiskError("1;2;3");
  	}
  
  	public void Test1() {
  		DiskNormal("1;2;3");
  	}

	private Color basicRed = new Color(1f, 0.18f, 0.18f, 0);
	private float tarAlpha = 0.1f;
	private float currentAlpha = 0f;
	private float speed = 0.1f;
	private bool isNowAdd = true;

	private IEnumerator blinblin() {
		if (!isAlert) {
			while (true) {
				Vector4 tempCol = new Vector4(basicRed.r, basicRed.g, basicRed.b, currentAlpha);
				AlertMask.GetComponent<Image>().color = tempCol;
				if (isNowAdd) {
					currentAlpha += speed * Time.deltaTime;
				}
				else {
					currentAlpha -= speed * Time.deltaTime;
				}

				if (currentAlpha >= tarAlpha) {
					isNowAdd = false;
				}
				
				if (currentAlpha <= 0) {
					isNowAdd = true;
				}

				yield return 0;


			}
		}
	}
}

