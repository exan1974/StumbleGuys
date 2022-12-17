using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buoyant : MonoBehaviour
{
    public float underwaterDrag = 3f;
    public float underwaterAngularDrag = 1f ;
    public float airDrag = 0f;
    public float airAngularDrag = 0.5f;

    
    private Rigidbody rb;
    public float buoyancyForce = 40;
    private bool hasTouchedWater;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Check if never touched water
        float diffY = transform.position.y;
        bool isUnderwater = diffY < 0;
        if (isUnderwater)
        {
            hasTouchedWater = true;
        }
        // Ignore if never touched water
        if(!hasTouchedWater) return;

        // Buoyancy logic
        if (isUnderwater)
        {
        Vector3 vector = Vector3.up * buoyancyForce * -diffY;
        rb.AddForce(vector, ForceMode.Acceleration);
        }
        rb.drag = isUnderwater ? underwaterDrag : airDrag;
        rb.angularDrag = isUnderwater ? underwaterAngularDrag : airAngularDrag;
    }
}
