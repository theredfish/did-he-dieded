using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoppableGameobject : MonoBehaviour {
    private bool isFreezed = false;

    public void Freeze()
    {
        FreezeRigidbody();
        FreezeCharacterController();
    }

    private void FreezeRigidbody()
    {
        Rigidbody optionalRigidbody = GetComponent<Rigidbody>();

        if (null != optionalRigidbody && !isFreezed)
        {
            optionalRigidbody.isKinematic = true;
            isFreezed = true;
        }
        else
        {
            optionalRigidbody.isKinematic = false;
            isFreezed = false;
        }
    }

    private void FreezeCharacterController()
    {
        CharacterController optionalCharacterController = GetComponent<CharacterController>();
        if (null != optionalCharacterController && !isFreezed)
        {
            optionalCharacterController.enabled = false;
            isFreezed = true;
        }
        else
        {
            optionalCharacterController.enabled = true;
            isFreezed = false;
        }
    }
}
