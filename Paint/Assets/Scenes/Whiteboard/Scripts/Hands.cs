using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Hands : MonoBehaviour
{
    private XRDirectInteractor interactor = null;

    void Start()
    {
        interactor = GetComponent<XRDirectInteractor>();
    }

    void Update()
    {
    }
}
