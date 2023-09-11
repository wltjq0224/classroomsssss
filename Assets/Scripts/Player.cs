using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float jumpForce = 10.0f;
    public float mouseSensitivity = 100.0f;

    private Rigidbody rb;
    private Camera mainCamera;
    private float verticalLookRotation;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        mainCamera = Camera.main;
    }

    void Update()
    {
        // WASD �̵�
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 moveDirection = transform.forward * verticalInput + transform.right * horizontalInput;
        moveDirection.Normalize();
        rb.velocity = moveDirection * moveSpeed + Vector3.up * rb.velocity.y;

        // Spacebar ����
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        // ���콺 ȸ��
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        verticalLookRotation += mouseY;
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, -90f, 90f);
        mainCamera.transform.localEulerAngles = Vector3.left * verticalLookRotation;
        transform.Rotate(Vector3.up * mouseX);
    }
    public float rotateSpeed;

    void OnCollisionEnter(Collision collision)
    {
        // �浹�� ������Ʈ�� "Guidepoint"�� ���
        if (collision.gameObject.name == "Guidepoint")
        {
            // "Guidepoint" ������Ʈ ����
            Destroy(collision.gameObject);
        }
    }
}
