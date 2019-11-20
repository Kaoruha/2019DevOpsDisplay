using System;
using System.Collections.Generic;
using UnityEngine;


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
		id = row * 6 + column;
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
}