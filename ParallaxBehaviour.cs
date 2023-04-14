using UnityEngine;

public class ParallaxBehaviour : MonoBehaviour
{
    [SerializeField] private Transform followingTarget;
    [SerializeField, Range(0f, 1f)] private float parallaxSrength = 0.1f;
    [SerializeField] bool disableVerticalParallax;
    private Vector3 targetPreviousPosition;

    void Start()
    {
        if (!followingTarget) followingTarget = Camera.main.transform;

        targetPreviousPosition = followingTarget.position;
    }

    void Update()
    {
        Vector3 delta = followingTarget.position - targetPreviousPosition;

        if (disableVerticalParallax) delta.y = 0;

        targetPreviousPosition = followingTarget.position;
        transform.position += delta * parallaxSrength;
    }
}
