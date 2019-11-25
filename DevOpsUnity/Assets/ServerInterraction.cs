using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerInterraction : MonoBehaviour
{
	#region Hover

	private Vector3 origialPos;
	private Vector3 hoverPos;
	private Vector3 detailPos;
	private float speed;
	private Transform myServer;
	private bool isDetail;
	public Transform camTarget;
	public Transform mainCam;

//	初始化设置
	private void SetPos() {
		myServer = transform;
		origialPos = transform.position;
		hoverPos = new Vector3(origialPos.x,origialPos.y,origialPos.z - 1.6f);
		detailPos = new Vector3(origialPos.x,origialPos.y,origialPos.z - 16f);
		speed = 20f;
		isDetail = false;
	}

	private void GetCam() {
		camTarget = GameObject.Find("CameraTarget").transform;
		mainCam = GameObject.Find("MainCamera").transform;
	}

//	动态设置目标与摄像机速度
	private void SetSpeed(Vector3 tar,Vector3 cam) {
		float tarDistance = Vector3.Distance(tar, camTarget.position);
		float camDistance = Vector3.Distance(cam,mainCam.position);
		print("tar:" + tarDistance);
		print("cam:" + camDistance);
		tarSpeed = 30f;
		camSpeed = tarSpeed * camDistance / tarDistance;
	}
	

//	设备移动
	private IEnumerator MoveTo(Vector3 tarPos) {
		while (myServer.position != tarPos) {
			gameObject.transform.position = Vector3.MoveTowards(myServer.position,tarPos,speed * Time.deltaTime);
			yield return 0;
		}

		if (myServer.localPosition == tarPos) {
			StopCoroutine("MoveTo");
		}
	}
	
	private Vector3 camOffSet = new Vector3(10f,10f,-10f);
	private Vector3 camHome = new Vector3(25.5f,46.5f,-43.3f);
	private float camSpeed = 40f;
//	摄像机移动
	private IEnumerator CamMove(Vector3 tarPos) {
		while (mainCam.position != tarPos) {
			mainCam.transform.position = Vector3.MoveTowards(mainCam.position,tarPos,camSpeed * Time.deltaTime);
			yield return 0;
		}

		if (camTarget.localPosition == tarPos) {
			StopCoroutine("tarMove");
		}
	}

	private float tarSpeed = 30f;
	private Vector3 tarHome = new Vector3(10.2f,21.8f,1.8f);
//	摄像机对象移动
	private IEnumerator TarMove(Vector3 tarPos) {
		while (camTarget.position != tarPos) {
			camTarget.transform.position = Vector3.MoveTowards(camTarget.position,tarPos,tarSpeed * Time.deltaTime);
			yield return 0;
		}

		if (camTarget.localPosition == tarPos) {
			StopCoroutine("tarMove");
		}
	}
	private void MoveIn() {
		if (!isDetail) {
			StopCoroutine("MoveTo");
			StartCoroutine("MoveTo", hoverPos);
		}
	}

	private void MoveOut() {
		if (!isDetail) {
			StopCoroutine("MoveTo");
			StartCoroutine("MoveTo", origialPos);
		}
	}
	
	

	#endregion

	private void Awake() {
		SetPos();
		GetCam();
	}

	private void OnMouseEnter() {
		MoveIn();
		//TODO Hover显示服务器标签
	}

	private void OnMouseExit() {
		MoveOut();
		//TODO 移出后隐藏服务器标签
	}

	private void OnMouseDown() {
		isDetail = true;
		StopCoroutine("MoveTo");
		StartCoroutine("MoveTo", detailPos);
		StopCoroutine("CamMove");
		StopCoroutine("TarMove");
		SetSpeed(detailPos,detailPos+camOffSet);
		StartCoroutine("CamMove", detailPos + camOffSet);
		StartCoroutine("TarMove", detailPos);
		//TODO 点击后显示具体数据
	}

	private void Update() {
//		退出详情模式
		if (isDetail) {
			if (Input.GetMouseButtonDown(1)) {
				StopCoroutine("MoveTo");
				StartCoroutine("MoveTo", origialPos);
				StopCoroutine("CamMove");
				StopCoroutine("TarMove");
				SetSpeed(tarHome,camHome);
				StartCoroutine("CamMove", camHome);
				StartCoroutine("TarMove", tarHome);
				isDetail = false;
				//TODO 退出后数据隐藏
			}
		}
		
		
	}
}
