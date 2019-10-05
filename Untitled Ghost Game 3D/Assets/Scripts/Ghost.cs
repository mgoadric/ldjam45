using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{

    private Rigidbody rb;
    public int moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void Update()
    {
        // https://answers.unity.com/questions/1373810/how-to-move-the-character-using-wasd.html
        if (Input.GetKey(KeyCode.A))
            rb.AddForce(Vector3.left * moveSpeed);
        if (Input.GetKey(KeyCode.D))
            rb.AddForce(Vector3.right * moveSpeed);
        if (Input.GetKey(KeyCode.W))
            rb.AddForce(Vector3.forward * moveSpeed);
        if (Input.GetKey(KeyCode.S))
            rb.AddForce(Vector3.back * moveSpeed);

    }
}
