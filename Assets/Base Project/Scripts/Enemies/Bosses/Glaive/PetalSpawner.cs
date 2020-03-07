using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PetalSpawner : MonoBehaviour {

    public GameObject petalBullet;
    public float preSpawnDelay;
    public float spawnDelay;

    private List<Transform> _childTransforms; 
    
    private void Awake() {
        _childTransforms = GetComponentsInChildren<Transform>().ToList();
        _childTransforms.RemoveAt(0);
    }

    private IEnumerator Start() {
        yield return new WaitForSeconds(preSpawnDelay);
        
        foreach (Transform childTransform in _childTransforms) {
            Instantiate(petalBullet, childTransform.position, Quaternion.identity);
            yield return new WaitForSeconds(spawnDelay);
        }
    }
}
