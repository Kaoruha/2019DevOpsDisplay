using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using LitJson;

public class Initialization : MonoBehaviour {

	private JsonData myJson;

	public List<List<Server>> ServerList  = new List<List<Server>>();
	
	private Server defaltServ = new Server();

	private void Start() {
		myJson   = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/Scripts/Config/Config.json"))["data"];
		ServerInitialization();
	}

	private void ServerInitialization() {
		for (int i = 0; i < myJson.Count; i++) {
			ServerList.Add(new List<Server>());
			for (int j = 0; j < myJson[i]["ServerConfig"].Count; j++) {
				ServerList[i].Add(defaltServ);
				
//				添加服务器ID
				int id = Int16.Parse(myJson[i]["ServerConfig"][j]["id"].ToString());
				ServerList[i][j].SetID(id);
				
//				添加服务器名称
				string serverName = myJson[i]["ServerConfig"][j]["name"].ToString();
				ServerList[i][j].SetName(serverName);
				
//				添加服务器类型
//				0:LenovoRD650
//				1:Others
				int serverType = Int16.Parse(myJson[i]["ServerConfig"][j]["type"].ToString());
				ServerList[i][j].SetServerType(serverType);
				
//				添加服务器描述
				string des = myJson[i]["ServerConfig"][j]["description"].ToString();
				ServerList[i][j].SetDes(des);
				
//				设置服务器位置（机柜42U）
				int pos = Int16.Parse(myJson[i]["ServerConfig"][j]["pos"].ToString());
				ServerList[i][j].SetPos(pos);
				
//				设置服务器高度（默认LenovoRD650占2U）
				int floor = Int16.Parse(myJson[i]["ServerConfig"][j]["floor"].ToString());
				ServerList[i][j].SetFloor(floor);
				
				print("Server: "+ ServerList[i][j].GetName());
				print("ServerPos: " + ServerList[i][j].GetPos());
				
			}
			
		}
	}
}
