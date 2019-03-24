using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Input;

public class InputSystem : MonoBehaviour
{

    public InputAction movementAction;
    public InputAction keyboardMovementAction;

    private Buffer experimentalInputBuffer;

    private Rigidbody2D rigidBody;
    public float speed = 3f;

    // Start is called before the first frame update
    void Start()
    {
        experimentalInputBuffer = GetComponent<Buffer>();
        rigidBody = GetComponent<Rigidbody2D>();
    }

    void OnDisable()
    {
        movementAction.Disable();
        keyboardMovementAction.Disable();
    }

    void OnEnable()
    {
        movementAction.Enable();
        keyboardMovementAction.Enable();
    }

    void Awake()
    {

        movementAction.performed += ctx =>
        {
            Vector2 rawValue = ctx.ReadValue<Vector2>();
            experimentalInputBuffer.fifo.Enqueue(rawValue);
            String consumable = experimentalInputBuffer.VectorToConsumable(rawValue);
            experimentalInputBuffer.pushConsumable(consumable);

        };

        keyboardMovementAction.performed += ctx => experimentalInputBuffer.fifo.Enqueue(ctx.ReadValue<Vector2>());

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 result;
        if (experimentalInputBuffer.fifo.TryDequeue(out result))
        {
            doMovement(result);
        }
    }

    private void doMovement(Vector2 v)
    {
        String r2;
        if (experimentalInputBuffer.consumables.TryDequeue(out r2))
        {
            Debug.Log($"{v.x},{v.y} @ {Time.time} with State: {r2}. Capacity: {experimentalInputBuffer.consumables.Count}");
        }

        rigidBody.velocity = new Vector3(speed * v.x, speed * v.y, 0);

    }
}
