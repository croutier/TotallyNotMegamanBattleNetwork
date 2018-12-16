using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameHandler : MonoBehaviour {


    Player p1;
    Player p2;
    InputHandler iHandler;
    [SerializeField] BoardManager board;
    [SerializeField] Text hp1;
    [SerializeField] Text hp2;
    Clock timer;
    List<ISpecial> displayedSpecials;
    private void Start()
    {
        timer = Clock.Instance;
        timer.Every2Ticks += ExecuteInput;
        timer.OnTick += ActualizateBoard;
        iHandler = GetComponent<InputHandler>();
        displayedSpecials = new List<ISpecial>();
        p1 = new Player(new Vector2Int(1, 1), playerID.player1);
        p2 = new Player(new Vector2Int(4, 1), playerID.player2);
    }
    private void Update()// Funcion de testeo hasta implementar la seleccion de Specials(en caso de llegar a ser implementado en este prototipo)
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Added a special attack for testing");
            p1.AddSpecial(new SpecialAttack("DebugSpecial", 20, 1, 1, 0, 0, 2));
            p2.AddSpecial(new SpecialAttack("DebugSpecial", 20, 1, 1, 0, 0, 2));
        }
    }
    private void ExecuteInput()
    {
        ReciveInput(iHandler.Player1Input,p1);
        ReciveInput(iHandler.Player2Input,p2);
        iHandler.ResetInputs();
        if (p1.shoots && p1.GetPos.y == p2.GetPos.y)
            p2.ReciveDamage(3);
        if (p2.shoots && p1.GetPos.y == p2.GetPos.y)
            p1.ReciveDamage(3);
        p1.shoots = false;
        p2.shoots = false;
    }
    private void ActualizateBoard()
    {
        board.ResetBoard();
        board.GetSlot(p1.GetPos.x, p1.GetPos.y).SelectSprite(TileSpites.player,playerID.player1);
        board.GetSlot(p2.GetPos.x, p2.GetPos.y).SelectSprite(TileSpites.player, playerID.player2);
        DisplaySpecial();
        CheckHits();
        hp1.text = p1.CurrentHp.ToString();
        hp2.text = p2.CurrentHp.ToString();
        p1.ReduceInvTime();
        p2.ReduceInvTime();
    }

    private void CheckHits()
    {
        for (int i = 0; i < displayedSpecials.Count; i++)
        {

            if (displayedSpecials[i].GetType() == typeof(SpecialAttack))
            {
                Vector2Int[] damageZone = ((SpecialAttack)displayedSpecials[i]).DamageZone;
                for (int j = 0; j < damageZone.Length; j++)
                {
                    if(displayedSpecials[i].Caster != playerID.player1 && p1.GetPos == damageZone[i])
                    {
                        p1.ReciveDamage(((SpecialAttack)displayedSpecials[i]).Damage);
                    }
                    if (displayedSpecials[i].Caster != playerID.player2 && p2.GetPos == damageZone[i])
                    {
                        p2.ReciveDamage(((SpecialAttack)displayedSpecials[i]).Damage);
                    }
                }
            }
        }
    }

    private void DisplaySpecial()
    {
        for (int i = 0; i < displayedSpecials.Count; i++)
        {
           
            if(displayedSpecials[i].GetType() == typeof(SpecialAttack))
            {
                Vector2Int[] damageZone = ((SpecialAttack)displayedSpecials[i]).DamageZone;
                for (int j = 0; j < damageZone.Length; j++)
                {
                    board.GetSlot(damageZone[j].x, damageZone[j].y).SelectSprite(TileSpites.shot,displayedSpecials[i].Caster);
                }
            }                
            
        }
        displayedSpecials.RemoveAll(special => special.Active == false);
    }

    public void ReciveInput(input pInput,Player player)
    {
        if (player.Alive)
        {
            switch (pInput)
            {
                case input.shoot:
                    player.shoots = true;
                    break;
                case input.special:
                    if (player.HasSpecial())
                    {
                        Debug.Log("SPECIAL Shoot!!");
                        ISpecial playerSpecial = player.GetSpecial();
                        playerSpecial.Call(player.GetPos, player.GetID);
                        displayedSpecials.Add(playerSpecial);
                    }
                    break;
                case input.up:
                    player.TryToMove(new Vector2Int(0, -1));
                    break;
                case input.right:
                    player.TryToMove(new Vector2Int(1, 0));
                    break;
                case input.left:
                    player.TryToMove(new Vector2Int(-1, 0));
                    break;
                case input.down:
                    player.TryToMove(new Vector2Int(0, 1));
                    break;

            }
        }        
    }
}
