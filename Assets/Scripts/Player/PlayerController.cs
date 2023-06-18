using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    

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
   [SerializeField] BoxCollider2D beamCollider;
   public Transform humanReceivePoint;

    bool beamState = false;
    [SerializeField] TextMeshProUGUI beamBattery;
    CinemachineVirtualCamera cam;

    GameCanvas gameCanvas;

    PlayerHealth health;

    [SerializeField] AudioClip ambient,  beamSound;
   
    private void Start()
    {
        
        beam.gameObject.SetActive(false);
        beamBattery.text = "Battery";
        AudioManager.instance.PlaySound(ambient, true);
        
    }
    void Update()
    {
        if(cam == null)
        {
            cam = FindObjectOfType<CinemachineVirtualCamera>();
            cam.Follow = this.gameObject.transform;
            cam.LookAt = this.gameObject.transform;
        }

     
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

       
    }

    public void OnMove(InputValue value)
    {
        rawInput = value.Get<Vector2>();
    }

    public void OnFire(InputValue value)
    {
       
        
        beamState = !beamState;
        beam.gameObject.SetActive(beamState);

        if (beamState)
        {
            AudioManager.instance.PlaySound(beamSound, true); // Pass true as the second argument to indicate looping
        }
        else
        {
            AudioManager.instance.StopSound(beamSound); // Stop the sound when beamState is false
        }
    }
}