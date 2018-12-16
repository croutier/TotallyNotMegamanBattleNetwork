using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum TileSpites
{
    shot,
    player,
}

public class MultipleSpriteHandler : MonoBehaviour {

    [SerializeField] Sprite emptyTile;
    [SerializeField] Sprite p1Shot;
    [SerializeField] Sprite p2Shot;
    [SerializeField] Sprite player1;
    [SerializeField] Sprite player2;

    public void SelectSprite(TileSpites spriteToDisplay, playerID palyer)
    {
        switch (spriteToDisplay)
        {           
            case TileSpites.shot:
                if(palyer == playerID.player1)
                {
                    gameObject.GetComponent<Image>().sprite = p1Shot;
                }
                else
                {
                    gameObject.GetComponent<Image>().sprite = p2Shot;
                }     
                break;
            case TileSpites.player:
                if (palyer == playerID.player1)
                {
                    gameObject.GetComponent<Image>().sprite = player1;
                }
                else
                {
                    gameObject.GetComponent<Image>().sprite = player2;
                }
                break;    
        }
    }
    public void ResetSprite()
    {
        gameObject.GetComponent<Image>().sprite = emptyTile;
    }
}
