using TMPro;
using UnityEngine;

namespace FunnyBlox
{
  public class GeneratorTextureBalloons : MonoSingleton<GeneratorTextureBalloons>
  {
    private static readonly string[] MILLION_SYSTEM_SCALES =
      { "K", "M", "B", "T", "Q", "Qt", "S", "Sp", "O", "N", "D" };

    [SerializeField] private CameraTextureGenerator cameraTextureGenerator;

    [SerializeField] private TextMeshPro textMesh;
    [SerializeField] private int amountOfTextures = 50;

    private TextureManager _textureManager;

    private void Start()
    {
      GenerateTextures();
    }

    private void GenerateTextures()
    {
      _textureManager = new TextureManager(cameraTextureGenerator);

      int value = 2;
      
      textMesh.gameObject.SetActive(true);

      for (int i = 0; i < amountOfTextures; i++)
      {
        textMesh.text = ConvertToMillionSystem(i);
        _textureManager.GenerateTexture(i, textMesh);
        value *= 2;
      }
      
      Destroy(textMesh.gameObject);

    }

    private string ConvertToMillionSystem(int index)
    {
      if (index < 10)
        return ((int)Mathf.Pow(2, index)).ToString();

      return string.Format("{0}{1}", (int)Mathf.Pow(2, index % 10), MILLION_SYSTEM_SCALES[index / 10 - 1]);
    }

    public Texture2D GetTexture(int value)
    {
      return _textureManager.GetTexture(value);
    }
  }
}