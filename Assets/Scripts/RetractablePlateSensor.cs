using UnityEngine;
using System.Collections;

public class RetractablePlateSensor : MonoBehaviour {
	[SerializeField]
	private GameObject retractablePlate;

	private void Retract() {
		retractablePlate.GetComponent<Animator>().SetTrigger("retract");
	}
}
