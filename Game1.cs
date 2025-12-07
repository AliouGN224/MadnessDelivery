using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using MadnessDelivery.GameLogic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MadnessDelivery.GameLogic;
namespace MadnessDelivery;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Sprite _ship; // instance de Sprite
    private static ContentManager _content;
    
    private GameGrid grid;
    private GameMap gameMap;
    /*private Routes routes;

    [XmlElement("routes")]
    public Routes Routes
    {
        get => routes;
        set
        {
            if (value != null)
            {
                routes = value;
            }
        }
    }
    private Maisons maisons;

    [XmlElement("maisons")]
    public Maisons _Maisons
    {
        get => maisons;
        set
        {
            if (value != null)
            {
                maisons = value;
            }
            
        }
    }

    private Joueurs joueurs;

    [XmlElement("joueurs")]
    public Joueurs _Joueurs
    {
        get => joueurs;
        set
        {
            if (value != null)
            {
                joueurs = value;
            }
        }
    }
    */

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        grid = new GameGrid();
        
        GameLogic.MadnessDelivery madnessDelivery = new GameLogic.MadnessDelivery
        {
            _Routes = new Routes{ListeRoute = new List<Route>()},
            _Joueurs = new Joueurs{_Joueurs = new List<Joueur>()},
            _Maisons = new Maisons{_ListeMaisons = new List<Maison>()}
        };
        /*routes = new Routes{ListeRoute = new List<Route>()};
        maisons = new Maisons();*/
        gameMap = new GameMap(grid, madnessDelivery._Routes, madnessDelivery._Maisons);
        
        
        // Taille fenêtre 
        _graphics.PreferredBackBufferWidth = 1950;
        _graphics.PreferredBackBufferHeight = 1100;
        _graphics.ApplyChanges();
        
        grid.CenterALaFenetre(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
        
        
 /*       // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        int c0 = GameGrid.COLS / 2;
        int r0 = GameGrid.ROWS / 2;

        // 4 virages
        routes.AjouterRoute(new Route(2, 2,     TypeRoute.VIRAGE,      Orientation.NORD_EST));
        grid.Cells[2, 2] = CellType.Route;

        routes.AjouterRoute(new Route(2, c0,     TypeRoute.VIRAGE,      Orientation.SUD_EST));
        grid.Cells[2, c0] = CellType.Route;

        routes.AjouterRoute(new Route(r0,     2,     TypeRoute.VIRAGE,      Orientation.NORD_OUEST));
        grid.Cells[r0, 2] = CellType.Route;

        routes.AjouterRoute(new Route(r0, c0,     TypeRoute.VIRAGE,      Orientation.SUD_OUEST));
        grid.Cells[r0, c0] = CellType.Route;
        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
 */       
        
        VilleGenerateur.ConstruireVilleManuelle(grid, madnessDelivery._Routes, madnessDelivery._Maisons); 
        /*grid.Cells[10, 8] = CellType.Route;
        routes.AjouterRoute(
            new Route(new Vector2(10, 8), TypeRoute.DROITE, Orientation.NORD_EST)
        );*/
        
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
        gameMap.Update(gameTime);
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