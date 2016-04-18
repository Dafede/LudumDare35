using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPool{
	GameObject pooledObject;
	int pooledObjectsAmount;
	bool willGrow;
	List<GameObject> pooledObjects;
	Transform parentObject;

	public ObjectPool(GameObject obj, Transform parent = null, bool grow = false, int amount = 10){
		pooledObject = obj;
		parentObject = parent;
		willGrow = grow;
		pooledObjectsAmount = amount;
		pooledObjects = new List<GameObject> ();

		for(int i = 0; i < pooledObjectsAmount; i++){
			GameObject o = (GameObject)GameObject.Instantiate (pooledObject);
			if (parent != null) {
				o.transform.parent = parent;
			}
			o.SetActive (false);
			pooledObjects.Add (o);

		}
	}

	public GameObject GetPooledObject()
	{
		for (int i = 0; i < pooledObjects.Count; i++) {
			if (!pooledObjects [i].activeInHierarchy) {
				return pooledObjects [i];
			}
		}

		if (willGrow) {
			GameObject o = (GameObject)GameObject.Instantiate (pooledObject);
			if (parentObject != null) {
				o.transform.parent = parentObject;
			}
			o.SetActive (false);
			pooledObjects.Add (o);
		}

		return null;
	}

	public void DisableAllObjects(){
		for (int i = 0; i < pooledObjects.Count; i++) {
			if (pooledObjects [i].activeInHierarchy) {
				pooledObjects [i].SetActive (false);
			}
		}
	}

}
