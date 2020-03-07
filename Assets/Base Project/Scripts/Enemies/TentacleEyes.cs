using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TentacleEyes : Enemy {

    public List<Transform> explosionPoints; 
    
    public TentacleEyes() {
        health = 1f;
        isDead = false;
    }

    public override void Explode() {
        StartCoroutine(EnemyExplosionManager.Explode(explosion, 
            explosionPoints.Select(transform => transform.position).ToList(), 0.1f));
    }

    public override void Destroy() {
        // do nothing
    }
}
