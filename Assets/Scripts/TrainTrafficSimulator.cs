using UnityEngine;
using System.Collections;

public class TrainTrafficSimulator : MonoBehaviour {

	public float TrainInterval = 15.0f;
	
	public bool Direction;

	private float lastCarTimestamp = 0.0f;
	
	private GameObject lane;
	
	// Use this for initialization
	void Start () {
		lane = this.gameObject;
		float delay = Random.Range (1.0f, 5.0f);
		InvokeRepeating ("SendTrain", delay, TrainInterval);
	}
	
	// Update is called once per frame
	void Update () {
				
	}
	
	void SendTrain()
	{
		lastCarTimestamp = Time.realtimeSinceStartup;

		GameObject train = (GameObject)Instantiate(Resources.Load("train"));
		train.transform.parent = lane.transform;

		if (Direction) {
			train.transform.localPosition = lane.transform.FindChild("anchor_left").localPosition;
		}
		else {
			train.transform.localPosition = lane.transform.FindChild("anchor_right").localPosition;
		}

		train.AddComponent<CarMovement>();
		train.GetComponent<CarMovement>().Speed = 5.0f;
		train.GetComponent<CarMovement>().Direction = this.Direction;
	}


}
