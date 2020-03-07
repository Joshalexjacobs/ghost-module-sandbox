using UnityEngine;

public class ParallaxWaveManager : MonoBehaviour {

    public GameObject currentParallax;

    public void ChangeParallax(GameObject nextParallax) {
        Destroy(currentParallax);

        currentParallax = Instantiate(nextParallax, transform.position, Quaternion.identity);
        currentParallax.transform.parent = gameObject.transform;
    }
    
}
