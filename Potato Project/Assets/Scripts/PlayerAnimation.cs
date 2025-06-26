using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FSR
{

    public class PlayerAnimation : MonoBehaviour
    {

        private Animator Anim = null;
        private string currentState;
        private Player P = null;

        private float xAxis;
        private float yAxis;
        private Rigidbody2D rb2d;
        private bool isJumpPressed;
        private int groundMask;
        private bool isAulosPressed;
        private bool isAulosPlaying;
        private bool canLand;

        //Animation States
        const string PLAYER_IDLE = "IdleAnim";
        const string PLAYER_WALK_L = "WalkingAnimL";
        const string PLAYER_WALK_R = "WalkingAnimR";
        const string PLAYER_DASH = "DashAnim";
        const string PLAYER_JUMP_L = "JumpAnimL";
        const string PLAYER_JUMP_R = "JumpAnimR";
        const string PLAYER_AULOS = "AulosAnim";
        const string PLAYER_LAND_L = "LandingAnimL";
        const string PLAYER_LAND_R = "LandingAnimR";
        const string PLAYER_FALL_L = "FallingAnimL";
        const string PLAYER_FALL_R = "FallingAnimR";

        //[SerializeField] private FSR_Player fSR_Player;

        //Animation Durration
        private float aulosTime;
        private float landingTime;

        // Start is called before the first frame update
        void Start()
        {
            rb2d = GetComponent<Rigidbody2D>();
            Anim = GetComponent<Animator>();
            P = GetComponent<Player>();
            //groundMask = 1 << LayerMask.NameToLayer("Ground");
            //conncet to player's animator component
        }

        void ChangeAnimationState(string newState)
        {
            //stop the same animation from interrupting itself
            if (currentState == newState)
            {
                return;
            }

            //play the animation
            Anim.Play(newState);

            //reassign the current state
            currentState = newState;

        }
        // Update is called once per frame
        void Update()
        {
            if (!P.inDialog)
            {
                //Checking directional inputs
                xAxis = Input.GetAxisRaw("Horizontal");

                //Checking Jump input
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    isJumpPressed = true;
                }

                //Checking Aulos
                if (P.aulosAbility && Input.GetKeyDown("f"))
                {
                        isAulosPressed = true;
                }
            }
            else
                xAxis = 0;
        }

        private void FixedUpdate()
        {
            if (P.inDialog)
                ChangeAnimationState(PLAYER_IDLE);
            else
            {
                if (P.isGrounded() && !P.isDashing && !isAulosPlaying && rb2d.velocity.y == 0f && !canLand)
                {
                    
                    if (xAxis > 0)
                    {
                        ChangeAnimationState(PLAYER_WALK_R);
                        //fSR_Player.step();
                    }
                    else if (xAxis < 0)
                    {
                        ChangeAnimationState(PLAYER_WALK_L);
                        //fSR_Player.step();
                    }
                    else
                    {
                        ChangeAnimationState(PLAYER_IDLE);
                    }
                }

                if (P.isDashing)
                {
                    ChangeAnimationState(PLAYER_DASH);
                }

                //-------------------------------------
                //Check player trying to jump
                /*if (isJumpPressed) 
                {
                    isJumpPressed = false;
                }*/

                if (isJumpPressed && !P.faceRight && /*!P.isDashing*/ rb2d.velocity.y >= 0f)
                {
                    ChangeAnimationState(PLAYER_JUMP_L);
                    isJumpPressed = false;
                }

                else if (isJumpPressed && P.faceRight && /*!P.isDashing*/ rb2d.velocity.y >= 0f) 
                {
                    
                    ChangeAnimationState(PLAYER_JUMP_R);
                    isJumpPressed = false;
                }

               
                //Check player trying to attack
                /*if ((isAulosPressed && P.isGrounded()) && !P.isDashing)
                {
                    isAulosPressed = false;
                    if (!isAulosPlaying)
                    {

                        isAulosPlaying = true;
                        ChangeAnimationState(PLAYER_AULOS);
                        aulosTime = Anim.GetCurrentAnimatorStateInfo(0).length;
                        Invoke("AulosComplete", aulosTime); 

                    }
                }*/

                if (rb2d.velocity.y < 0f && !P.faceRight && !P.isDashing)
                {
                    ChangeAnimationState(PLAYER_FALL_L);
                    canLand = true;   
                }
                else if (rb2d.velocity.y < 0f && P.faceRight && !P.isDashing)
                {
                    ChangeAnimationState(PLAYER_FALL_R);
                    canLand = true;   
                }

                if (!P.faceRight && canLand && rb2d.velocity.y == 0f && !P.isDashing)
                {
                    bool isLanding = false;
                    if (!isLanding)
                    {
                        isLanding = true;
                        ChangeAnimationState(PLAYER_LAND_L);
                        landingTime = Anim.GetCurrentAnimatorStateInfo(0).length;
                        Invoke("LandingComplete", landingTime); 
                    }
                }

                if (P.faceRight && canLand && rb2d.velocity.y == 0f && !P.isDashing)
                {
                    bool isLanding = false;
                    if (!isLanding)
                    {
                        isLanding = true;
                        ChangeAnimationState(PLAYER_LAND_R);
                        landingTime = Anim.GetCurrentAnimatorStateInfo(0).length;
                        Invoke("LandingComplete", landingTime); 
                    }
                }
            }
        }

        void AulosComplete()
        {
            isAulosPlaying = false;
        }

        void LandingComplete()
        {
            canLand = false; 
        }
    }
}