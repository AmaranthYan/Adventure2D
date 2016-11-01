using UnityEngine;
using System.Collections;

public class ColliderController : MonoBehaviour {
	private int currentColliderIndex = 0;
	[SerializeField]
	private PolygonCollider2D[] colliders;

	public void SetColliderForSprite(int spriteNum)	{
		colliders[currentColliderIndex].enabled = false;
		currentColliderIndex = spriteNum;
		colliders[currentColliderIndex].enabled = true;
	}
}
