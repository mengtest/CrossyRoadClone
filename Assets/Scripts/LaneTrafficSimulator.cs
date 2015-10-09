using UnityEngine;
using System.Collections;

public class LaneTrafficSimulator : MonoBehaviour {

	public float Density;	
	public bool Direction;
	public float Speed;
	
	private float lastCarTimestamp = 0.0f;
	private GameObject lane;

	void Start () {
		lane = this.gameObject;
		this.InvokeRepeating ("GenerateCar", 0.0f, Density);
	}

	void Update () {
			
	}
	
	private void GenerateCar()
	{
		GameObject car = (GameObject)Instantiate(Resources.Load("car_regular"));
		car.transform.parent = lane.transform;		
		
		if(Direction) {
			car.transform.localPosition = lane.transform.FindChild("anchor_left").localPosition;
		}
		else {
			car.transform.localPosition = lane.transform.FindChild("anchor_right").localPosition;
		}
		
		car.AddComponent<CarMovement>();
		car.GetComponent<CarMovement>().Speed = this.Speed;
		car.GetComponent<CarMovement>().Direction = this.Direction;
	}		
}
