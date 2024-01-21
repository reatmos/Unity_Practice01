using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int stage;

    void Awake() {
        stage = 0;
    }
}
