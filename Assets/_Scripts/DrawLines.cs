//Authored by saban mete turkay demirkiran
//follow: https://github.com/sbmeteturkay
//Taken from :https://gamedev.stackexchange.com/questions/96964/how-to-correctly-draw-a-line-in-unity

using UnityEngine;
using System.Collections.Generic;
namespace MeteTurkay{
	public class DrawLines : MonoBehaviour
    {
        // Material used for the connecting lines
        public Material lineMat;

        public float radius = 0.05f;

        // Connect all of the `points` to the `mainPoint`
        public GameObject mainPoint;
        public List<GameObject> points=new List<GameObject>();

        // Fill in this with the default Unity Cylinder mesh
        // We will account for the cylinder pivot/origin being in the middle.
        public Mesh cylinderMesh;


        [SerializeField] List<GameObject> ringGameObjects=new List<GameObject>();

        // Use this for initialization
        void Start()
        {
            //this.connectingRings = new ProceduralRing[points.Length];
            for (int i = 0; i < points.Count-1; i++)
            {
                print("for");
                // Make a gameobject that we will put the ring on
                // And then put it as a child on the gameobject that has this Command and Control script
                this.ringGameObjects[i] = new GameObject();
                this.ringGameObjects[i].name = "Connecting ring #" + i;
                this.ringGameObjects[i].transform.parent = this.gameObject.transform;

                // We make a offset gameobject to counteract the default cylindermesh pivot/origin being in the middle
                GameObject ringOffsetCylinderMeshObject = new GameObject();
                ringOffsetCylinderMeshObject.transform.parent = this.ringGameObjects[i].transform;

                // Offset the cylinder so that the pivot/origin is at the bottom in relation to the outer ring gameobject.
                ringOffsetCylinderMeshObject.transform.localPosition = new Vector3(0f, 1f, 0f);
                // Set the radius
                ringOffsetCylinderMeshObject.transform.localScale = new Vector3(radius, 1f, radius);

                // Create the the Mesh and renderer to show the connecting ring
                MeshFilter ringMesh = ringOffsetCylinderMeshObject.AddComponent<MeshFilter>();
                ringMesh.mesh = this.cylinderMesh;

                MeshRenderer ringRenderer = ringOffsetCylinderMeshObject.AddComponent<MeshRenderer>();
                ringRenderer.material = lineMat;

            }
        }
        public void AddItem(GameObject item)
        {
                points.Add(item);
                print("for");

            print(points.Count);
            // Make a gameobject that we will put the ring on
            // And then put it as a child on the gameobject that has this Command and Control script
                this.ringGameObjects.Add( new GameObject());
                this.ringGameObjects[points.Count - 1].name = "Connecting ring #" + (points.Count - 1);
                this.ringGameObjects[points.Count - 1].transform.parent = this.gameObject.transform;

                // We make a offset gameobject to counteract the default cylindermesh pivot/origin being in the middle
                GameObject ringOffsetCylinderMeshObject = new GameObject();
                ringOffsetCylinderMeshObject.transform.parent = this.ringGameObjects[points.Count - 1].transform;

                // Offset the cylinder so that the pivot/origin is at the bottom in relation to the outer ring gameobject.
                ringOffsetCylinderMeshObject.transform.localPosition = new Vector3(0f, 1f, 0f);
                // Set the radius
                ringOffsetCylinderMeshObject.transform.localScale = new Vector3(radius, 1f, radius);

                // Create the the Mesh and renderer to show the connecting ring
                MeshFilter ringMesh = ringOffsetCylinderMeshObject.AddComponent<MeshFilter>();
                ringMesh.mesh = this.cylinderMesh;

                MeshRenderer ringRenderer = ringOffsetCylinderMeshObject.AddComponent<MeshRenderer>();
                ringRenderer.material = lineMat;
        }
        public void AddItem(Vector3 transform)
        {
            GameObject obj = new GameObject();
            obj.transform.position = transform;
            points.Add(obj);
            print("for");

            print(points.Count);
            // Make a gameobject that we will put the ring on
            // And then put it as a child on the gameobject that has this Command and Control script
            this.ringGameObjects.Add(new GameObject());
            this.ringGameObjects[points.Count - 1].name = "Connecting ring #" + (points.Count - 1);
            this.ringGameObjects[points.Count - 1].transform.parent = this.gameObject.transform;

            // We make a offset gameobject to counteract the default cylindermesh pivot/origin being in the middle
            GameObject ringOffsetCylinderMeshObject = new GameObject();
            ringOffsetCylinderMeshObject.transform.parent = this.ringGameObjects[points.Count - 1].transform;

            // Offset the cylinder so that the pivot/origin is at the bottom in relation to the outer ring gameobject.
            ringOffsetCylinderMeshObject.transform.localPosition = new Vector3(0f, 1f, 0f);
            // Set the radius
            ringOffsetCylinderMeshObject.transform.localScale = new Vector3(radius, 1f, radius);

            // Create the the Mesh and renderer to show the connecting ring
            MeshFilter ringMesh = ringOffsetCylinderMeshObject.AddComponent<MeshFilter>();
            ringMesh.mesh = this.cylinderMesh;

            MeshRenderer ringRenderer = ringOffsetCylinderMeshObject.AddComponent<MeshRenderer>();
            ringRenderer.material = lineMat;
        }
        // Update is called once per frame
        void Update()
        {
            for (int i = 0; i < points.Count; i++)
            {
                // Move the ring to the point
                this.ringGameObjects[i].transform.position = this.points[i].transform.position;

                // Match the scale to the distance
                float cylinderDistance = 0.5f * Vector3.Distance(this.points[i].transform.position, this.mainPoint.transform.position);
                this.ringGameObjects[i].transform.localScale = new Vector3(this.ringGameObjects[i].transform.localScale.x, cylinderDistance, this.ringGameObjects[i].transform.localScale.z);

                // Make the cylinder look at the main point.
                // Since the cylinder is pointing up(y) and the forward is z, we need to offset by 90 degrees.
                this.ringGameObjects[i].transform.LookAt(this.mainPoint.transform, Vector3.up);
                this.ringGameObjects[i].transform.rotation *= Quaternion.Euler(90, 0, 0);
            }
        }
    }
}
