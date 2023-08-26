using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwable : MonoBehaviour
{
    private Vector2 throwVector;
    private Rigidbody2D rb;

    public float throwSpeed = 100f;
    public float damping = 0.2f;
    public float stopThreshold = 0.1f;

    private Vector3 initialMousePos;
    private float dragDistanceMultiplier = 0.0001f;

    private bool isThrown = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnMouseDown()
    {
        initialMousePos = Input.mousePosition;
    }

    private void OnMouseDrag()
    {
        CalculateThrowVector();
    }

    private void CalculateThrowVector()
    {
        Vector3 currentMousePos = Input.mousePosition;
        float dragDistance = (currentMousePos - initialMousePos).magnitude;
        throwVector = -1f * (rb.transform.position - Camera.main.ScreenToWorldPoint(currentMousePos)).normalized * (throwSpeed + dragDistance * dragDistanceMultiplier);
    }

    private void OnMouseUp()
    {
        Throw();
    }

    private void Update()
    {
        rb.velocity *= (1f - damping * Time.deltaTime);

        if (rb.velocity.magnitude < stopThreshold)
        {
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }
    }

    public void Throw()
    {
        rb.AddForce(-throwVector);
        isThrown = true;
    }

    public bool IsThrown()
    {
        return isThrown;
    }

    public bool IsStoppedMoving()
    {
        return rb.velocity.magnitude < stopThreshold;
    }
}
