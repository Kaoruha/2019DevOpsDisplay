using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public Transform target;

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(target);
    }

    private bool isInUse = false;

    public void useCam() {
        if (!isInUse) {
            this.isInUse = true;
        }
    }

    public void UseDone() {
        if (isInUse) {
            this.isInUse = false;
        }
    }

    public bool GetStatus() {
        return this.isInUse;
    }
}
