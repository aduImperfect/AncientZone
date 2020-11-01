using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float fRotationSpeed;

    public GameObject playerObject;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        float angleHorizontal = 0.0f;
        float angleVertical = 0.0f;

        if (Mathf.Abs(angleHorizontal = Input.GetAxis("RightHorizontal")) > 0.1f)
        {
            InputHandler.HandleRotationInput(playerObject, angleHorizontal);
        }
        else
        {
            angleHorizontal = 0.0f;
        }

        if (Mathf.Abs(angleVertical = Input.GetAxis("RightVertical")) > 0.1f)
        {
            this.transform.Rotate(Vector3.right, (angleVertical * fRotationSpeed * Time.deltaTime));
        }
        else
        {
            angleVertical = 0.0f;
        }
    }
}
