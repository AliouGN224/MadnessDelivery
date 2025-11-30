using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MadnessDelivery;

public class GameMap
{
    private GameGrid grid;
    
    private Texture2D texHerbe;
    private Dictionary<TypeRoute, Dictionary<Orientation, Texture2D>> texturesRoutes;

    
    public GameMap(GameGrid grid)
    {
        this.grid = grid;
    }
    
    // Chargement des textures
    public void LoadContent(ContentManager content)
    {
        texHerbe = content.Load<Texture2D>("vegetations/lightGreenBorders");
        texturesRoutes[TypeRoute.DROITE] = content.Load<Texture2D>("Routes/route_droite");
        texturesRoutes[TypeRoute.VIRAGE] = content.Load<Texture2D>("Routes/route_virage");
        texturesRoutes[TypeRoute.PASSAGEPIETON] = content.Load<Texture2D>("Routes/route_passage");
        texturesRoutes[TypeRoute.GIRATOIRE] = content.Load<Texture2D>("Routes/blueCross");
        texturesRoutes[TypeRoute.DIRECTION3] = content.Load<Texture2D>("Routes/route_3direction");
        
        texturesRoutes[TypeRoute.PASSAGEPIETON][Orientation.NORD_EST]   = content.Load<Texture2D>("routes/blueCrosswalk3");
        texturesRoutes[TypeRoute.PASSAGEPIETON][Orientation.Deg90]  = passagePieton90;
        texturesRoutes[TypeRoute.PASSAGEPIETON][Orientation.Deg180] = passagePieton180;
        texturesRoutes[TypeRoute.PASSAGEPIETON][Orientation.Deg270] = passagePieton270;
    }
    
    // Affichage de la grille
    public void Draw(SpriteBatch spriteBatch)
    {
        for (int col = 0; col < GameGrid.COLS; col++)
        {
            for (int row = 0; row < GameGrid.ROWS; row++)
            {
                Vector2 pos = grid.ToWorld(col, row);
                spriteBatch.Draw(texHerbe, pos, Color.White);
            }
        }
    }
}