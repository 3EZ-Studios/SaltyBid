﻿using Assets.Scripts.Moves;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Experimental.Input;

public class Character : MonoBehaviour
{
    public float speed = 3f;
    private Rigidbody2D rigidBody;
    private bool grounded = false;
    private MockInputBuffer inputBuffer;
    private InputBuffer experimentalInputBuffer;

    public InputAction movementAction;

    public Character Opponent { get; private set; }
    public State CurrentState { get; private set; } = State.NEUTRAL;
    public double Health { get; set; } = 100;
    public double Guard { get; set; } = 100;

    private Move currentMove;
    public Move CurrentMove {
        get {
            return currentMove;
        }
        set {
            // Only allow the move to be set if no move is in progress, and only allow the move to
            // be cleared if a move is in progress
            if (currentMove == null ^ value == null)
            {
                currentMove = value;
            }
            else
            {
                Debug.LogError($"Unexpected change to CurrentMove " +
                    $"OldVal: {currentMove} NewVal: {value}");
            }
        }
    }

    void OnDisable()
    {
        movementAction.Disable();   
    }

    void OnEnable()
    {
        movementAction.Enable();
    }

    void Awake()
    {

        movementAction.performed += ctx =>
        {
            Vector2 rawValue = ctx.ReadValue<Vector2>();
            experimentalInputBuffer.fifo.Enqueue(rawValue);
            String consumable = experimentalInputBuffer.VectorToConsumable(rawValue);
            experimentalInputBuffer.consumables.Enqueue(consumable);

        };

    }

    // Start is called before the first frame update
    void Start()
    {
        Opponent = FindObjectsOfType<Character>().Where(c => c != this).Single();
        experimentalInputBuffer = new InputBuffer(); 
        inputBuffer = GetComponent<MockInputBuffer>();

        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        Vector2 result;
        if (experimentalInputBuffer.fifo.TryDequeue(out result))
        {
            doMovement(result);
        }

        if (!grounded)
        {
            CurrentState = State.MIDAIR;
        }
        else if (inputBuffer?.Peek(KeyCode.A) ?? false)
        {
            CurrentState = State.BACK_WALK;
            rigidBody.velocity = new Vector3(-1 * speed, rigidBody.velocity.y, 0);
        }
        else if (inputBuffer?.Peek(KeyCode.D) ?? false)
        {
            CurrentState = State.FORWARD_WALK;
            rigidBody.velocity = new Vector3(speed, rigidBody.velocity.y, 0);
        }
        else
        {
            CurrentState = State.NEUTRAL;
        }

        if ((inputBuffer?.Match(KeyCode.Space) ?? false) && CurrentState != State.MIDAIR)
        {
            rigidBody.AddForce(new Vector3(0, 300, 0));
        }
    }

    private void doMovement(Vector2 v)
    {
        String r2;
        if (experimentalInputBuffer.consumables.TryDequeue(out r2))
        {
            Debug.Log($"{v.x},{v.y} @ {Time.time} with State: {r2}");
        }

        rigidBody.velocity = new Vector3(speed * v.x , speed * v.y, 0);

    }


    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag.Equals("Ground"))
        {
            grounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag.Equals("Ground"))
        {
            grounded = false;
        }
    }

    public void setHitboxState(HitboxPath hitboxPath, bool state)
    {
        Hitbox hitbox = transform.Find(hitboxPath.Path)?.gameObject?.GetComponent<Hitbox>();
        if (hitbox == null)
        {
            Debug.LogError($"Unknown hitbox path: {hitboxPath}");
        }
        else
        {
            hitbox.Active = state;
        }
    }

    public enum State
    {
        NEUTRAL,
        MIDAIR,
        BACK_WALK,
        FORWARD_WALK
    };

    public class HitboxPath
    {
        public string Path { get; private set; }

        private HitboxPath(string path)
        {
            Path = path;
        }

        public static HitboxPath UPPER_BODY = new HitboxPath("AnimationControl/Hitboxes/UpperBody");
        public static HitboxPath HEAD = new HitboxPath("AnimationControl/Hitboxes/UpperBody/Head");
        public static HitboxPath LEFT_UPPER_ARM = new HitboxPath("AnimationControl/Hitboxes/UpperBody/LeftArm/UpperLeftArm");
        public static HitboxPath LEFT_LOWER_ARM = new HitboxPath("AnimationControl/Hitboxes/UpperBody/LeftArm/UpperLeftArm/LowerLeftArm");
        public static HitboxPath LEFT_HAND = new HitboxPath("AnimationControl/Hitboxes/UpperBody/LeftArm/UpperLeftArm/LowerLeftArm/LeftHand");
        public static HitboxPath RIGHT_UPPER_ARM = new HitboxPath("AnimationControl/Hitboxes/UpperBody/RightArm/UpperRightArm");
        public static HitboxPath RIGHT_LOWER_ARM = new HitboxPath("AnimationControl/Hitboxes/UpperBody/RightArm/UpperRightArm/LowerRightArm");
        public static HitboxPath RIGHT_HAND = new HitboxPath("AnimationControl/Hitboxes/UpperBody/RightArm/UpperRightArm/LowerRightArm/RightHand");
        public static HitboxPath LOWER_BODY = new HitboxPath("AnimationControl/Hitboxes/LowerBody");
        public static HitboxPath LEFT_UPPER_LEG = new HitboxPath("AnimationControl/Hitboxes/LowerBody/LeftLeg/UpperLeftLeg");
        public static HitboxPath LEFT_LOWER_LEG = new HitboxPath("AnimationControl/Hitboxes/LowerBody/LeftLeg/UpperLeftLeg/LowerLeftLeg");
        public static HitboxPath LEFT_FOOT = new HitboxPath("AnimationControl/Hitboxes/LowerBody/LeftLeg/UpperLeftLeg/LowerLeftLeg/LeftFoot");
        public static HitboxPath RIGHT_UPPER_LEG = new HitboxPath("AnimationControl/Hitboxes/LowerBody/RightLeg/UpperRightLeg");
        public static HitboxPath RIGHT_LOWER_LEG = new HitboxPath("AnimationControl/Hitboxes/LowerBody/RightLeg/UpperRightLeg/LowerRightLeg");
        public static HitboxPath RIGHT_FOOT = new HitboxPath("AnimationControl/Hitboxes/LowerBody/RightLeg/UpperRightLeg/LowerRightLeg/RightFoot");
    }
}
