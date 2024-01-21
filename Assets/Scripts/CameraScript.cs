using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {
    public GameObject player;
    public float xmove = 0;
    public float ymove = 0;
    public float distance = 5;

    void LateUpdate() {
        xmove += Input.GetAxis("Mouse X");
        ymove -= Input.GetAxis("Mouse Y");
        transform.rotation = Quaternion.Euler(ymove, xmove, 0);
        Vector3 reverseDistance = new Vector3(0.0f, 0.0f, distance);
        transform.position = player.transform.position - transform.rotation * reverseDistance;
    }
}
