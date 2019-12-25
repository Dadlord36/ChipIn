using UnityEngine;
using UnityEngine.Assertions;
using UnityWeld.Binding;

namespace BindingAdapters
{
    [Adapter(typeof(Texture2D), typeof(Sprite))]
    public class Texture2DToSprite : IAdapter
    {
        public object Convert(object valueIn, AdapterOptions options)
        {
            var texture = valueIn as Texture2D;
            Assert.IsNotNull(texture);
            return Sprite.Create(texture, new Rect(0f, 0f, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        }
    }
}