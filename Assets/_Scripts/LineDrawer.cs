//Authored by saban mete turkay demirkiran
//follow: https://github.com/sbmeteturkay

using UnityEngine;

namespace MeteTurkay{
	public class LineDrawer : MonoBehaviour
	{
        [SerializeField] LineRenderer line;
        bool draw=true;
        public Transform pointOne, pointTwo;

        private void Update()
        {
            if (!draw)
                return;
            else
                DrawLineBetweenTwoPoints();
        }
        public void DrawLineBetweenTwoPoints()
        {
           line.SetPosition(0, pointOne.position);
            line.SetPosition(1, pointTwo.position); 
        }
    }
}
