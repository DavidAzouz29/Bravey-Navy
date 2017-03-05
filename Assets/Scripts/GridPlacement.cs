///<summary>
/// Author: David Azouz
/// Date Created: 04/03/2017
/// 
/// viewed: 
/// </summary>

using UnityEngine;
using System.Collections;

public class GridPlacement : MonoBehaviour
{
    public enum E_GRID_STATE
    {
        E_GRID_STATE_EMPTY,
        E_GRID_STATE_PLACED,
        E_GRID_STATE_MISS, // didn't hit a thing
        E_GRID_STATE_HIT, // hit has landed on a ship
        E_GRID_STATE_SHIPDEAD, // all hits landed on a ship and it's dead
    }
    private E_GRID_STATE[,] eCurrentGrid;
    public E_GRID_STATE eCurrentGridState;

    public enum E_SHIP_STATE
    {
        E_SHIP_STATE_NOT_PLACED,
        E_SHIP_STATE_PLACED, // 1 is true, can be used for if checks
        E_SHIP_STATE_ALIVE = E_SHIP_STATE_PLACED,
        E_SHIP_STATE_HIT,
        E_SHIP_STATE_DEAD,
    }
    private E_SHIP_STATE eCurrentShipState;
    private Shader cubeState;

    // Use this for initialization
    void Start ()
    {
        eCurrentGrid = new E_GRID_STATE[10, 10];
        /*foreach(E_GRID_STATE grid in eCurrentGrid)
        {
            grid = E_GRID_STATE.E_GRID_STATE_EMPTY;
        }*/
        eCurrentShipState = E_SHIP_STATE.E_SHIP_STATE_NOT_PLACED;
        cubeState = GetComponentInChildren<Shader>();
    }
	
	// Update is called once per frame
	void Update ()
    {

        //TODO: if placed and *clicked*, drag along

	}

    public void CheckSquare()
    {
        switch(eCurrentGridState)
        {
            case E_GRID_STATE.E_GRID_STATE_EMPTY:
            case E_GRID_STATE.E_GRID_STATE_MISS:
                {
                    cubeState = Grid.Instance.gridEmpty.shader;
                    break;
                }
            case E_GRID_STATE.E_GRID_STATE_PLACED:
            case E_GRID_STATE.E_GRID_STATE_SHIPDEAD:
                {
                    cubeState = Grid.Instance.gridPlaced.shader;
                    break;
                }
            case E_GRID_STATE.E_GRID_STATE_HIT:
                {
                    cubeState = Grid.Instance.gridHit.shader;
                    break;
                }
            default:
                {
                    break;
                }
        }
    }
}
