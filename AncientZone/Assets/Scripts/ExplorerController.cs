using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplorerController : MonoBehaviour
{
    public float forwardMagnitude;

    public float axisHVal;
    public float axisVVal;
    public bool isRunning;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        axisHVal = Input.GetAxis("Horizontal");
        axisVVal = Input.GetAxis("Vertical");
        isRunning = Input.GetButton("Fire2");

        InputHandler.HandleMovementInput(this.gameObject, axisHVal, axisVVal, isRunning);
        
        Debug.DrawRay(this.transform.position, this.transform.forward * this.forwardMagnitude);
    }
}
