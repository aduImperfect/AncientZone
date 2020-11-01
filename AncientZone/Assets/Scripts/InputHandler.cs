using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public static void HandleMovementInputTargeted(GameObject character, float verticalAxisVal, bool isRunning, Vector3 targetPosition)
    {
        CharacterSpeed characterSpd = character.GetComponent<CharacterSpeed>();

        if (Mathf.Abs(verticalAxisVal) > 0.01f)
        {
            if (isRunning)
            {
                character.transform.position = Vector3.MoveTowards(character.transform.position, targetPosition, verticalAxisVal * characterSpd.fRunSpeed * Time.deltaTime);
            }
            else if (Mathf.Abs(verticalAxisVal) > 0.95f)
            {
                character.transform.position = Vector3.MoveTowards(character.transform.position, targetPosition, verticalAxisVal * characterSpd.fMoveSpeed * Time.deltaTime);
            }
            else
            {
                character.transform.position = Vector3.MoveTowards(character.transform.position, targetPosition, verticalAxisVal * characterSpd.fSlowSpeed * Time.deltaTime);
            }
        }
    }

    public static void HandleMovementInputTargeted(GameObject character, float verticalAxisVal, bool isRunning, GameObject targetCharacter)
    {
        if(targetCharacter == null)
        {
            Debug.LogError("No Targeted Object Exists!!!");
            return;
        }

        HandleMovementInputTargeted(character, verticalAxisVal, isRunning, targetCharacter.transform.position);
    }

    public static void HandleMovementInput(GameObject character, float horizontalAxisVal, float verticalAxisVal, bool isRunning)
    {
        CharacterSpeed characterSpd = character.GetComponent<CharacterSpeed>();
        
        if (Mathf.Abs(verticalAxisVal) > 0.01f)
        {
            if (isRunning)
            {
                character.transform.position += character.transform.forward * (verticalAxisVal * characterSpd.fRunSpeed * Time.deltaTime);
            }
            else if (Mathf.Abs(verticalAxisVal) > 0.95f)
            {
                character.transform.position += character.transform.forward * (verticalAxisVal * characterSpd.fMoveSpeed * Time.deltaTime);
            }
            else
            {
                character.transform.position += character.transform.forward * (verticalAxisVal * characterSpd.fSlowSpeed * Time.deltaTime);
            }
        }
        else
        {
            verticalAxisVal = 0.0f;
        }

        if (Mathf.Abs(horizontalAxisVal) > 0.01f)
        {
            character.transform.position += character.transform.right * (horizontalAxisVal * characterSpd.fSlowSpeed * Time.deltaTime);
        }
        else
        {
            horizontalAxisVal = 0.0f;
        }
    }

    public static float HandleRotationInputTargeted(GameObject character, Vector3 targetPosition)
    {
        CharacterSpeed characterSpd = character.GetComponent<CharacterSpeed>();

        Vector3 characterForward = character.transform.TransformDirection(Vector3.forward);

        Vector3 toTarget = targetPosition - character.transform.position;

        float angleToTarget = Vector3.Dot(characterForward.normalized, toTarget.normalized);

        float angleInRad = Mathf.Acos(angleToTarget);
        float angleInDeg = angleInRad * Mathf.Rad2Deg;

        if (angleInDeg < 1.0f)
        {
            return angleInDeg;
        }

        character.transform.Rotate(Vector3.up, angleInRad);

        return angleInDeg;
    }

    public static float HandleRotationInputTargeted(GameObject character, GameObject targetCharacter)
    {
        if (targetCharacter == null)
        {
            Debug.LogError("No Targeted Object Exists!!!");
            return float.MinValue;
        }

        return HandleRotationInputTargeted(character, targetCharacter.transform.position);
    }

    public static void HandleRotationInput(GameObject character, float angleHorizontal)
    {
        character.transform.Rotate(Vector3.up, (angleHorizontal * character.GetComponent<CharacterSpeed>().fRotSpeed * Time.deltaTime));
    }
}
