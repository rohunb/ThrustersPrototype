﻿using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour {

    public enum ControlModes { Keyboard, Mouse, Hydra };
    public ControlModes controlMode;

    //mouse control variables
    private bool stablizing = false;
    private Vector2 mousePos;
    public Vector2 mouseDeadZone = new Vector2(0.0f, 0.0f);


    //hydra game object
    SixenseInput hydraInput;

    //ship components to send inputs to
    ShipMove shipMove;
    ShipAttack shipAttack;
    PlayerInventory playerInventory;

	// Use this for initialization
	void Awake () {
        hydraInput = GameObject.FindGameObjectWithTag("HydraInput").GetComponent<SixenseInput>();
        shipMove = gameObject.GetComponent<ShipMove>();
        shipAttack = gameObject.GetComponent<ShipAttack>();
        playerInventory = gameObject.GetComponent<PlayerInventory>();
        switch (controlMode)
        {
            case ControlModes.Keyboard:
                hydraInput.enabled = false;
                break;
            case ControlModes.Mouse:
                hydraInput.enabled = false;
                break;
            case ControlModes.Hydra:
                hydraInput.enabled = true;
                break;
            default:
                break;
        }
	}
    
	// Update is called once per frame
	void Update () {
        //motion control
        float inputXOne = 0;
        float inputYOne = 0;
        float inputXTwo = 0;
        float inputYTwo = 0;

        //motion inputs
        float currLeftX, currRightX;

        switch (controlMode)
        {
            case ControlModes.Hydra:
                //motion input code
                //warning ... contains hacks and magic number. will fix. ... I promise -A
                if (SixenseInput.Controllers[0].Enabled)
                {
                    inputXOne = SixenseInput.Controllers[0].JoystickX;
                    inputYOne = SixenseInput.Controllers[0].JoystickY;
                }

                if (inputXOne > 0.5f)
                {
                    shipMove.StrafeRight(1f);
                }

                if (inputXOne < -0.5f)
                {
                    shipMove.StrafeLeft(1f);
                }

                if (SixenseInput.Controllers[0].Trigger == 1) //left trigger
                {
                    shipMove.MoveBack(1f);
                }

                if (SixenseInput.Controllers[1].Trigger == 1) //right trigger
                {
                    shipMove.MoveFoward(1f);
                }

                if (SixenseInput.Controllers[0].GetButtonDown(SixenseButtons.BUMPER))
                {
                    shipMove.MoveDown(1f);
                }

                if (SixenseInput.Controllers[1].GetButtonDown(SixenseButtons.BUMPER))
                {
                    shipMove.MoveUp(1f);
                }

                if (SixenseInput.Controllers[0].Rotation.x < -0.25f || SixenseInput.Controllers[1].Rotation.x < -0.25f)
                {
                    shipMove.PitchUp(1f);
                }

                if (SixenseInput.Controllers[0].Rotation.x > 0.25f || SixenseInput.Controllers[1].Rotation.x > 0.25f)
                {
                    shipMove.PitchDown(1f);
                }

                if (SixenseInput.Controllers[0].Rotation.y < -0.25f || SixenseInput.Controllers[1].Rotation.y < -0.25f)
                {
                    shipMove.TurnLeft(1f);
                }

                if (SixenseInput.Controllers[0].Rotation.y > 0.25f || SixenseInput.Controllers[1].Rotation.y > 0.25f)
                {
                    shipMove.TurnRight(1f);
                }

                if (SixenseInput.Controllers[0].Rotation.z < -0.25f || SixenseInput.Controllers[1].Rotation.z < -0.25f)
                {
                    shipMove.RollRight(1f);
                }

                if (SixenseInput.Controllers[0].Rotation.z > 0.25f || SixenseInput.Controllers[1].Rotation.z > 0.25f)
                {
                    shipMove.RollLeft(1f);
                }

                bool controller1Stable, controller2Stable;

                controller1Stable = (SixenseInput.Controllers[0].Rotation.x < 0.2f && SixenseInput.Controllers[0].Rotation.y < 0.2f && SixenseInput.Controllers[0].Rotation.z < 0.2f);
                controller2Stable = (SixenseInput.Controllers[1].Rotation.x < 0.2f && SixenseInput.Controllers[1].Rotation.y < 0.2f && SixenseInput.Controllers[1].Rotation.z < 0.2f);

                if (controller2Stable && controller1Stable)
                {
                    shipMove.KillLinearVelocity();
                }

                //move a little bit in Bkg space
                //BkgCamera.position = (transform.position / 2000f);
                break;
            case ControlModes.Mouse:
                UniversalKeyboardControls();
                mousePos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
                if (!stablizing)
                {
                    if (mousePos.x > 0.5f + mouseDeadZone.x)
                    {
                        //Debug.Log("TurnRight: " + (mousePos.x - 0.5f - mouseDeadZone.x) * (1 / (.5f - mouseDeadZone.x)));
                        shipMove.TurnRight((mousePos.x - 0.5f - mouseDeadZone.x) * (1 / (.5f - mouseDeadZone.x)));
                        //TurnRight(mousePos.x);
                    }
                    if (mousePos.x < 0.5f - mouseDeadZone.x)
                    {
                       // Debug.Log("TurnLeft: " + (Mathf.Abs(mousePos.x - 0.5f) - mouseDeadZone.x) * (1 / (.5f - mouseDeadZone.x)));
                        shipMove.TurnLeft((Mathf.Abs(mousePos.x - 0.5f) - mouseDeadZone.x) * (1 / (.5f - mouseDeadZone.x)));
                        //TurnLeft(Mathf.Abs(mousePos.x - 0.5f)+.5f);
                    }
                    if (mousePos.y > 0.5f + mouseDeadZone.y)
                    {
                       // Debug.Log("PitchUp: " + (mousePos.y - 0.5f - mouseDeadZone.y) * (1 / (.5f - mouseDeadZone.y)));
                        shipMove.PitchUp((mousePos.y - 0.5f - mouseDeadZone.y) * (1 / (.5f - mouseDeadZone.y)));
                        //PitchUp(mousePos.y);
                    }
                    if (mousePos.y < 0.5f - mouseDeadZone.y)
                    {
                       // Debug.Log("PitchDown: " + (Mathf.Abs(mousePos.y - 0.5f) - mouseDeadZone.y) * (1 / (.5f - mouseDeadZone.y)));
                        shipMove.PitchDown((Mathf.Abs(mousePos.y - 0.5f) - mouseDeadZone.y) * (1 / (.5f - mouseDeadZone.y)));
                        //PitchDown(Mathf.Abs(mousePos.y - 0.5f)+ .5f);
                    }
                }
                if (Input.GetKey(KeyCode.Q))
                {
                    shipMove.RollLeft(1f);
                }
                else if (Input.GetKey(KeyCode.E))
                {
                    shipMove.RollRight(1f);
                }
                break;
            case ControlModes.Keyboard:
                UniversalKeyboardControls();
                if (Input.GetKey(KeyCode.Q))
                {
                    shipMove.TurnLeft(1f);
                }
                else if (Input.GetKey(KeyCode.E))
                {
                    shipMove.TurnRight(1f);
                }
                if (Input.GetKey(KeyCode.U))
                    shipMove.MoveUp(1f);
                if (Input.GetKey(KeyCode.H))
                    shipMove.MoveDown(1f);
                if (Input.GetKey(KeyCode.J))
                    shipMove.RollLeft(1f);
                if (Input.GetKey(KeyCode.L))
                    shipMove.RollRight(1f);
                if (Input.GetKey(KeyCode.I))
                    shipMove.PitchDown(1f);
                if (Input.GetKey(KeyCode.K))
                    shipMove.PitchUp(1f);
                break;
            
            default:
                break;
        }
	}
    private void UniversalKeyboardControls()
    {
        float moveV = Input.GetAxis("Vertical");
        float moveH = Input.GetAxis("Horizontal");
        if (moveV > 0.0f)
        {
            shipMove.MoveFoward(moveV);

        }
        else if (moveV < 0.0f)
        {
            shipMove.MoveBack(Mathf.Abs(moveV));
        }
        if (moveH > 0.0f)
        {
            shipMove.StrafeRight(moveH);
        }
        else if (moveH < 0.0f)
        {
            shipMove.StrafeLeft(Mathf.Abs(moveH));
        }
        if (Input.GetKey(KeyCode.U))
            shipMove.MoveUp(1f);
        if (Input.GetKey(KeyCode.H))
            shipMove.MoveDown(1f);
        if (Input.GetKey(KeyCode.Space))
            shipMove.KillLinearVelocity();
        if (Input.GetKey(KeyCode.R))
        {
            stablizing = true;
            shipMove.KillAngularVelocity();
        }
        else
        {
            stablizing = false;
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            shipMove.FireAfterburner();
            
        }
        else
        {
            shipMove.StopFiringAfterburners();
        }

        //weapon inputs
        if (Input.GetButton("Fire1"))
        {
            shipAttack.FirePrimary();
        }
        if (Input.GetButton("Fire2"))
        {
            shipAttack.FireSecondary();
        }
        if (Input.GetKey(KeyCode.Alpha1))
        {
            shipAttack.FireTertiary();
        }
        //mining laser is a constant beam 
        if (Input.GetKey(KeyCode.Alpha2))
        {
            shipAttack.FireUtility();
        }
        else
        {
            shipAttack.StopFiringUtility();
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            shipAttack.TargetNextEnemy();
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            shipAttack.TargetNearestEnemy();
        }
        if(Input.GetKeyDown(KeyCode.I))
        {
            playerInventory.ToggleInventory();
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            playerInventory.ToggleMissionLog();
        }
    }
}