using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MadnessDelivery;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Sprite _ship; // instance de Sprite
    private static ContentManager _content;
    
    private GameGrid grid;
    private GameMap gameMap;
    

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        grid = new GameGrid();
        gameMap = new GameMap(grid);
        
        // Taille fenêtre 
        _graphics.PreferredBackBufferWidth = 1600;
        _graphics.PreferredBackBufferHeight = 900;
        _graphics.ApplyChanges();
        
        grid.CenterALaFenetre(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        gameMap.LoadContent(Content);
        //Texture2D shipTexture = Content.Load<Texture2D>("vegetations/lightGreen");
        //_ship = new Sprite(shipTexture, new Vector2(5, 5), 10);
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();
        //_ship.Update(gameTime);
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
        //_ship.Draw(_spriteBatch);
        gameMap.Draw(_spriteBatch);
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}