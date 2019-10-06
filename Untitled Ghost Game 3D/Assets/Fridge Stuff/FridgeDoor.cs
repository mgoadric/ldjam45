using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FridgeDoor : MonoBehaviour
{
    [SerializeField] private Animator fridgeAnimController;

    public void OnPush()
    {
        Debug.Log(message: "onPush");
        fridgeAnimController.SetBool("pushed", true);
    }
}
