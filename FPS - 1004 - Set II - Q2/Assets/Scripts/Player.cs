using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    #region Movement Variables
    public float movSpeed = 5f;
    #endregion

    #region Camera Variables
    public Transform cam;
    public float rotSpeed = 5f;
    float curRotX = 0;
    Vector3 curCamRotation;
    public float maxRotAngle = 60f;
    #endregion

    #region Jump Varibles
    public float jumpForce = 200f;
    bool isGrounded = true;
    #endregion

    #region Bullet Variables
    public GameObject bullet;
    public GameObject bulletPrefab;
    public Transform nozzle;
    public float bulletExpTime = 2f;
    public float bulletSpeed = 2000f;
    #endregion

    #region Player Variables
    Rigidbody rb;
    #endregion

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        #region Movement
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        //Vector3 mouseX = Input.GetAxis("MouseX");
        //Vector3 mouseY = Input.GetAxis("MouseY");

        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        Vector3 moveRight = transform.right * h;
        Vector3 moveForward = transform.forward * v;

        Vector3 movement = (moveRight + moveForward).normalized * movSpeed /** Time.deltaTime*/;
        movement.y = rb.velocity.y;
        rb.velocity = movement;
        #endregion

        #region Camera
        curRotX += (mouseY * rotSpeed);
        curRotX = Mathf.Clamp(curRotX, -maxRotAngle, maxRotAngle);
        transform.Rotate(Vector3.up * mouseX * rotSpeed);
        curCamRotation = cam.rotation.eulerAngles;
        curCamRotation.x = -curRotX;
        cam.rotation = Quaternion.Euler(curCamRotation);
        #endregion

        #region Jump
        if (Input.GetButtonDown("Jump") && isGrounded == true)
        {
            rb.AddForce(Vector3.up * jumpForce);
            isGrounded = false;
        }
        #endregion

        #region Bullet
        if (Input.GetButtonDown("Fire1"))
        {
            bullet = Instantiate(bulletPrefab, nozzle.position, nozzle.rotation);
            bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward * bulletSpeed);
            Destroy(bullet, bulletExpTime);
        }
        #endregion

        #region Death
        //if (isGrounded == false)
        //{
        //    SceneManager.LoadScene("Menu");
        //}
        #endregion
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
    }

}
