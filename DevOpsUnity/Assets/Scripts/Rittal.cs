using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rittal : MonoBehaviour
{
    #region LightControl
    private List<GameObject> lightList = new List<GameObject>();

    public enum IORate {
        low,mediumn,high
    }

    private IORate rate = IORate.low;
	

    public void SetIO(IORate rate) {
        switch (rate) {
            case IORate.low:
                this.rate = IORate.low;
                break;
            case IORate.mediumn:
                this.rate = IORate.mediumn;
                break;
            case IORate.high:
                this.rate = IORate.high;
                break;
        }
    }

	

    #endregion
}
