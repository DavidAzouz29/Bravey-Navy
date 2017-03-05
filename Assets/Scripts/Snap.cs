///<summary>
/// Author: David Azouz
/// Date Created: 04/03/2017
/// 
/// viewed: https://forum.unity3d.com/threads/in-game-snap-to-grid.77029/
/// </summary>

using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class Snap : MonoBehaviour
{
    [Header("Snaps ships to grid.")]
    public Vector2 v2ScaleFactor;
    //public float fHalfWay = 0.5f;

    [SerializeField]
    private Vector3 v3currentPos;
    private float posX = 0;
    private float posZ = 0;
	// Use this for initialization
	void Start ()
    {
        //v3currentPos = Vector3.zero;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (transform.tag == "Ship")
        {
            v3currentPos = transform.position;
            posX = Mathf.Round(v3currentPos.x / v2ScaleFactor.x) * v2ScaleFactor.x;
            posZ = Mathf.Round(v3currentPos.z / v2ScaleFactor.y) * v2ScaleFactor.y;
            transform.position = new Vector3(posX, v3currentPos.y, posZ);

            //TODO: delete - used to display correct number on same frame
            v3currentPos = transform.position;
        }
	}
}
