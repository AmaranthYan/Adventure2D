using UnityEngine;
using System.Collections;

public class CharacterInitializer : MonoBehaviour {
    [SerializeField]
    private Vector3 defaultPosition = Vector3.zero;
    [SerializeField]
    private Quaternion defaultRotation = Quaternion.identity;
    [SerializeField]
    private Vector3 defaultScale = Vector3.one;

	void Start() {        
        GetComponent<Rigidbody2D>().centerOfMass = Vector2.zero;
	}

    public void InitTransform()
    {
        transform.position = defaultPosition;
        transform.rotation = defaultRotation;
        transform.localScale = defaultScale;
    }
}
