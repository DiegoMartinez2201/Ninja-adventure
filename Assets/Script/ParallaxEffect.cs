using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    [SerializeField] private float parallaxMultiplier;
    private Transform cameraTransform;
    private Vector3 previousCameraPosition;
    private float spriteWidth, starPosition ;
    private void Start()
    {
        cameraTransform = GetComponent<Transform>();
        previousCameraPosition = cameraTransform.position;
        spriteWidth = GetComponent<SpriteRenderer>().bounds.size.x;
        starPosition = transform.position.x;
    }
    void LateUpdate()
    {
        float deltaX = (cameraTransform.position.x - previousCameraPosition.x) * parallaxMultiplier;
        float moveAmount = cameraTransform.position.x * (1-parallaxMultiplier);
        transform.Translate(new Vector3(deltaX, 0, 0));
        previousCameraPosition = cameraTransform.position;
        if (moveAmount > starPosition + spriteWidth)
        {
            transform.Translate(new Vector3(spriteWidth, 0, 0));
        }
        else if(moveAmount < starPosition - spriteWidth)
        {
            transform.Translate(new Vector3(-spriteWidth, 0, 0));
        }
    }
}
