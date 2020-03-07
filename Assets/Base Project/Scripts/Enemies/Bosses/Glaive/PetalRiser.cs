using System.Collections;
using UnityEngine;

public class PetalRiser : MonoBehaviour {

    public float riseDelay;
    public float postRiseDelay;
    public float destroyTime;

    public GameObject parent;
    
    public ParticleSystem particle;
    
    private BoxCollider2D _boxCollider2D;

    private void Awake() {
        _boxCollider2D = GetComponent<BoxCollider2D>();
    }

    private IEnumerator Start()  {
        yield return new WaitForSeconds(riseDelay);
        _boxCollider2D.enabled = true;
        particle.Play();
        
        yield return new WaitForSeconds(postRiseDelay);
        _boxCollider2D.enabled = false;
        particle.Stop();
        
        yield return new WaitForSeconds(destroyTime);
        Destroy(parent.gameObject);
    }
    
}
