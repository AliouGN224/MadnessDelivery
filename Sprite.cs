using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MadnessDelivery;

public class Sprite
{
    private Texture2D _texture;
    protected Vector2 _position;
    private int _size = 100;
    private static readonly int _sizeMin = 10;
    private Color _color = Color.White;
    private Vector2 _speed; // la vitesse

    public Texture2D _Texture { get => _texture; init => _texture = value; }
    public int _Size { get => _size; set => _size = value >= _sizeMin ? value : _sizeMin; }
    public Rectangle _Rect { get => new Rectangle((int) _position.X, (int) _position.Y, _size, _size); }

    public Sprite(Texture2D texture, Vector2 position, int size) {
        _Texture = texture;
        _position = position;
        _Size = size;
    }

    public void Update(GameTime gameTime) {
        if (Keyboard.GetState().IsKeyDown(Keys.Up))
        {
            _speed.Y -= 0.5f;
        }
        if (Keyboard.GetState().IsKeyDown(Keys.Down))
        {
            _speed.Y += 0.5f;
        }
        if (Keyboard.GetState().IsKeyDown(Keys.Right))
        {
            _speed.X += 0.5f;
        }
        if (Keyboard.GetState().IsKeyDown(Keys.Left))
        {
            _speed.X -= 0.5f;
        }

        _position.X += _speed.X;
        _position.Y += _speed.Y;

        if (_speed.X > 0) _speed.X -= 0.05f;
        if (_speed.X < 0) _speed.X += 0.1f;
        if (_speed.Y > 0) _speed.Y -= 0.1f;
        if (_speed.Y < 0) _speed.Y += 0.1f;
    }

    public void Draw(SpriteBatch spriteBatch) {
        var origin = new Vector2(_texture.Width / 2f, _texture.Height / 2f);
        spriteBatch.Draw(
            _texture,           // Texture2D
            _Rect,              // Rectangle destination
            null,               // Nullable<Rectangle> sourceRectangle
            _color,             // Color
            0.0f,               // float rotation
            origin,             // Vector2 origin
            SpriteEffects.None, // SpriteEffects effects
            0f                  // float layerDepth
        );
    } 
}