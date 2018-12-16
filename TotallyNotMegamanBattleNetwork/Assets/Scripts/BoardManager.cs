using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardManager : MonoBehaviour {

    MultipleSpriteHandler[,] tilesSprites;
    GridLayoutGroup grid;
    GameObject parent;
    [SerializeField] GameObject tilePrefab;
    [SerializeField] int col;
    [SerializeField] int row;
    [SerializeField] float separation;

    private void Start()
    {
        grid = GetComponent<GridLayoutGroup>();
        CreateBoard();
    }
    

    private void CreateBoard()
    {

        Vector2 cellSize = new Vector2((GetComponent<RectTransform>().rect.width - (separation * (col - 1))) / col, (GetComponent<RectTransform>().rect.height - (separation * (row - 1))) / row);
        grid.cellSize = cellSize;
        tilesSprites = new MultipleSpriteHandler[col,row];
        for (int i = 0; i < col; i++)
        {
            for (int j = 0; j < row; j++)
            {
                GameObject instance = Instantiate(tilePrefab);
                instance.transform.SetParent(transform);
                tilesSprites[i,j] = instance.GetComponent<MultipleSpriteHandler>();                
            }
        }
        
    }
    public MultipleSpriteHandler GetSlot(int col,int row)
    {
        return tilesSprites[col,row];
    }
    public void ResetBoard()
    {
        for (int i = 0; i < tilesSprites.GetLength(0); i++)
        {
            for (int j = 0; j < tilesSprites.GetLength(1); j++)
            {
                tilesSprites[i, j].ResetSprite();
            }
        }
    }

}
