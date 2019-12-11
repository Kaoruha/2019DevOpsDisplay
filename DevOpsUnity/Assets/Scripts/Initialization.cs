using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using LitJson;

public class Initialization : MonoBehaviour {

	public Rittal myRittal;

	private List<Vector3> posLists = new List<Vector3>(3);

	private JsonData myJson;

	public Transform rittalList;

	private Server defaltServer = new Server();

	private void Start() {
		GetRittalPos();
		TextAsset jsonAsset = Resources.Load<TextAsset>("Config");
		myJson   = JsonMapper.ToObject(jsonAsset.text)["data"];
//		myJson   = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/Resources/Config.json"))["data"];
		DoInitialization();
	}

	private void DoInitialization() {
//		机柜初始化
		for (int i = 0; i < myJson.Count; i++) {
			Rittal go = Instantiate(myRittal, posLists[i], myRittal.transform.rotation,transform);
			go.transform.SetParent(rittalList);
//			服务器初始化
			for (int j = 0; j < myJson[i]["ServerConfig"].Count; j++) {
				int pos = int.Parse(myJson[i]["ServerConfig"][j]["pos"].ToString());
				int floor = int.Parse(myJson[i]["ServerConfig"][j]["floor"].ToString());
				int type = int.Parse(myJson[i]["ServerConfig"][j]["type"].ToString());
				string name = myJson[i]["ServerConfig"][j]["name"].ToString();
				go.Initialization();
				go.addServer(pos,floor,type);
				List<Server> servers = go.Generation();
				servers[j].Initialization();
				servers[j].SetName(name);
				string ip1 = myJson[i]["ServerConfig"][j]["ip"]["ip1"].ToString() + "||" +
				             myJson[i]["ServerConfig"][j]["ip"]["ip2"].ToString();
				string ip2 = myJson[i]["ServerConfig"][j]["ip"]["ip3"].ToString() + "||" +
				             myJson[i]["ServerConfig"][j]["ip"]["ip4"].ToString();
				servers[j].SetIP1(ip1);
				servers[j].SetIP2(ip2);
				for (int k = 0; k < myJson[i]["ServerConfig"][j]["Disk"].Count; k++) {
					int column = int.Parse(myJson[i]["ServerConfig"][j]["Disk"][k]["column"].ToString());
					int row =int.Parse(myJson[i]["ServerConfig"][j]["Disk"][k]["row"].ToString());
					int state =int.Parse(myJson[i]["ServerConfig"][j]["Disk"][k]["state"].ToString());
					servers[j].SetDiskStatus(column,row,state);
					string msg = myJson[i]["ServerConfig"][j]["Disk"][k]["msg"].ToString();
				}
				List<Disk> disks = servers[j].Generation();
				
			}
		}
			
	}


//	获取机柜坐标
	private void GetRittalPos() {
		posLists.Add(new Vector3(0,0,0));
		for (int i = 0; i < transform.childCount; i++) {
			posLists.Add(transform.GetChild(i).position);
		}
		
	}
}
