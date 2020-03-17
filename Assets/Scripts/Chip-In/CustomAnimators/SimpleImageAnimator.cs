using System.Collections.Generic;
using Common.Interfaces;
using DataModels.Interfaces;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Utilities;

namespace CustomAnimators
{
    public interface IImageAnimator
    {
        void StartAnimating();
        void Pause();
        void StopAnimating();
    }

    public class SimpleImageAnimator : UIBehaviour, IImageAnimator, IInitialize
    {
        public struct SpritesSheet : IIdentifier
        {
            public readonly Texture2D SpritesSheetTexture;
            public readonly int RowsNumber, ColumnsNumber;

            public int? Id { get; set; }

            public SpritesSheet(Texture2D spritesSheetTexture, int rowsNumber, int columnsNumber, int? id)
            {
                SpritesSheetTexture = spritesSheetTexture;
                RowsNumber = rowsNumber;
                ColumnsNumber = columnsNumber;
                Id = id;
            }
        }

        public struct SpritesAnimatorResource
        {
            public readonly List<Sprite> SpritesSequence;

            public SpritesAnimatorResource(SpritesSheet spritesSheet)
            {
                SpritesSequence = SpriteSheetUtility.GetSpritesFromSpritesSheet(spritesSheet.SpritesSheetTexture,
                    spritesSheet.RowsNumber, spritesSheet.ColumnsNumber);
            }
        }

        [SerializeField] private Image image;

        private SpritesAnimatorResource _spritesAnimatorResource;
        private float _spriteUpdateInterval;
        private float _playbackLength;
        private int _spriteIndex;
        private bool _playInLoop;

        private Sprite ImageSprite
        {
            get => image.sprite;
            set => image.sprite = value;
        }

        public void Setup(SpritesAnimatorResource resource, float updateInterval, bool loopTheAnimation = false)
        {
            StopAnimating();
            _spritesAnimatorResource = resource;
            _spriteUpdateInterval = updateInterval;
            _playInLoop = loopTheAnimation;
            _playbackLength = (_spritesAnimatorResource.SpritesSequence.Count - 1) * _spriteUpdateInterval;
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
                ImageSprite = _spritesAnimatorResource.SpritesSequence[_spriteIndex];
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