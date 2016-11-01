using UnityEngine;
using System.Collections;

public class CrusherRod : MonoBehaviour {
	[SerializeField]
	private GameObject crusher;

	private void TurnOn() {
		crusher.GetComponent<CrusherController>().Run();
	}
}
