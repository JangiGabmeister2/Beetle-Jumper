using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatePlayer : MonoBehaviour
{
    Animator _playerAnimator;
    SpriteRenderer _renderer;

    //an id for the animator controller's bool parameters
    int isMoving;
    int isJumping;

    private void Start()
    {
        _playerAnimator = GetComponent<Animator>();
        _renderer = GetComponent<SpriteRenderer>();

        //allows the sprite renderer to cast shadows
        _renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;

        //connects the bool id to the animator's bool parameters
        isMoving = Animator.StringToHash("isMoving");
        isJumping = Animator.StringToHash("isJumping");
    }

    private void Update()
    {
        Animate();
    }

    private void Animate()
    {
        //if player presses either forward, backward, left, or right buttons, return true
        bool _isMoving = Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0;
        //if player presses jump button, returns true
        bool _isJumping = Input.GetButton("Jump");

        //flips the sprite's x-rotation according to which direction the player is heading towards
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            _renderer.flipX = true;
        }
        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            _renderer.flipX = false;
        }   

        //sets the values of bool ids as the above bool's values
        _playerAnimator.SetBool(isMoving, _isMoving);
        _playerAnimator.SetBool(isJumping, _isJumping);
    }
}
