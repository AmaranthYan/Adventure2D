using UnityEngine;
using System.Collections;

public class VentilatorRod : MonoBehaviour {
	[SerializeField]
	private GameObject windZone;
	
	private void ShutDown() {
		windZone.transform.FindChild("Ventilator").GetComponent<Animator>().SetBool("isRunning", false);
		Invoke("StopPhysics", 0.6f);
		Invoke("StopParticleSystem", 0.6f);
	}
	
	private void StopPhysics() {
		windZone.GetComponent<WindZone2D>().enabled = false;
	}

	private void StopParticleSystem() {
		windZone.transform.FindChild("WindParticle").GetComponent<ParticleSystem>().Stop();
	}
}
