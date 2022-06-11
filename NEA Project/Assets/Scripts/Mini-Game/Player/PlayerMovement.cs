using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    float currentSpeed;
    [SerializeField] float maxSpeed;
    [SerializeField] float accelerationRate;
    bool isQuestionPhase;
    bool isEndOfGame;
    [SerializeField] float sideSpeed;
    float distanceTravelled;

    [SerializeField] TextMeshProUGUI distanceTravelledText, speedText;
    [SerializeField] CharacterController charController;

    Vector3 moveVector;
    float altMaxSpeed;

    public float MaxSpeed { get { return altMaxSpeed == 0? maxSpeed : altMaxSpeed; }}
    public float MinSpeed { get { return isQuestionPhase? 0.5f : 0; } }
    public bool IsQuestionPhase { get { return isQuestionPhase; } set { isQuestionPhase = value; } }
    public float DistanceTravelled { get { return distanceTravelled; } }

    void Start()
    {
        moveVector = new Vector3();
    }

    void Update()
    {
        if (!isEndOfGame)
        {
            // If the key to accelerate is being pressed, increase the speed. If not, decrease the speed.
            if (Input.GetKey(KeyCode.LeftShift))
                currentSpeed += accelerationRate * Time.deltaTime;
            else
                currentSpeed -= accelerationRate * Time.deltaTime;

            // currentSpeed is clamped between the min and max speed values
            currentSpeed = Mathf.Clamp(currentSpeed, MinSpeed, MaxSpeed);

            // Right and left motion is taken from player input
            moveVector.x = Input.GetAxis("Horizontal") * sideSpeed * Time.deltaTime;

            // Forward movement is set to the current speed
            moveVector.z = currentSpeed;

            // Distance = speed * time
            distanceTravelled += currentSpeed * Time.deltaTime;

            // Move the player forward by the moveVector
            charController.Move(moveVector);

            // Display the distance travelled and speed to the UI
            distanceTravelledText.text = "Distance travelled : " + ((distanceTravelled > 10) ? distanceTravelled.ToString("00.0") : distanceTravelled.ToString("0.0"));
            speedText.text = "Speed : " + currentSpeed.ToString("0.0");
        }
    }

    public void ChangeMaxSpeed(float speed)
    {
        altMaxSpeed = speed;
    }

    public void ResetMaxSpeed()
    {
        altMaxSpeed = 0;
    }

    public void StopPlayerMovement()
    {
        isEndOfGame = true;
    }
}
