using UnityEngine;
using System.Collections.Generic;
//using Random = UnityEngine.Random;

namespace Flocking
{
    class BoidManager : MonoBehaviour
    {
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        // * Public Instance Variables { Class Variables}
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        //public Texture2D texture;
        public Boid[] ships = new Boid[3];
        //public Random rng = new Random();

        List<Boid> boids = new List<Boid>();
        float spawnFrequency = 0.05f;
        float spawnCount = float.MaxValue;
        Rect screenRect;

        // Part of our game manager's Game Object that doesn't destroy 
        // - therefore we don't want to create an instance
        private static BoidManager _instance;
        public static BoidManager Instance
        {
            get
            {
                if (_instance != null)
                {
                    return _instance;
                }
                // If we're null
                BoidManager boidManager = FindObjectOfType<BoidManager>();
                if (boidManager != null)
                {
                    _instance = boidManager;
                }
                return _instance;
            }
        }

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        // ** Constructor
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        public void Start()
        {
            //texture = _cm.Load<Texture2D>("Boid");
            screenRect = new Rect(0, 0, Screen.width, Screen.height);
            for (int i = 0; i <= ships.Length - 1; ++i)
            {
                Vector3 position = new Vector3(Random.Range(-10.0f, 10.0f), 0, Random.Range(-10.0f, 10.0f));
                Boid boid = (Boid)Instantiate(ships[i], position, Quaternion.identity);
                boids.Add(boid);
            }
        }

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        // * Class Function
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        public void Update()
        {
            spawnCount += Time.deltaTime;
            if (Input.GetMouseButtonDown(0) && spawnCount > spawnFrequency)
            {
                //Vector3 mousePos = Camera.current.ScreenToWorldPoint(new Vector3(100, 100, Camera.current.nearClipPlane));
                //boids.Add(ships[Random.Range(0, ships.Length -1)]);
                Vector3 position = new Vector3(Random.Range(-10.0f, 10.0f), 0, Random.Range(-10.0f, 10.0f));
                Boid boid = (Boid)Instantiate(ships[Random.Range(0, ships.Length - 1)], position, Quaternion.identity);
                boids.Add(boid);
                spawnCount = 0;
            }

            for (int i = 0; i < boids.Count; ++i)
            {
                boids[i].FixedUpdate();
                if(!screenRect.Contains(new Vector2((int)boids[i].transform.position.x, (int)boids[i].transform.position.y)))
                    boids.RemoveAt(i--);
            }
        }

        /*public void Draw()
        {
            _sb.Begin();

            for (int i = 0; i < boids.Count; ++i)
                boids[i].Draw(_sb);

            _sb.End();
        }*/

        public Boid[] FindNeighbours(Boid _of)
        {
            List<Boid> neighbours = new List<Boid>();

            for (int i = 0; i < boids.Count; ++i)
            {
                if (boids[i] == _of)
                    continue;

                Vector2 vec = _of.transform.position - boids[i].transform.position;
                float dist = vec.magnitude;

                if (dist < _of.radius)
                {
                    neighbours.Add(boids[i]);
                }
            }

            return neighbours.ToArray();
        }

        /*void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(_of.transform.position, boids[i].transform.position);
            Gizmos.DrawWireSphere(vec, _of.radius);
        }*/
    }
}
