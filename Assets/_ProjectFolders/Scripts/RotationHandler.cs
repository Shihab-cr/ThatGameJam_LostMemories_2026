using UnityEngine;

public class RotationHandler : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 2f;
    [SerializeField] private Vector3 rotationAxis = Vector3.up; // Default rotation axis is Y-axis
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotationAxis, rotationSpeed * Time.deltaTime);
    }
}
