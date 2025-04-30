using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace FunnyBlox
{
    public class TextureManager
    {

        private CameraTextureGenerator _textureGenerator;
        private readonly Dictionary<int, Texture2D> _numbersPool = new Dictionary<int, Texture2D>();

        private Vector2 _numberAspectRatioInterval = new Vector2(0.0f, 4.0f);
        private Vector2 _numbersTextureMarginInterval = new Vector2(0.1f, 0.35f);

        public TextureManager(CameraTextureGenerator cameraTextureGenerator)
        {
            _textureGenerator = cameraTextureGenerator;
        }

        public Texture2D GenerateTexture(int pieceValue, TextMeshPro pieceNumber)
        {
            pieceNumber.ForceMeshUpdate();
            float size = Mathf.Max(pieceNumber.bounds.size.x, pieceNumber.bounds.size.y);

            float numberAspectRatio = pieceNumber.bounds.size.x / pieceNumber.bounds.size.y;

            float step =
                (numberAspectRatio - _numberAspectRatioInterval.x) /
                (_numberAspectRatioInterval.y - _numberAspectRatioInterval.x);

            float textureMargin =
                Mathf.Lerp(
                    _numbersTextureMarginInterval.y,
                    _numbersTextureMarginInterval.x,
                    step);

            Texture2D texture = _textureGenerator.GenerateTextureFrom(pieceNumber.gameObject, size);//, textureMargin
            _numbersPool[pieceValue] = texture;
            return texture;
        }

        public Texture2D GetTexture(int value)
        {
            if (_numbersPool.ContainsKey(value))
            {
                return _numbersPool[value];
            }

            return null;
        }
    }
}