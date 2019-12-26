using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    

    [SerializeField] float shipMoveSpeed = 5f;


    Rigidbody2D rb2D;
    Vector2 velocity;
    Quaternion previousRotation;

    

    private void Awake() {
        GetComponents();
    }


    private void Update() {
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        velocity = CalculateShipMovement(input);
        Debug.DrawRay(transform.position, transform.up * 10, Color.red);
        
    }
    private void FixedUpdate()
    {

        rb2D.velocity = velocity;

        Quaternion rotation = UpdateLookDirection(true);
        transform.rotation = rotation;
    }

     Quaternion UpdateLookDirection(bool smoothRotation)
    {
        
        if (velocity == Vector2.zero) {
            return previousRotation;
        }
        previousRotation = transform.rotation;
        Quaternion rotation = Quaternion.LookRotation(Vector3.forward, velocity);
        return (smoothRotation) ? Quaternion.Slerp(transform.rotation, rotation, Time.fixedDeltaTime * 10f) : rotation;
    }

    Vector2 CalculateShipMovement(Vector2 input){
        Vector2 velocity = input * shipMoveSpeed;
        return velocity;
    }


    #region Variable References
    void GetComponents(){
        rb2D = GetComponent<Rigidbody2D>();
    }

    #endregion

}
