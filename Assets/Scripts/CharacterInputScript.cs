using UnityEngine;
using System.Collections;
 
[RequireComponent(typeof(CharacterScript))]
public class CharacterInputScript : MonoBehaviour {
    
    private CharacterScript character;
    private Camera mainCamera;
 
    private Vector2 heading;
 

    private void Awake () 
    {
        heading = Vector2.zero;
    }
 

    private void Start()
    {
        this.character = this.GetComponent<CharacterScript>();
        this.mainCamera = Camera.main;
    }
 

    void Update()
    {
        // TODO specific to touch screen
        /*
        //check for touches
        if (Input.touchCount > 0) {
            //what was the position?
            Vector2 touchPosition = Input.GetTouch(0).position;
            Vector3 touchWorldPosition = this.mainCamera.ScreenToWorldPoint(new Vector3(touchPosition.x, touchPosition.y, 15));
            //where is our character currently?
            Vector3 characterPosition = character.gameObject.transform.position;
            //vector math says point to get to - current position = heading.
            this.heading = new Vector2(touchWorldPosition.x - characterPosition.x, touchWorldPosition.y - characterPosition.y);
            //make sure we don't surpass 1.
            this.heading.Normalize();
        }
        */
    }
 


    /*
    ========================
    FixedUpdate
    ========================
    */
    // FixedUpdate is used instead of Update when dealing with Rigidbody.
    void FixedUpdate()
    {
        Vector2 movement = new Vector2();

        // Can move with keyboard or mouse, mouse being the priority
        if ( Input.GetButton("Fire1") ) {
            Vector2 currentPosition = transform.position;
            Vector2 moveToward = Camera.main.ScreenToWorldPoint( Input.mousePosition );
            movement = moveToward - currentPosition;
        } else {
            movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        }
        
        this.heading = movement.normalized;
 
        character.Move(heading);

    }
}
