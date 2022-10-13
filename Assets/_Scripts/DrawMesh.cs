//Authored by saban mete turkay demirkiran
//follow: https://github.com/sbmeteturkay

using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using System.Collections;

namespace MeteTurkay{
	public class DrawMesh : MonoBehaviour
	{
		Camera cam;
        private void Start()
        {
			cam = GetComponent<PlayerInput>().camera;
        }
		private IEnumerator Draw()
        {
			GameObject drawing = new GameObject("Drawing");
			drawing.AddComponent<MeshFilter>();
			drawing.AddComponent<MeshRenderer>();
			Mesh mesh = new Mesh();
			List<Vector3> vertices = new List<Vector3>(new Vector3[8]);

			//Start draw position
			Vector3 startPosition = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
			Vector3 temp = new Vector3(startPosition.x, startPosition.y, 0.5f);
			for(int i = 0; i < vertices.Count; i++)
            {
				vertices[i] = temp;
            }
			List<int> triangles = new List<int>(new int[]{
			0, 2, 1, //face front
			0, 3, 2,
			2, 3, 4, //face top
			2, 4, 5,
			1, 2, 5, //face right
			1, 5, 6,
			0, 7, 4, //face left
			0, 4, 3,
			5, 4, 7, //face back
			5, 7, 6,
			0, 6, 7, //face bottom
			0, 1, 6
			 });
			mesh.vertices = vertices.ToArray();
			mesh.triangles = triangles.ToArray();
			drawing.GetComponent<MeshFilter>().mesh = mesh;
			drawing.GetComponent<Renderer>().material.color = Color.red;
			Vector3 lastMousePosition = startPosition;
            while (true)
            {
				float minDistance = 0.1f;
				float distance = Vector3.Distance(cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10)), lastMousePosition);

                while (distance < minDistance)
                {
					distance= Vector3.Distance(cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10)), lastMousePosition);
					yield return null;
				}
				vertices.AddRange(new Vector3[4]);
				triangles.AddRange(new int[30]);
				int vIndex = vertices.Count - 8;
				//previos verices indices
				int vIndex0 = vIndex + 3;
				int vIndex1 = vIndex + 2;
				int vIndex2 = vIndex + 1;
				int vIndex3 = vIndex + 0;
				//new vertices indices
				int vIndex4 = vIndex + 4;
				int vIndex5 = vIndex + 5;
				int vIndex6 = vIndex + 6;
				int vIndex7 = vIndex + 7;
				Vector3 currentMousePosition = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
				Vector3 mouseFowardVector = (currentMousePosition - lastMousePosition).normalized;

				float lineThickness = 0.25f;

				Vector3 topRightVertex = currentMousePosition + Vector3.Cross(mouseFowardVector, Vector3.back)*lineThickness;
				Vector3 bottomRightVertex = currentMousePosition + Vector3.Cross(mouseFowardVector, Vector3.forward) * lineThickness;
				Vector3 topLeftVertex = new Vector3(topRightVertex.x, topRightVertex.y, 1);
				Vector3 bottomLeftVertex = new Vector3(bottomRightVertex.x, bottomRightVertex.y, 1);

				vertices[vIndex4] = topLeftVertex;
				vertices[vIndex5] = topRightVertex;
				vertices[vIndex6] = bottomRightVertex;
				vertices[vIndex7] = bottomLeftVertex;

				int tIndex = triangles.Count - 30;
				
				// New Top Face
				triangles[tIndex + 0] = vIndex2;
				triangles[tIndex + 1] = vIndex3;
				triangles[tIndex + 2] = vIndex4;
				triangles[tIndex + 3] = vIndex2;
				triangles[tIndex + 4] = vIndex4;
				triangles[tIndex + 5] = vIndex5;

				// New Right Face
				triangles[tIndex + 6] = vIndex1;
				triangles[tIndex + 7] = vIndex2;
				triangles[tIndex + 8] = vIndex5;
				triangles[tIndex + 9] = vIndex1;
				triangles[tIndex + 10] = vIndex5;
				triangles[tIndex + 11] = vIndex6;

				// New Left Face
				triangles[tIndex + 12] = vIndex0;
				triangles[tIndex + 13] = vIndex7;
				triangles[tIndex + 14] = vIndex4;
				triangles[tIndex + 15] = vIndex0;
				triangles[tIndex + 16] = vIndex4;
				triangles[tIndex + 17] = vIndex3;

				// New Back Face
				triangles[tIndex + 18] = vIndex5;
				triangles[tIndex + 19] = vIndex4;
				triangles[tIndex + 20] = vIndex7;
				triangles[tIndex + 21] = vIndex0;
				triangles[tIndex + 22] = vIndex4;
				triangles[tIndex + 23] = vIndex3;

				// New Bottom Face
				triangles[tIndex + 24] = vIndex0;
				triangles[tIndex + 25] = vIndex6;
				triangles[tIndex + 26] = vIndex7;
				triangles[tIndex + 27] = vIndex0;
				triangles[tIndex + 28] = vIndex1;
				triangles[tIndex + 29] = vIndex6;

				mesh.vertices = vertices.ToArray();
				mesh.triangles = triangles.ToArray();

				lastMousePosition = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y,10));

				yield return null;
			}
        }
        public void StartDraw(InputAction.CallbackContext callbackContext)
        {
			if (!callbackContext.performed)
				return;
			print("start draw");
			StartCoroutine(Draw());
        }
		public void EndDraw(InputAction.CallbackContext callbackContext)
		{
			if (!callbackContext.performed)
				return;
			print("enddraw");
			StopAllCoroutines();
		}
	}
}
