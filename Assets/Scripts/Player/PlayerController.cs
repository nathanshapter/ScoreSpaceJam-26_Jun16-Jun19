using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] int score = 5;

    Vector2 rawInput;
    Vector3 velocity;
    Vector3 targetPosition;
    Quaternion targetRotation;
    bool isMoving;

    [SerializeField] float moveSpeed;
    [SerializeField] float smoothTime = 0.1f;
    [SerializeField] float rotationAngle = 15f; // Adjust the desired rotation angle
    [SerializeField] float rotationSpeed = 5f; // Adjust the rotation speed

    [SerializeField] private Vector2 clampMin = new Vector2(-5f, -5f); // Define the minimum position values
    [SerializeField] private Vector2 clampMax = new Vector2(5f, 5f); // Define the maximum position values


    [SerializeField] SpriteRenderer beam;
   public Transform humanReceivePoint;
    private void Start()
    {
        beam.gameObject.SetActive(false);
    }
    void Update()
    {
        // Smoothly move the object within the clamped position range
        Vector3 delta = rawInput;
        targetPosition = transform.position + (delta * moveSpeed * Time.deltaTime);
        targetPosition = new Vector3(
            Mathf.Clamp(targetPosition.x, clampMin.x, clampMax.x),
            Mathf.Clamp(targetPosition.y, clampMin.y, clampMax.y),
            transform.position.z
        );

        if (targetPosition != transform.position)
        {
            isMoving = true;

            if (delta.x > 0)
                targetRotation = Quaternion.Euler(0f, 0f, -rotationAngle); // Rotate slightly to the right when moving right
            else if (delta.x < 0)
                targetRotation = Quaternion.Euler(0f, 0f, rotationAngle); // Rotate slightly to the left when moving left
        }
        else if (isMoving)
        {
            isMoving = false;
            targetRotation = Quaternion.identity; // Reset the rotation when stopped
        }

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);

        if (Input.GetKeyDown(KeyCode.F))
        {
            score += 5;
            StartCoroutine(LeaderBoard.instance.SubmitScoreRoutine(score));
        }
    }

    public void OnMove(InputValue value)
    {
        rawInput = value.Get<Vector2>();
    }

    public void OnFire(InputValue value)
    {
        beam.gameObject.SetActive(true);
    }
}