using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class ServerInterraction : MonoBehaviour
{
	#region Hover

	private Vector3 origialPos;
	private Vector3 hoverPos;
	private Vector3 detailPos;
	private float speed;
	private Transform myServer;
	private bool isDetail;
	private Transform camTarget;
	private Transform mainCam;

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
	
	private Vector3 camOffSet = new Vector3(10f,12f,-26f);
	private Vector3 camHome = new Vector3(16.4f,31.2f,-75.5f);
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
	private Vector3 tarHome = new Vector3(10.4f,21f,-34.3f);
//	摄像机对象移动
	private IEnumerator TarMove(Vector3 tarPos) {
		Vector3 temp = tarPos + new Vector3(1f, .5f, 0);
		while (camTarget.position != temp) {
			camTarget.transform.position = Vector3.MoveTowards(camTarget.position,temp,tarSpeed * Time.deltaTime);
			yield return 0;
		}

		if (camTarget.localPosition == temp) {
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

	#region INFO
//	服务器名称
	private string name = "Controller01";
	public TextMeshPro tagName;
	public void SetName(string name) {
		this.name = name;
		tagName.text = name;
		tagNameTips.GetChild(1).GetComponent<TextMeshPro>().text = name;
	}
	
//	服务器资产标签
	private string assetsID = "X0204-003-003";
	private string assetsName = "显示器";
	private string assetsBrand = "Philips显示器（AU5A196200029138G）";
	private string assetsSN = "AU5A196200029138G";

	public void SetAssets(string id,string name,string brand,string sn) {
		this.assetsID = id;
		this.assetsName = name;
		this.assetsBrand = brand;
		this.assetsSN = sn;
	}
	
//	性能参数
	public Transform info;
	private Transform[] infos;
	private double temperature = 20.2f;
	public TextMeshPro temperatureTextMesh;
	private int rotationRate = 2000;
	public TextMeshPro rotationRateTextMesh;
	private double cpuRate = 32.2f;
	public TextMeshPro cpuTextMesh;
	private double ramRate = 45.2f;
	public TextMeshPro ramTextMesh;
	private string ip1 = "10.200.43.160\r\n10.200.43.160";
	public TextMeshPro ip1TextMesh;
	private string ip2 = "10.200.43.161\r\n10.200.43.161";
	public TextMeshPro ip2TextMesh;
	private int voltage = 220;
	public TextMeshPro voltageTextMesh;
	private int power = 550;
	public TextMeshPro powerTextMesh;

	private float changeRate = 1.5f;//数字特效变化速度
	private IEnumerator RateChangeEffect(TextMeshPro temp) {
		float tarScale = 1.2f;
		temp.transform.localScale = new Vector3(tarScale,tarScale,tarScale);
		while (true) {
			temp.transform.localScale = new Vector3(tarScale,tarScale,tarScale);
			tarScale -= changeRate * Time.deltaTime;
			if (tarScale<=1f) {
				temp.transform.localScale = Vector3.one;
				yield break;
			}
			yield return 0;
		}
	}

	public void SetTemperature(float temp) {
		this.temperature = Math.Round(temp, 2);
		temperatureTextMesh.text = this.temperature.ToString();
		StartCoroutine("RateChangeEffect", temperatureTextMesh);
	}
	
	public void SetRotationRate(int temp) {
		this.rotationRate = temp;
		rotationRateTextMesh.text = this.rotationRate.ToString();
		StartCoroutine("RateChangeEffect", rotationRateTextMesh);
	}
	
	public void SetCPURate(float temp) {
		this.cpuRate = Math.Round(temp, 1);
		cpuTextMesh.text = this.cpuRate.ToString();
		StartCoroutine("RateChangeEffect", cpuTextMesh);
	}
	
	public void SetRAMRate(float temp) {
		this.ramRate = Math.Round(temp, 1);
		ramTextMesh.text = this.ramRate.ToString();
		StartCoroutine("RateChangeEffect", ramTextMesh);
	}
	
	public void SetIP1(string temp) {
		this.ip1 = temp.Replace("||", "\r\n");
		ip1TextMesh.text = this.ip1;
		StartCoroutine("RateChangeEffect", ip1TextMesh);
	}
	
	public void SetIP2(string temp) {
		this.ip2 = temp.Replace("||", "\r\n");
		ip2TextMesh.text = this.ip2;
		StartCoroutine("RateChangeEffect", ip2TextMesh);
	}
	
	public void SetVoltage(int temp) {
		this.voltage = temp;
		voltageTextMesh.text = voltage.ToString();
		StartCoroutine("RateChangeEffect", voltageTextMesh);
	}
	
	public void SetPower(int temp) {
		this.power = temp;
		powerTextMesh.text = power.ToString();
		StartCoroutine("RateChangeEffect", powerTextMesh);
	}

	private IEnumerator InfoInitialization() {
		this.temperature = Random.Range(39f, 60f);
		SetTemperature(float.Parse(this.temperature.ToString()));
		yield return new WaitForSeconds(.1f);
		this.rotationRate = Random.Range(2800, 3000);
		SetRotationRate(rotationRate);
		yield return new WaitForSeconds(.1f);
		this.cpuRate = Random.Range(20f, 40f);
		SetCPURate(float.Parse(this.cpuRate.ToString()));
		yield return new WaitForSeconds(.1f);
		this.ramRate = Random.Range(20f, 50f);
		SetRAMRate(float.Parse(this.ramRate.ToString()));
		yield return new WaitForSeconds(.1f);
		SetIP1(ip1);
		yield return new WaitForSeconds(.1f);
		SetIP2(ip2);
		yield return new WaitForSeconds(.1f);
		SetVoltage(voltage);
		yield return new WaitForSeconds(.1f);
		SetPower(power);
		
		yield break;
	}

	private void FakeUpdate() {
		StartCoroutine("FakeTempUpdate");
		StartCoroutine("FakeRotationUpdate");
		StartCoroutine("FakeCPUUpdate");
		StartCoroutine("FakeRAMUpdate");
	}

	private void StopUpdate() {
		StopCoroutine("FakeTempUpdate");
		StopCoroutine("FakeRotationUpdate");
		StopCoroutine("FakeCPUUpdate");
		StopCoroutine("FakeRAMUpdate");
	}
	private IEnumerator FakeTempUpdate() {
		while (true) {
			yield return new WaitForSeconds(Random.Range(1f,20f));
			double tarTemp = this.temperature + Random.Range(-2f, 2f);
			SetTemperature(float.Parse(tarTemp.ToString()));
		}
	}
	
	private IEnumerator FakeRotationUpdate() {
		while (true) {
			yield return new WaitForSeconds(Random.Range(4f,10f));
			int offset = Random.Range(-300, 300);
			int tarRotation = this.rotationRate + offset;
			SetRotationRate(tarRotation);
		}
	}
	
	private IEnumerator FakeCPUUpdate() {
		while (true) {
			yield return new WaitForSeconds(Random.Range(0.2f,3f));
			double tarCPU = this.temperature + Random.Range(-10f, 10f);
			SetCPURate(float.Parse(tarCPU.ToString()));
		}
	}
	
	private IEnumerator FakeRAMUpdate() {
		while (true) {
			yield return new WaitForSeconds(Random.Range(0.5f,5f));
			double tar = this.temperature + Random.Range(-13f, 13f);
			SetRAMRate(float.Parse(tar.ToString()));
		}
	}
	
	
	
	


	private void GetInfo() {
		infos = new Transform[8];
		for (int i = 0; i < info.childCount; i++) {
			infos[i] = info.GetChild(i).transform;
			infos[i].localScale = Vector3.zero;
		}
	}

	private float showRate = 6f;//出现速度
	private float disappearRate = 8f;//消失速度
	private float temp = 0f;
	
//	数据显示
	private IEnumerator InfoShow(Transform trans) {
		float i = 0f;
		while (true) {
			if (i >= 1f) {
				trans.localScale = Vector3.one;
				yield break;
			}
			i += showRate * Time.deltaTime;
			trans.localScale = new Vector3(i,i,i);
			yield return 0;
		}

		
	}

	
//	数据隐藏
	private IEnumerator InfoHidden(Transform trans) {
		float t = 1f;
		while (true) {
			if (t <= 0f) {
				trans.localScale = Vector3.zero;
				yield break;
			}
			t -= disappearRate * Time.deltaTime;
			trans.localScale = new Vector3(t,t,t);
			yield return 0;
			
		}
	}

//	依次显示
	private IEnumerator Show() {
		if (!isDetail) {
			FanTriggle(true);
			while (true) {
				for (int i = 0; i < infos.Length; i++) {
					StartCoroutine(InfoShow(infos[i]));
					yield return new WaitForSeconds(0.1f);
				}
				yield break;
			}
		}
			
	}
	
//	依次隐藏
	private IEnumerator Hidden() {
		FanTriggle(false);
		while (true) {
			for (int i = 0; i < infos.Length; i++) {
				StartCoroutine(InfoHidden(infos[infos.Length - i -1]));
				yield return new WaitForSeconds(0.1f);
			}
			yield break;
		}
	}
	
//	服务器名称
	public Transform tagNameTips;

	private void NameTipsInitialization() {
		tagNameTips.localScale = Vector3.zero;
	}
	
	
	
	
	#endregion

	#region Fan

	private Vector3 fanOffset = new Vector3(1.514f,0,0);
	public GameObject fanPrefab;
	public Transform fanFather;
	private void FanGeneration() {
		for (int i = 0; i < 8; i++) {
			Vector3 pos = i * fanOffset + transform.position;
			GameObject go = GameObject.Instantiate(fanPrefab, pos,fanPrefab.transform.rotation);
			go.transform.SetParent(fanFather);
		}
	}

	private void FanTriggle(bool temp) {
		if (temp) {
			for (int i = 0; i < fanFather.childCount; i++) {
				fanFather.GetChild(i).GetComponent<FanController>().TurnOn();
			}
		}
		else {
			for (int i = 0; i < fanFather.childCount; i++) {
				fanFather.GetChild(i).GetComponent<FanController>().TurnOFF();
			}
		}
		
	}

	#endregion

	private void Awake() {
		SetPos();
		GetCam();
		FanGeneration();
		GetInfo();
		NameTipsInitialization();
	}

	private void OnMouseEnter() {
			MoveIn();
			if (!isDetail) {
				StopCoroutine("InfoHidden");
				StartCoroutine("InfoShow", tagNameTips);
			}
	}

	private void OnMouseExit() {
		MoveOut();
		if (!isDetail) {
			StopCoroutine("InfoShow");
			StartCoroutine("InfoHidden", tagNameTips);
		}
	}

	private void OnMouseDown() {
			StopCoroutine("MoveTo");
			StartCoroutine("MoveTo", detailPos);
			StopCoroutine("CamMove");
			StopCoroutine("TarMove");
			SetSpeed(detailPos,detailPos+camOffSet);
			StartCoroutine("CamMove", detailPos + camOffSet);
			StartCoroutine("TarMove", detailPos);
			StopCoroutine("Hidden");
			StartCoroutine("Show");
			isDetail = true;
			StartCoroutine("InfoInitialization");
			FakeUpdate();

	}

	private void Update() {
//		退出详情模式
		if (isDetail) {
			if (Input.GetMouseButtonDown(1)) {
				StopCoroutine("MoveTo");
				StartCoroutine("MoveTo", origialPos);
				StopCoroutine("CamMove");
				SetSpeed(tarHome,camHome);
				StartCoroutine("CamMove", camHome);
				StopCoroutine("TarMove");
				StartCoroutine("TarMove", tarHome);
				StopCoroutine("Show");
				StartCoroutine("Hidden");
				StopCoroutine("InfoShow");
				StartCoroutine("InfoHidden", tagNameTips);
				StopUpdate();
				isDetail = false;
			}
		}
		
		
	}
}
