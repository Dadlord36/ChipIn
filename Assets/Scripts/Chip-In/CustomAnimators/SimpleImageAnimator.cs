using System.Collections.Generic;
using Common.Interfaces;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Utilities;

namespace CustomAnimators
{
    public interface IImageAnimator
    {
        /*void InitializeAnimator(SimpleImageAnimator.SpritesAnimatorResource spritesAnimatorResource,
            float updateInterval, bool loopTheAnimation);*/

        void StartAnimating();
        void Pause();
        void StopAnimating();
    }

    public class SimpleImageAnimator : UIBehaviour, IImageAnimator, IInitialize
    {
        public struct SpritesSheet
        {
            public readonly Texture2D SpritesSheetTexture;
            public readonly int RowsNumber, ColumnsNumber;

            public SpritesSheet(Texture2D spritesSheetTexture, int rowsNumber, int columnsNumber)
            {
                SpritesSheetTexture = spritesSheetTexture;
                RowsNumber = rowsNumber;
                ColumnsNumber = columnsNumber;
            }
        }

        public struct SpritesAnimatorResource
        {
            public readonly List<Sprite> SpritesSequence;

            public SpritesAnimatorResource(SpritesSheet spritesSheet)
            {
                SpritesSequence = SpriteSheetUtility.GetSprites(spritesSheet.SpritesSheetTexture,
                    new Vector2Int(spritesSheet.RowsNumber, spritesSheet.ColumnsNumber));
            }

            public SpritesAnimatorResource(List<Sprite> spritesSequence)
            {
                SpritesSequence = spritesSequence;
            }
        }

        [SerializeField] private Image image;

        private Sprite[] _sprites;
        private float _spriteUpdateInterval;
        private float _playbackLength;
        private int _spriteIndex;
        private bool _playInLoop;

        private Sprite ImageSprite
        {
            get => image.sprite;
            set => image.sprite = value;
        }

        public void Setup(SpritesAnimatorResource resource, float updateInterval,
            bool loopTheAnimation = false)
        {
            StopAnimating();
            _sprites = resource.SpritesSequence.ToArray();
            _spriteUpdateInterval = updateInterval;
            _playInLoop = loopTheAnimation;
            _playbackLength = (_sprites.Length - 1) * _spriteUpdateInterval;
        }

        public void Initialize()
        {
            StopPlaying();
        }

        public void StartAnimating()
        {
            enabled = true;
        }

        public void StopAnimating()
        {
            StopPlaying();
        }

        public void Pause()
        {
            enabled = false;
        }

        private float _progress;
        private float _time;


        private int SpriteIndex
        {
            get => _spriteIndex;
            set
            {
                if (_spriteIndex == value) return;
                _spriteIndex = value;
                ImageSprite = _sprites[_spriteIndex];
            }
        }

        private void Update()
        {
            _time += Time.deltaTime;
            _progress = Mathf.InverseLerp(0, _playbackLength, _time);
            SpriteIndex = (int) (_time / _spriteUpdateInterval);

            if (!(_progress >= 1.0f)) return;

            if (_playInLoop)
            {
                RestartAnimation();
            }
            else
            {
                StopPlaying();
            }
        }

        private void ResetTrackingVariables()
        {
            _time = _progress = 0f;
        }

        private void RestartAnimation()
        {
            ResetTrackingVariables();
        }

        private void StopPlaying()
        {
            enabled = false;
            ResetTrackingVariables();
        }
    }
}