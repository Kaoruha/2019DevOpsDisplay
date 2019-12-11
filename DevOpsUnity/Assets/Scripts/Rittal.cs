using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rittal : MonoBehaviour {
    public GameObject serverPrefab;
    private List<Vector3> serverPos;
    private Vector3 currentPos = Vector3.zero;
    private Vector3 rittalPos;
    private float yOffset =2f;
    private List<Server> serverList =new List<Server>();

    
//    服务器类型
//    0:LenovoRD650
//    1:Others
    public void addServer(int pos, int floor,int type) {
        serverPos.Add(new Vector3(pos,floor,type));
    }

    public List<Server> Generation() {
        for (int i = 0; i < serverPos.Count; i++) {
            currentPos = new Vector3(rittalPos.x,rittalPos.y+yOffset*serverPos[i].x/2,rittalPos.z);
            GameObject go = Instantiate(serverPrefab, currentPos, serverPrefab.transform.rotation,transform);
            go.transform.SetParent(transform.GetChild(0));
            serverList.Add(go.GetComponent<Server>());
        }

        return serverList;

    }

    public void Initialization() {
        rittalPos = transform.position;
        serverPos = new List<Vector3>(21);
    }
}
