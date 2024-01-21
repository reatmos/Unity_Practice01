using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MuseMove : MonoBehaviour
{
    public float moveSpeed = 6.0f;
    public float rotationSpeed = 700.0f;
    public float jumpForce = 8.0f;
    public float gravity = 20.0f;

    private Vector3 moveDirection = Vector3.zero;
    private CharacterController characterController;

    /*
    public int itemCount;
    public Text itemCountText;
    public GameManager GameManager;

    private Transform Inventory;
    public bool openInven;
    */

    void Start() {
        characterController = GetComponent<CharacterController>();
        /*
        itemCountText = GameObject.Find("itemCount").GetComponent<Text>();
        Inventory = GameObject.Find("Canvas").transform.Find("Inventory");
        Inventory.gameObject.SetActive(false);
        openInven = false;
        */
    }

    void Update() {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 moveDirectionGround = new Vector3(horizontal, 0, vertical);
        moveDirectionGround = Camera.main.transform.TransformDirection(moveDirectionGround);
        moveDirectionGround.y = 0;

        if (characterController.isGrounded) {
            moveDirection = moveDirectionGround * moveSpeed;

            if (Input.GetButtonDown("Jump")) {
                moveDirection.y = jumpForce;
            }
        } else {
            moveDirection.x = moveDirectionGround.x * moveSpeed;
            moveDirection.z = moveDirectionGround.z * moveSpeed;
        }

        moveDirection.y -= gravity * Time.deltaTime;
        characterController.Move(moveDirection * Time.deltaTime);

        if (horizontal != 0 || vertical != 0) {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirectionGround);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }

        /*
        if (Input.GetKey(KeyCode.I) && openInven == false) {
            GameObject.Find("Canvas").transform.Find("Inventory").gameObject.SetActive(true);
            openInven = true;
        } else if (Input.GetKey(KeyCode.I) && openInven == true) {
            Inventory.gameObject.SetActive(false);
            openInven = false;
        }
        */

        // UpdateItemCountText();
    }

    void OnTriggerEnter(Collider collider) {
    /*
        if (collider.tag == "Item") {
            itemCount++;
        } else if (collider.tag == "Finish") {
            GameManager.stage++;
            SceneManager.LoadScene("Stage" + GameManager.stage);
        } else */if (collider.tag == "FallDown") {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }/* else if (collider.tag == "Enemy") {
            GameManager.stage += 2;
            SceneManager.LoadScene("Stage" + GameManager.stage);
        }*/
    }

    /*
    void UpdateItemCountText() {
        itemCountText.text = "[itemCount] : " + itemCount.ToString();
    }
    */
}
