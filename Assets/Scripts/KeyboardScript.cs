using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class KeyboardScript : MonoBehaviour {
    public GameObject Cam;
    public CharacterController SelectPlayer;
    public float Speed;
    public float JumpPow;

    private float Gravity;
    private Vector3 MoveDir;

    public int itemCount;
    public Text itemCountText;
    public GameManager GameManager;

    private Transform Inventory;
    public bool openInven;

    void Start() {
        Speed = 5.0f;
        Gravity = 5.0f;
        MoveDir = Vector3.zero;
        JumpPow = 5.0f;
        itemCountText = GameObject.Find("itemCount").GetComponent<Text>();
        Inventory = GameObject.Find("Canvas").transform.Find("Inventory");
        Inventory.gameObject.SetActive(false);
        openInven = false;
    }

    void Update() {
        if (SelectPlayer == null) {
            return;
        }

        if (SelectPlayer.isGrounded) {
            MoveDir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            MoveDir = SelectPlayer.transform.TransformDirection(MoveDir);
            MoveDir *= Speed;

            if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0) {
                var offset = Cam.transform.forward;
                offset.y = 0;
                transform.LookAt(SelectPlayer.transform.position + offset);
            }

            if (Input.GetButton("Jump")) {
                MoveDir.y = JumpPow;
            }

        } else {
            MoveDir.y -= Gravity * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.I) && openInven == false) {
            GameObject.Find("Canvas").transform.Find("Inventory").gameObject.SetActive(true);
            openInven = true;
        } else if (Input.GetKey(KeyCode.I) && openInven == true) {
            Inventory.gameObject.SetActive(false);
            openInven = false;
        }

        SelectPlayer.Move(MoveDir * Time.deltaTime);
        UpdateItemCountText();
    }

    void OnTriggerEnter(Collider collider) {
        if (collider.tag == "Item") {
            itemCount++;
        } else if (collider.tag == "Finish") {
            GameManager.stage++;
            SceneManager.LoadScene("Stage" + GameManager.stage);
        } else if (collider.tag == "FallDown") {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        } else if (collider.tag == "Enemy") {
            GameManager.stage += 2;
            SceneManager.LoadScene("Stage" + GameManager.stage);
        }
    }

    void UpdateItemCountText() {
        itemCountText.text = "[itemCount] : " + itemCount.ToString();
    }
}
