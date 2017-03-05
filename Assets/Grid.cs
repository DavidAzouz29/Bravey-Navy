///<summary>
/// Author: David Azouz
/// Date Created: 04/03/2017
/// 
/// viewed: 
/// </summary>

using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Grid : MonoBehaviour
{
    public Material gridEmpty;
    public Material gridPlaced;
    public Material gridHit;

    private static Grid _instance;
    public static Grid Instance
    {
        get
        {
            // If we have an instance
            if (_instance != null)
            {
                return _instance;
            }
            // If we're null
            // Still need to call this for the build/ first initialisation in Splash.
            Grid grid = FindObjectOfType<Grid>();
            // If we've found an object
            if (grid != null)
            {
                _instance = grid;
                return _instance;
            }
            return _instance;
        }
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
