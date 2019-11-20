using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Server: MonoBehaviour {

	
	
//	服务器ID
	private int id = 0;

	public int GetID() {
		return this.id;
	}

	public void SetID(int id) {
		this.id = id;
	}

	
//	服务器名称
	private string ServerName = "MyServer";

	public string GetName() {
		return this.ServerName;
	}

	public void SetName(string str) {
		if (str != null && str != "") {
			this.ServerName = str;
		}
	}

//	服务器类型
//	0:LenovoRD650
//	1:Others	
	enum ServerType {
		LenovoRD650,
		Others
	}

	private ServerType serverType = ServerType.LenovoRD650;

	public string GetServerType() {
		return this.serverType.ToString();
	}

	public void SetServerType(int type) {
		switch (type) {
			case 0:
				this.serverType = ServerType.LenovoRD650;
				break;
			case 1:
				this.serverType = ServerType.Others;
				break;
		}
	}

	
//	服务器描述
	private string description = "MyDescription";

	public string GetDes() {
		return this.description;
	}

	public void SetDes(string str) {
		if (str != null && str != "") {
			this.description = str;
		}
	}

//	服务器于机柜位置
//	服务器总共42U
	private int pos = 0;

	public int GetPos() {
		return this.pos;
	}

	public void SetPos(int pos) {
		if (pos>=0 && pos<=42) {
			this.pos = pos;
		}
	}
	
	
//	服务器高度，默认占机柜2U
	private int floor = 2;

	public int GetFloor() {
		return this.floor;
	}

	public void SetFloor(int floor) {
		if (floor>=1) {
			this.floor = floor;
		}
	}
	
//	硬盘阵列
	public GameObject disk;
	private float xOffset =2.035f;
	private float yOffset =0.385f;
	private Vector3 serverPos;
	private Vector3 originalPso = new Vector3(-10.2f, 0f, 0f);
	private Vector3 diskPos =new Vector3(-10.2f,0f,0f); 


	private void Start() {
		serverPos = transform.position;
		DiskInitialization();
	}


	private void DiskInitialization() {    
		for (int i = 0; i < 4; i++) {        
			for (int j = 0; j < 6; j++) {            
				diskPos = new Vector3(serverPos.x + originalPso.x + j * xOffset, serverPos.y + originalPso.y -i*yOffset, serverPos.z + originalPso.z);            
				Instantiate(disk, diskPos, disk.transform.rotation,transform);        
			}            
		}
		
	}
}
