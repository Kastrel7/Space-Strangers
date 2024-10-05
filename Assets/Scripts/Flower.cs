using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;
using static UnityEngine.GraphicsBuffer;

public class Flower : MonoBehaviour
{
    Transform trans;
    public Transform cam;
    Transform target;
    RawImage img;
    GameObject bucket;

    public Texture flwr2;
    public Texture flwr3;
    public Texture flwr4;

    public XRRayInteractor rayInteractor;
    public InputActionReference bButton;

    private float bButt;

    // Start is called before the first frame update
    void Start()
    {
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
        trans.forward = cam.forward;

        BButton();
        if (target != null)
        {
            if (bButt == 1)
            {
                runWateringCan();
            }
        }
    }

    void runWateringCan()
    {
        StartCoroutine(wateringCan());
    }

    private void BButton()
    {
        bButt = bButton.action.ReadValue<float>();
    }

    public void OnSelectEnter(SelectEnterEventArgs args)
    {
        target = args.interactableObject.transform;
    }

    public void OnSelectExit(SelectExitEventArgs args)
    {
        target = null;
    }

    IEnumerator wateringCan()
    {
        bucket = target.GetChild(0).GetChild(1).gameObject;
        bucket.SetActive(true);
        img = target.GetChild(0).GetChild(0).GetComponent<RawImage>();
        yield return new WaitForSeconds(1.5f);
        img.texture = flwr2;
        yield return new WaitForSeconds(1.5f);
        img.texture = flwr3;
        yield return new WaitForSeconds(1.5f);
        img.texture = flwr4;
        yield return new WaitForSeconds(1.5f);
        bucket.SetActive(false);
    }
}
