using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Input;

public class BoxMovement : MonoBehaviour
{
    public InputAction crouchAction;
    public InputAction moveAction;
    public InputBindingCompositeContext x;

    private bool crouchText = false;
    private Vector2 hAxisGamepad;
    private float speed = 3f;

    void Awake()
    {

       // moveAction.AddCompositeBinding("AxisComposite");
        crouchAction.performed += ctx => crouch();
        moveAction.performed += ctx =>  hAxisGamepad = ctx.ReadValue<Vector2>();
     
    }

    void OnEnable()
    {

        crouchAction.Enable();
        moveAction.Enable();
    }

    void Start()
    {

      //  crouchAction.AddBinding("/<keyboard>/sKey");
    }

    void Update()
    {
        moveHorizontal(hAxisGamepad);
    }

    void OnGUI()
    {
        if (crouchText)
        {
            GUI.Label(new Rect(100, 100, 200, 100), "crouching");
        }
        
    }


    void OnDisable()
    {
        crouchAction.Disable();
        moveAction.Disable();
    }


    //--------------------------------------------------------------------------------------------------//

    void crouch() {
        crouchText = true;
        
    }

    void moveHorizontal(Vector2 dir) {

        Vector2 onlyHorizontal = new Vector2(dir.x, 0f);

        transform.Translate(onlyHorizontal * speed * Time.deltaTime);
    }
}
