using UnityEngine;
using System.Collections;

public class ElevatorSwitch : MonoBehaviour {
	[SerializeField]
	private GameObject elevator;

	private void GoUp() {
		elevator.GetComponent<Animator>().SetBool("isGoingDown", false);
		elevator.GetComponent<Animator>().SetBool("isGoingUp", true);
	}
	
	private void GoDown() {
		elevator.GetComponent<Animator>().SetBool("isGoingUp", false);
		elevator.GetComponent<Animator>().SetBool("isGoingDown", true);
	}
}
