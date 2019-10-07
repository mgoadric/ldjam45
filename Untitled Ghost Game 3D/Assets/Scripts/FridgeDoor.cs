using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FridgeDoor : MonoBehaviour
{
    [SerializeField] private Animator fridgeAnimController;
    public GameObject canOrigin,can;
    private Collider collider;
    AudioSource audioData;

    private void Start()
    {
        audioData = GetComponent<AudioSource>();
        collider = GetComponent<Collider>();
    }

    public void OnPush()
    {
        Debug.Log(message: "onPush");
        if (!fridgeAnimController.GetBool("pushed"))
        {
            fridgeAnimController.SetBool("pushed", true);
            audioData.Play(0);
            SpawnCan();
        }
    }

    private void SpawnCan()
    {
        Debug.Log(message: "spawning can");
        collider.enabled = false;
        can = Instantiate(can, canOrigin.transform.position, Quaternion.identity);
        Vector3 launchForce = new Vector3(1, 0, 0);
        can.GetComponent<Rigidbody>().AddForce(launchForce);

    }

}
