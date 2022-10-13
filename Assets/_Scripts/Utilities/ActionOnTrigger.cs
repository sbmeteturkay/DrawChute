//Authored by saban mete turkay demirkiran
//follow: https://github.com/sbmeteturkay

using UnityEngine;
using UnityEngine.Events;
namespace MeteTurkay{
	public class ActionOnTrigger : MonoBehaviour
	{
        public UnityEvent onPlayerEnter;
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                onPlayerEnter?.Invoke();
            }
        }
    }
}
