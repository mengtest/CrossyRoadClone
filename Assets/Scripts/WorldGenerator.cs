using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WorldGenerator : MonoBehaviour {
	
	public int WorldBufferSize;
	
	private GameObject root;
	
	private int minStreetSize = 2;
	
	private int maxStreetSize = 5;

	private int minRailwaySize = 1;

	private int maxRailwaySize = 2;
	
	private List<LaneDescriptor> laneDescriptors;
	
	// Use this for initialization
	void Start () {
		root = this.gameObject;
		
		laneDescriptors = new List<LaneDescriptor>();
		GenerateWorldChunk(100);
		
		for(int i = 0; i < WorldBufferSize; i++)
		{						
			GenerateLane(laneDescriptors[i]);
		}	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	private void GenerateLane(LaneDescriptor descriptor)
	{
		switch(descriptor.Type)
		{
			case LaneType.Grass:
				GenerateEmptyLane();
				break;
			case LaneType.Car:
				var randomDensity = Random.Range(3.0f, 6.0f);
				var randomSpeed = Random.Range(0.5f, 1.5f);
				var randomDirection = Random.Range(0.0f, 1.0f) > 0.5f;
				GenerateCarLane(randomDensity, randomSpeed, randomDirection);
				break;		
			case LaneType.Train:
				randomDirection = Random.Range(0.0f, 1.0f) > 0.5f;
				GenerateRailway(randomDirection);
				break;
		}
	}
	
	private void GenerateCarLane(float density, float speed, bool direction)
	{
		float currentZPosition = 0.0f;
		int currentChildId = 0;
		
		if(root.transform.childCount != 0)
		{
			string lastChildName = root.transform.GetChild(root.transform.childCount - 1).name;
			int lastChildId = int.Parse(lastChildName);
			currentChildId = lastChildId + 1;
			currentZPosition = currentChildId * 1.8f;
		}
		
		GameObject lane = (GameObject)Instantiate(Resources.Load("lane"));		
		lane.transform.position = new Vector3(lane.transform.position.x, 
			lane.transform.position.y, currentZPosition);
		lane.name = currentChildId.ToString();
		lane.transform.parent = root.transform;
		lane.AddComponent<LaneTrafficSimulator>();
		lane.GetComponent<LaneTrafficSimulator>().Density = density;
		lane.GetComponent<LaneTrafficSimulator>().Speed = speed;
		lane.GetComponent<LaneTrafficSimulator>().Direction = direction;
						
		var leftAnchor = new GameObject();
		leftAnchor.name = "anchor_left";
		leftAnchor.transform.parent = lane.transform;		
		leftAnchor.transform.localPosition = new Vector3(-5.5f, 0.5f, 0.0f);
		
		var rightAnchor = new GameObject();
		rightAnchor.name = "anchor_right";
		rightAnchor.transform.parent = lane.transform;
		rightAnchor.transform.localPosition = new Vector3(5.5f, 0.5f, 0.0f);
	}

	private void GenerateRailway(bool direction)
	{
		float currentZPosition = 0.0f;
		int currentChildId = 0;
		
		if(root.transform.childCount != 0)
		{
			string lastChildName = root.transform.GetChild(root.transform.childCount - 1).name;
			int lastChildId = int.Parse(lastChildName);
			currentChildId = lastChildId + 1;
			currentZPosition = currentChildId * 1.8f;
		}
		
		GameObject lane = (GameObject)Instantiate(Resources.Load("railway"));		
		lane.transform.position = new Vector3(lane.transform.position.x, 
		                                      lane.transform.position.y, currentZPosition);
		lane.name = currentChildId.ToString();
		lane.transform.parent = root.transform;
		lane.AddComponent<TrainTrafficSimulator>();
		lane.GetComponent<TrainTrafficSimulator>().Direction = direction;
		
		var leftAnchor = new GameObject();
		leftAnchor.name = "anchor_left";
		leftAnchor.transform.parent = lane.transform;		
		leftAnchor.transform.localPosition = new Vector3(-10.0f, 0.5f, 0.0f);
		
		var rightAnchor = new GameObject();
		rightAnchor.name = "anchor_right";
		rightAnchor.transform.parent = lane.transform;
		rightAnchor.transform.localPosition = new Vector3(13.0f, 0.5f, 0.0f);
	}
	
	private void GenerateEmptyLane()
	{
		float currentZPosition = 0.0f;
		int currentChildId = 0;
		
		if(root.transform.childCount != 0)
		{
			string lastChildName = root.transform.GetChild(root.transform.childCount - 1).name;
			int lastChildId = int.Parse(lastChildName);
			currentChildId = lastChildId + 1;
			currentZPosition = currentChildId * 1.8f;
		}
		
		GameObject lane = (GameObject)Instantiate(Resources.Load("lane_grass"));		
		lane.transform.position = new Vector3(lane.transform.position.x, 
		                                      lane.transform.position.y, currentZPosition);
		lane.name = currentChildId.ToString();
		lane.transform.parent = root.transform;
	}
	
	private void GenerateWorldChunk(int chunkSize)
	{
		int currentSize = 0;
		
		while(currentSize < chunkSize) {
			if(laneDescriptors.Count == 0 || laneDescriptors[laneDescriptors.Count - 1].Type == LaneType.Car)
			{
				// Add the first free zone
				laneDescriptors.Add(new LaneDescriptor() { Type = LaneType.Grass });
				currentSize++;
			}

			float randomF = Random.Range(0.0f, 1.0f);
			if(randomF < 0.2f) {
				int railwaySize = Random.Range(minRailwaySize, maxRailwaySize);

				for(int i = 0; i < railwaySize; i++) {
					laneDescriptors.Add(new LaneDescriptor() { Type = LaneType.Train });
				}
				currentSize += railwaySize;
			}
			else {
				int currentStreetSize = Random.Range(minStreetSize, maxStreetSize);
				
				for(int i = 0; i < currentStreetSize; i++) {
					laneDescriptors.Add(new LaneDescriptor() { Type = LaneType.Car });
				}
				currentSize += currentStreetSize;
			}
		}
	}
}
