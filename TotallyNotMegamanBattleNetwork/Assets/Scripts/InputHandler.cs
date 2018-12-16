using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum input
{
    none,
    up,
    down,
    right,
    left,
    shoot,
    special    
}

public class InputHandler : MonoBehaviour {

    input player1Input;
    public input Player1Input {get{return player1Input;}}
    input player2Input;
    public input Player2Input { get { return player2Input; } }
    [SerializeField] KeyCode player1Up;
    [SerializeField] KeyCode player1Right;
    [SerializeField] KeyCode player1Left;
    [SerializeField] KeyCode player1Down;
    [SerializeField] KeyCode player1Shoot;
    [SerializeField] KeyCode player1Special;
                               
    [SerializeField] KeyCode player2Up;
    [SerializeField] KeyCode player2Right;
    [SerializeField] KeyCode player2Left;
    [SerializeField] KeyCode player2Down;
    [SerializeField] KeyCode player2Shoot;
    [SerializeField] KeyCode player2Special;

    private void Update()
    {
        player1Input = GetPlayer1Inputs();
        player2Input = GetPlayer2Inputs();
    }

    public void ResetInputs()
    {
        player1Input = input.none;
        player2Input = input.none;
    }

    private input GetPlayer1Inputs()
    {
        if (Input.GetKeyDown(player1Shoot))
        {
            return input.shoot;
        }
        if (Input.GetKeyDown(player1Special))
        {
            return input.special;
        }
        if (Input.GetKeyDown(player1Up))
        {
            return input.up;
        }
        if (Input.GetKeyDown(player1Right))
        {
            return input.right;
        }
        if (Input.GetKeyDown(player1Left))
        {
            return input.left;
        }
        if (Input.GetKeyDown(player1Down))
        {
            return input.down;
        }   
        return player1Input;
    }
    private input GetPlayer2Inputs()
    {
        if (Input.GetKeyDown(player2Shoot))
        {
            return input.shoot;
        }
        if (Input.GetKeyDown(player2Special))
        {
            return input.special;
        }
        if (Input.GetKeyDown(player2Up))
        {
            return input.up;
        }
        if (Input.GetKeyDown(player2Right))
        {
            return input.right;
        }
        if (Input.GetKeyDown(player2Left))
        {
            return input.left;
        }
        if (Input.GetKeyDown(player2Down))
        {
            return input.down;
        }       

        return player2Input;
    }
}
