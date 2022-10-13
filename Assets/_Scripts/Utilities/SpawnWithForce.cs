//Authored by saban mete turkay demirkiran
//follow: https://github.com/sbmeteturkay

using UnityEngine;

namespace MeteTurkay{
	[RequireComponent(typeof(Rigidbody))]
	public class SpawnWithForce : MonoBehaviour
	{
		[SerializeField] Rigidbody rb;
        [SerializeField] float upForce = 1f;
        [SerializeField] float sideForce = 0.1f;
        private void Start()
        {
            float xForce = Random.Range(sideForce/2, sideForce);
            xForce *= Helpers.CoinFlip()?-1:1;
            //we dont need down force, so no minus
            float yForce = Random.Range(upForce / 2f, upForce);
            float zForce = Helpers.CoinFlip() ? -Random.Range(sideForce / 2, sideForce) : Random.Range(sideForce / 2, sideForce);
            zForce *= Helpers.CoinFlip() ? -1 : 1;
            rb.gameObject.transform.rotation = Random.rotation;
            Vector3 force = new Vector3(xForce, yForce, zForce);
            rb.velocity = force;
        }
    }
}
