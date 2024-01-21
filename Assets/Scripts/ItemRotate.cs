using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRotate : MonoBehaviour {
    public float rotateSpeed = 30;
    public float floatSpeed = 2.5f;
    public float floatHeight = 0.3f;

    private Vector3 startPos;
    private Quaternion startRot;

    void Start() {
        startPos = transform.position;
        startRot = transform.rotation;
    }

    void Update() {
        transform.Rotate(Vector3.down * rotateSpeed * Time.deltaTime, Space.World);

        float newY = Mathf.Sin(Time.time * floatSpeed) * floatHeight + startPos.y;
        Vector3 newPosition = new Vector3(startPos.x, newY, startPos.z);
        transform.position = newPosition;
    }
}
