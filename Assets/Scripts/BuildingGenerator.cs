using UnityEngine;
using System.Collections;

public class BuildingGenerator : MonoBehaviour {
	[SerializeField] GameObject[] buildingPrefabs;
	[SerializeField] GameObject[] floorPrefabs;
	[SerializeField] GameObject[] doorPrefabs;
	[SerializeField] GameObject[] windowPrefabs;

	//GameObject buildings;
	GameObject[] floors;

	// Use this for initialization
	void Start () {
		MakeNewBuilding();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	GameObject MakeNewBuilding(){
		GameObject newBuilding;
		int numFloors = Random.Range(1,6);
		int numDoors = Random.Range(1,2);
		int orientation = Random.Range(0,3); //0 - North (z+), 1 - East(x+), 2 - South (z-), 3 - West (x-)
		int numWindows;
		Vector3 footprint = new Vector3(
			Map(Random.value,0f,1f,15f,45f), 
			(float) Random.Range(9,12),
			Map(Random.value,0f,1f,20f,45f));
		
		newBuilding = GameObject.Instantiate(buildingPrefabs[0]);
		newBuilding.transform.position = new Vector3(0,(numFloors*footprint.y)/2);
		newBuilding.GetComponent<BoxCollider>().size = new Vector3(footprint.x,(numFloors*footprint.y),footprint.z);

		floors = new GameObject[numFloors];
		for(int i = 0; i < numFloors; i++){
			floors[i] = GameObject.Instantiate(floorPrefabs[0]);
			floors[i].transform.parent = newBuilding.transform;
			floors[i].transform.localScale = footprint;
			floors[i].transform.position = new Vector3(0f,floors[i].transform.position.y + i*footprint.y+footprint.y/2);
		}

		GameObject door = GameObject.Instantiate(doorPrefabs[0]);
		door.transform.parent = newBuilding.transform;
		door.transform.localScale = new Vector3(1f, 2f, 1f);

		return newBuilding;
	}

	static float Map (float inFloat, float lowerBound, float upperBound, float lowerMap, float upperMap){
		//Also know as a linear something, Y = (X-A)/(B-A) * (D-C) + D
		//Given a value X and a range A,B, find X's position between the range C,D and return it as Y
		float outFloat = (inFloat-lowerBound)/(upperBound - lowerBound) * (upperMap - lowerMap) + lowerMap;
		return outFloat;
	}
}
