using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Assignables")]
    public Rigidbody2D rb;

    [Header("Movement")]
    public float moveSpeed = 35;
    public float counterMovement = 13;

    //Data
    private Vector2 input;

    //Setup components if they haven't been assigned
    private void Awake(){
        if(rb == null) rb = GetComponent<Rigidbody2D>();
    }

    private void Update(){
        //Get the player input
        input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
    }

    private void FixedUpdate(){
        //General values used for movement
        float vel = rb.velocity.magnitude;
        float maxSpeed = moveSpeed * 1.5f;

        //Cancel out input if moving faster than maxSpeed in a direction
        if(input.x > 0 && rb.velocity.x > maxSpeed) input.x = 0;
        if(input.x < 0 && rb.velocity.x < -maxSpeed) input.x = 0;
        if(input.y > 0 && rb.velocity.y > maxSpeed) input.y = 0;
        if(input.y < 0 && rb.velocity.y < -maxSpeed) input.y = 0;

        //Extra force to be applied if wanting to move one way and currently going the opposite
        float xBonus = 1f;
        float yBonus = 1f;

        if(input.x < 0 && rb.velocity.x > 0) xBonus = 2;
        if(input.x > 0 && rb.velocity.x < 0) xBonus = 2;
        if(input.y < 0 && rb.velocity.y > 0) yBonus = 2;
        if(input.y > 0 && rb.velocity.y < 0) yBonus = 2;

        //Slight bit extra force just for a better feel
        float extraForce = maxSpeed - vel;

        //Movement forces being applied
        rb.AddForce(input * moveSpeed * extraForce * xBonus * yBonus * 2 * Time.fixedDeltaTime);

        //Countermovement to have a smooth stop
        rb.AddForce(counterMovement * -rb.velocity * moveSpeed * Time.fixedDeltaTime);
    }
}
