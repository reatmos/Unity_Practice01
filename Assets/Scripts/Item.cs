using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {
    public enum TYPE { RED, GREEN }

    public TYPE type;
    public Sprite DefaultImg;
    public int MaxCount;

    private InventoryScript Iv;

    void Awake() {
        Iv = GameObject.FindGameObjectWithTag("Inventory").GetComponent<InventoryScript>();
    }

    void AddItem() {
        if (!Iv.AddItem(this)) {
            Debug.Log("Inventory is full.");
        } else {
            gameObject.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider _col) {
        if (_col.gameObject.layer == 10) {
            AddItem();
        }
    }
}
