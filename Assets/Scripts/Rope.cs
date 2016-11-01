using UnityEngine;
using System.Collections;

public class Rope : MonoBehaviour {
	[SerializeField]
	private GameObject pointA;
	[SerializeField]
	private GameObject pointB;
	public int piecesNumber;
	[SerializeField]
	private GameObject ropePiece;
	private const float OFFSET_X = 0.105f;
	private const float ANGLE_LIMIT = 75.0f;
	private GameObject[] pieces;

	void Awake() {
		pieces = new GameObject[piecesNumber];
	}

	void Start() {
		Vector3 position = (pointA.transform.position + pointB.transform.position) / 2;
		for (int i = 0; i < piecesNumber; i++) {
			pieces[i] = GameObject.Instantiate(ropePiece, position, Quaternion.identity) as GameObject;
			pieces[i].name = "RopePiece";
			pieces[i].transform.parent = this.transform;
			pieces[i].GetComponent<Rigidbody2D>().fixedAngle = false;
		}
		for (int i = 0; i < piecesNumber; i++) {
			HingeJoint2D hingeJoint;
            DistanceJoint2D distanceJoint;
            hingeJoint = AddHingeJoint(pieces[i]);
			hingeJoint.anchor = new Vector2(-OFFSET_X, 0.0f);
            distanceJoint = AddDistanceJoint(pieces[i]);
            distanceJoint.anchor = new Vector2(-OFFSET_X, 0.0f);
            if (i == 0) {
				hingeJoint.connectedBody = pointA.GetComponent<Rigidbody2D>();
                hingeJoint.connectedAnchor = pointA.GetComponent<HingeJoint2D>().anchor;
                //hingeJoint.useLimits = false;
                distanceJoint.connectedBody = pointA.GetComponent<Rigidbody2D>();
                distanceJoint.connectedAnchor = pointA.GetComponent<HingeJoint2D>().anchor;
            } else {
				hingeJoint.connectedBody = pieces[i - 1].GetComponent<Rigidbody2D>();
				hingeJoint.connectedAnchor = new Vector2(OFFSET_X, 0.0f);
                distanceJoint.connectedBody = pieces[i - 1].GetComponent<Rigidbody2D>();
                distanceJoint.connectedAnchor = new Vector2(OFFSET_X, 0.0f);
            }
			hingeJoint = AddHingeJoint(pieces[i]);
			hingeJoint.anchor = new Vector2(OFFSET_X, 0.0f);
            distanceJoint = AddDistanceJoint(pieces[i]);
            distanceJoint.anchor = new Vector2(OFFSET_X, 0.0f);
            if (i == piecesNumber - 1) {
				hingeJoint.connectedBody = pointB.GetComponent<Rigidbody2D>();
                hingeJoint.connectedAnchor = pointB.GetComponent<HingeJoint2D>().anchor;
                //hingeJoint.useLimits = false;
                distanceJoint.connectedBody = pointB.GetComponent<Rigidbody2D>();
                distanceJoint.connectedAnchor = pointB.GetComponent<HingeJoint2D>().anchor;
            } else {
				hingeJoint.connectedBody = pieces[i + 1].GetComponent<Rigidbody2D>();
				hingeJoint.connectedAnchor = new Vector2(-OFFSET_X, 0.0f);
                distanceJoint.connectedBody = pieces[i + 1].GetComponent<Rigidbody2D>();
                distanceJoint.connectedAnchor = new Vector2(-OFFSET_X, 0.0f);
            }
		}
	}

	void Update() {

	}

	public bool Cut(int index) {
		if ((index < 0) || (index >= pieces.Length))
			return false;
		GameObject.Destroy(pieces[index]);
		return true;
	}

	private HingeJoint2D AddHingeJoint(GameObject piece) {
		HingeJoint2D hingeJoint = piece.AddComponent<HingeJoint2D>();
        hingeJoint.autoConfigureConnectedAnchor = false;
		hingeJoint.enableCollision = false;
		//JointAngleLimits2D jointLimits = hingeJoint.limits;
		//jointLimits.min = reverse ? ANGLE_LIMIT : 180-ANGLE_LIMIT;
		//jointLimits.max = reverse ? -ANGLE_LIMIT : 180+ANGLE_LIMIT;
		//hingeJoint.limits = jointLimits;
        hingeJoint.useLimits = false;
        return hingeJoint;
	}

    private DistanceJoint2D AddDistanceJoint(GameObject piece)
    {
        DistanceJoint2D distanceJoint = piece.AddComponent<DistanceJoint2D>();
        distanceJoint.autoConfigureConnectedAnchor = false;
        distanceJoint.enableCollision = false;
        distanceJoint.autoConfigureDistance = false;
        distanceJoint.maxDistanceOnly = false;
        distanceJoint.distance = 0;
        return distanceJoint;
    }
}
