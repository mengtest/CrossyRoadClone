using UnityEngine;
using System.Collections;

public class CarMovement : MonoBehaviour {

	public float Speed;	
	public bool Direction;
	public float DestructionThreshold;
	
	private GameObject car;

	void Start () {
		car = this.gameObject;
	}

	void Update () {
		if(Direction) {
			Vector3 anchorPosition = car.transform.parent.FindChild("anchor_right").localPosition;
			this.transform.localPosition = Vector3.MoveTowards(this.transform.localPosition, anchorPosition, Time.deltaTime * Speed);
						
			if(Vector3.Distance(this.transform.localPosition, anchorPosition) < 0.01f)
			{
				Destroy(gameObject);
			}
		}
		else {
			Vector3 anchorPosition = car.transform.parent.FindChild("anchor_left").localPosition;
			this.transform.localPosition = Vector3.MoveTowards(this.transform.localPosition, anchorPosition, Time.deltaTime * Speed);
						
			if(Vector3.Distance(this.transform.localPosition, anchorPosition) < 0.01f)
			{
				Destroy(gameObject);
			}
		}				
	}
}
