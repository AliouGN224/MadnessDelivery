using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MadnessDelivery.GameLogic;

public class Joueur
{
    public Vector2 Position { get; private set; }
    public Orientation Direction { get; private set; } = Orientation.NORD_EST;

    private Texture2D sprite;
    private float vitesse = 2.0f;       // vitesse basique
    private float taille = 0.5f;        // scale du sprite
    private bool peutBouger = true;

    public Joueur(Texture2D texture, Vector2 depart)
    {
        sprite = texture;
        Position = depart;
    }
    
    public void Update(GameTime gameTime, GameGrid grid)
    {
        if (!peutBouger)
            return;

        KeyboardState k = Keyboard.GetState();

        Vector2 mouvement = Vector2.Zero;

        if (k.IsKeyDown(Keys.Up))
        {
            mouvement.Y -= vitesse;
            Direction = Orientation.NORD_EST; // a adapter plus tard
        }
        if (k.IsKeyDown(Keys.Down))
        {
            mouvement.Y += vitesse;
            Direction = Orientation.SUD_OUEST;
        }
        if (k.IsKeyDown(Keys.Left))
        {
            mouvement.X -= vitesse;
            Direction = Orientation.NORD_OUEST;
        }
        if (k.IsKeyDown(Keys.Right))
        {
            mouvement.X += vitesse;
            Direction = Orientation.SUD_EST;
        }

        // On ajoute le mouvement
        Position += mouvement;

    }
    
    public void Draw(SpriteBatch sb)
    {
        if (sprite == null)
            return;

        sb.Draw(
            sprite,
            Position,
            null,
            Color.White,
            0f,
            new Vector2(sprite.Width / 2f, sprite.Height / 2f),
            taille,
            SpriteEffects.None,
            1f
        );
    }
}