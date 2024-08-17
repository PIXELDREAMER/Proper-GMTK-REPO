using UnityEngine;

namespace Game.Core
{
    public class ParallaxEffect : MonoBehaviour
    {
        [SerializeField]private Vector2 effectMultiplier;

        [SerializeField]private Transform cameraTransform;
        [SerializeField]private bool infiniteHorizontal = true;
        [SerializeField]private bool infiniteVertical = true;

        private Vector3 lastCameraPosition;
        private float textureUnitSizeX = 0;
        private float textureUnitSizeY = 0;

        // Start is called before the first frame update
        void Start()
        {
            lastCameraPosition = cameraTransform.position;
            Sprite sprite = GetComponent<SpriteRenderer>().sprite;
            Texture texture = sprite.texture;
            textureUnitSizeX = texture.width/ sprite.pixelsPerUnit;
            textureUnitSizeY = texture.height/ sprite.pixelsPerUnit;
        }

        
        void LateUpdate()
        {
            Vector3 deltaMovement = cameraTransform.position - transform.position;
            transform.position += new Vector3(deltaMovement.x * effectMultiplier.x, deltaMovement.y * effectMultiplier.y);
            lastCameraPosition = cameraTransform.position;

            if(infiniteHorizontal)
            {
                if(Mathf.Abs(cameraTransform.position.x - transform.position.x) >= textureUnitSizeX * transform.lossyScale.x)
                {
                    float offsetPositionX = (cameraTransform.position.x - transform.position.x) % (textureUnitSizeX * transform.lossyScale.x);
                    transform.position = new Vector3(cameraTransform.position.x + offsetPositionX, transform.position.y);
                }
            }
            
            if(infiniteVertical)
            {
                if(Mathf.Abs(cameraTransform.position.y - transform.position.y) >= textureUnitSizeY * transform.lossyScale.y)
                {
                    float offsetPositionY = (cameraTransform.position.y - transform.position.y) % (textureUnitSizeY * transform.lossyScale.y);
                    transform.position = new Vector3(cameraTransform.position.x, transform.position.y + offsetPositionY);
                }
            }
        }
    }
}
