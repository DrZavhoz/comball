using UnityEngine;

public class CameraTextureGenerator : MonoBehaviour
{
    //------------------------------------------------------------------------------------------------------------------
    [SerializeField]
    private Camera _projectionCamera;

    [SerializeField] 
    private Vector2Int _textureSize;

    [SerializeField][Range(0.0f, 1f)]
    private float _textureMargin;

    //------------------------------------------------------------------------------------------------------------------
    public float TextureMargin => _textureMargin;

    //------------------------------------------------------------------------------------------------------------------
    private void Awake()
    {
        _projectionCamera.gameObject.SetActive(false);
    }

    //------------------------------------------------------------------------------------------------------------------
    public Texture2D GenerateTextureFrom(GameObject fromObject, float objectSize)
    {
        return GenerateTextureFrom(fromObject, objectSize, _textureMargin);
    }
    
    //------------------------------------------------------------------------------------------------------------------
    public Texture2D GenerateTextureFrom(GameObject fromObject, float objectSize, float textureMargin)
    {
        _projectionCamera.gameObject.SetActive(true);
        
        Vector3 posBefore = fromObject.transform.position;
        Transform parentBefore = fromObject.transform.parent;
        fromObject.transform.SetParent(transform, false);

        float cameraSize = objectSize + textureMargin * objectSize * 2;
        cameraSize *= 0.5f;
        _projectionCamera.orthographicSize = cameraSize;

        RenderTexture tempRenderTexture = new RenderTexture(_textureSize.x, _textureSize.y, 32);
        tempRenderTexture.antiAliasing = 8;
        _projectionCamera.targetTexture = tempRenderTexture;
        _projectionCamera.Render();

        RenderTexture.active = tempRenderTexture;
        
        Texture2D texture = new Texture2D(_textureSize.x,_textureSize.y, TextureFormat.ARGB32, false);
        texture.ReadPixels(
            new Rect(Vector2.zero, _textureSize), 0, 0);
        texture.Apply();

        _projectionCamera.targetTexture = null;
        RenderTexture.active = null;
        Destroy(tempRenderTexture);
        
        _projectionCamera.gameObject.SetActive(false);
        fromObject.transform.SetParent(parentBefore, false); 
        fromObject.transform.position = posBefore;

        return texture;
    }
}
