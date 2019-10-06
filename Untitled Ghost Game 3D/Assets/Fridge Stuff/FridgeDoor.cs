using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FridgeDoor : MonoBehaviour
{
    [SerializeField] private Animator fridgeAnimController;
    AudioSource audioData;

    private void Start()
    {
        audioData = GetComponent<AudioSource>();
    }

    public void OnPush()
    {
        Debug.Log(message: "onPush");
        if (!fridgeAnimController.GetBool("pushed"))
        {
            fridgeAnimController.SetBool("pushed", true);
            audioData.Play(0);
        }
    }
}
