using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;


public class Move : MonoBehaviour
{
    Rigidbody rb;
    Transform trans;
    Transform target = null;

    public Transform cam;
    public XRRayInteractor rayInteractor;
    public InputActionReference aButton, xButton;

    private float aButt, xButt;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        trans = GetComponent<Transform>();
    }

    void OnEnable()
    {
        rayInteractor.selectEntered.AddListener(OnSelectEnter);
        rayInteractor.selectExited.AddListener(OnSelectExit);
    }

    // Update is called once per frame
    void Update()
    {
        AButton();
        XButton();
        if (target != null)
        {
            if (aButt == 1)
            {
                trans.position = Vector3.MoveTowards(trans.position, target.position, Time.deltaTime * 20);
            }
        }
        else
        {
            if (aButt == 1) 
            {
                trans.position += cam.forward * 10 * Time.deltaTime;
            }
        }
        if (xButt == 1)
        {
            trans.position -= cam.forward * 10 * Time.deltaTime;
        }
    }

    private void AButton()
    {
        aButt = aButton.action.ReadValue<float>();
    }

    private void XButton()
    {
        xButt = xButton.action.ReadValue<float>();
    }

    public void OnSelectEnter(SelectEnterEventArgs args)
    {
        target = args.interactableObject.transform.GetChild(0);
    }

    public void OnSelectExit(SelectExitEventArgs args)
    {
        target = null;
    }
}
