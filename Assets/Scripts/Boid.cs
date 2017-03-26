using UnityEngine;
using System;
//using System.Double;
using Random = UnityEngine.Random;

namespace Flocking
{
    [RequireComponent(typeof(Rigidbody))]
    class Boid : MonoBehaviour
    {
        public new Transform transform;
        public Rigidbody rb;
        //public Vector3 velocity = Vector3.zero;
        public Vector2 heading = Vector2.zero;
        public float rotation = 0;
        public float radius = 70;
        public float maxVelocity = 50;

        BoidManager boidManager;

        //http://docs.oracle.com/javase/7/docs/api/java/util/Random.html#nextDouble()
        /*public double NextDouble()
        {
            return (((long)next(26) << 27) + next(27))
              / (double)(1L << 53);
        } */

        public void Start()
        {
            boidManager = BoidManager.Instance;
            transform = GetComponent<Transform>();
            rb = GetComponent<Rigidbody>();

            transform.SetParent(BoidManager.Instance.transform);
            float offset = 10.0f;
            transform.position = new Vector3(Random.Range(-offset, offset), transform.parent.position.y, Random.Range(-offset, offset));

            float x = Random.Range(0.0f, 1.0f) * 2 - 1;
            float z = Random.Range(0.0f, 1.0f) * 2 - 1;
            rb.velocity = new Vector3(x, 0, z) * maxVelocity;
        }

        /*public Boid(BoidManager _bm, Vector3 _position)
        {
            //GameObject go = new GameObject();
            //go.AddComponent<Boid>();
            //go.transform.SetParent(_bm.transform);
        }*/

        /*public void Spawn(BoidManager _bm, Vector3 _position)
        {
            transform.position = _position;

            //random velocity to start with
            //(float)NextDouble()

            float x = Random.Range(0.0f, 1.0f) * 2 - 1;
            float z = Random.Range(0.0f, 1.0f) * 2 - 1;
            rb.velocity = new Vector3(x, 0, z) * maxVelocity;
        }*/

        public void FixedUpdate()
        {
            //get neighbours
            Boid[] neighbours = boidManager.FindNeighbours(this);

            //if we have at least 1 neighbour
            if (neighbours.Length > 0)
            {
                //add all forces together and normalize
                Vector3 force = Vector3.zero;
                force += Separation(neighbours);
                force += Alignment(neighbours);
                force += Cohesion(neighbours);
                force.Normalize();

                //add force to velocity (as our forces are normalised, we multiply them by 100 so they have a greater influence on our heading)
                rb.velocity += force * 100 * Time.deltaTime;
                
                //make us move at max speed all the time (not ideal but just for this example)
                rb.velocity.Normalize();
                rb.velocity *= maxVelocity;
            }

            //move position by velocity
            transform.position += rb.velocity * Time.deltaTime;

            //set our rotation //TODO: fix
            heading = (rb.velocity == Vector3.zero ? new Vector3((float)-Math.Cos(rotation), (float)Math.Sin(rotation)) : rb.velocity);
            heading.Normalize();
            rotation = (float)Math.Atan2(heading.y, heading.x);
        }

        Vector3 Separation(Boid[] _boids)
        {
            Vector3 v = Vector3.zero;

            //get average direction from neighbours to us
            for (int i = 0; i < _boids.Length; ++i)
                v += transform.position - _boids[i].transform.position;
            v /= _boids.Length;

            //subtract velocity to get force towards that direction
            v -= rb.velocity;

            //normalize and return
            v.Normalize();
            return v;
        }

        Vector3 Alignment(Boid[] _boids)
        {
            Vector3 v = Vector3.zero;
            
            //get average velocity of neighbours
            for (int i = 0; i < _boids.Length; ++i)
                v += _boids[i].rb.velocity;
            v /= _boids.Length;
            
            //subtract velocity to get force towards that heading
            v -= rb.velocity;
            
            //normalize and return
            v.Normalize();
            return v;
        }

        Vector3 Cohesion(Boid[] _boids)
        {
            Vector3 v = Vector3.zero;
            
            //get average position of neighbours
            for (int i = 0; i < _boids.Length; ++i)
                v += _boids[i].transform.position;
            v /= _boids.Length;

            //subtract position to get direction from us to average
            v -= transform.position;
            //subtract velocity to get force towards that direction
            v -= rb.velocity;
            
            //normalize and return
            v.Normalize();
            return v;
        }

        /*public void Draw()
        {
            Rect rect = new Rect(position.x, position.y, boidManager.texture.width, boidManager.texture.height);
            Vector3 origin = new Vector3(boidManager.texture.width * 0.5f, boidManager.texture.height * 0.5f);
            //_sb.Draw(boidManager.texture, rect, null, Color.white, rotation, origin, SpriteEffects.None, 0);
        }*/
    }
}
