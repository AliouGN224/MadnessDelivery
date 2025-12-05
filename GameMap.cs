using System.Collections.Generic;
using MadnessDelivery.GameLogic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MadnessDelivery;

public class GameMap
{
    private GameGrid grid;
    private Routes routes;
    private Maisons maisons;
    private Livreur livreur;

    
    private Texture2D texHerbe;
    private Dictionary<TypeRoute, Dictionary<Orientation, Texture2D>> texturesRoutes;
    private Dictionary<TypeMaison, Texture2D> texturesMaisons;
    private Dictionary<Orientation, Texture2D> texturesLivreur;
    
    private Orientation orientationCouranteLivreur = Orientation.NORD_EST;
    
    public float Echelle = 0.49f; // 50% de la taille d’origine
    public float EchelleMaison = 0.30f;  //  0.70 et 0.90
    public float EchelleLivreur = 0.05f;
    private KeyboardState oldState;
    
    public GameMap(GameGrid grid, Routes routes, Maisons maisons)
    {
        this.grid = grid;
        this.routes = routes;
        this.maisons = maisons;

    }
    
    public void Update(GameTime gameTime)
    {
        GererDeplacementLivreur(gameTime);
    }
    
    // Chargement des textures
    public void LoadContent(ContentManager content)
    {
        texHerbe = content.Load<Texture2D>("vegetations/lightGreenBorders");
        // ---------- Texture livreur et initailisation du livreur ---------------
        texturesLivreur = new Dictionary<Orientation, Texture2D>();
        texturesLivreur[Orientation.NORD_EST]  = content.Load<Texture2D>("livreur/livreurNE");
        texturesLivreur[Orientation.SUD_EST]   = content.Load<Texture2D>("livreur/livreurSE");
        texturesLivreur[Orientation.SUD_OUEST] = content.Load<Texture2D>("livreur/livreurSW");
        texturesLivreur[Orientation.NORD_OUEST]= content.Load<Texture2D>("livreur/livreurNW");
        
        int colDepart = GameGrid.COLS / 2;
        int rowDepart = GameGrid.ROWS / 2;
        livreur = new Livreur
        {
            Position = new Vector2(colDepart, rowDepart),
            Orientation = Orientation.SUD_EST,  // orientation de départ
            Vitesse = 0.1f
        };
        orientationCouranteLivreur = Orientation.SUD_EST;
        
        // ---------- Texture Routes---------------
        texturesRoutes = new Dictionary<TypeRoute, Dictionary<Orientation, Texture2D>>();

        texturesRoutes[TypeRoute.DROITE]        = new Dictionary<Orientation, Texture2D>();
        texturesRoutes[TypeRoute.PASSAGEPIETON] = new Dictionary<Orientation, Texture2D>();
        texturesRoutes[TypeRoute.VIRAGE]        = new Dictionary<Orientation, Texture2D>();
        texturesRoutes[TypeRoute.LIMITE]        = new Dictionary<Orientation, Texture2D>();
        texturesRoutes[TypeRoute.DIRECTION3]    = new Dictionary<Orientation, Texture2D>();
        texturesRoutes[TypeRoute.GIRATOIRE]     = new Dictionary<Orientation, Texture2D>();
        // Routes droites
        texturesRoutes[TypeRoute.DROITE][Orientation.NORD_EST]  = content.Load<Texture2D>("routes/blueRoad0"); // horizontal (0°)
        texturesRoutes[TypeRoute.DROITE][Orientation.NORD_OUEST] = content.Load<Texture2D>("routes/blueRoad1"); // vertical  (90°)
        texturesRoutes[TypeRoute.DROITE][Orientation.SUD_EST]    = texturesRoutes[TypeRoute.DROITE][Orientation.NORD_EST];
        texturesRoutes[TypeRoute.DROITE][Orientation.SUD_OUEST]  = texturesRoutes[TypeRoute.DROITE][Orientation.NORD_OUEST];

        // Passage piéton
        texturesRoutes[TypeRoute.PASSAGEPIETON][Orientation.NORD_EST]  = content.Load<Texture2D>("routes/blueCrosswalk0");
        texturesRoutes[TypeRoute.PASSAGEPIETON][Orientation.NORD_OUEST] = content.Load<Texture2D>("routes/blueCrosswalk1");
        texturesRoutes[TypeRoute.PASSAGEPIETON][Orientation.SUD_EST]    = content.Load<Texture2D>("routes/blueCrosswalk2");
        texturesRoutes[TypeRoute.PASSAGEPIETON][Orientation.SUD_OUEST]  = content.Load<Texture2D>("routes/blueCrosswalk3");
        // Virages
        texturesRoutes[TypeRoute.VIRAGE][Orientation.NORD_EST]  = content.Load<Texture2D>("routes/blueCurve0");
        texturesRoutes[TypeRoute.VIRAGE][Orientation.NORD_OUEST] = content.Load<Texture2D>("routes/blueCurve1");
        texturesRoutes[TypeRoute.VIRAGE][Orientation.SUD_EST]    = content.Load<Texture2D>("routes/blueCurve2");
        texturesRoutes[TypeRoute.VIRAGE][Orientation.SUD_OUEST]  = content.Load<Texture2D>("routes/blueCurve3");
        // CUL-DE-SAC
        texturesRoutes[TypeRoute.LIMITE][Orientation.NORD_EST]  = content.Load<Texture2D>("routes/blueEnd0");
        texturesRoutes[TypeRoute.LIMITE][Orientation.NORD_OUEST] = content.Load<Texture2D>("routes/blueEnd1png");
        texturesRoutes[TypeRoute.LIMITE][Orientation.SUD_EST]    = content.Load<Texture2D>("routes/blueEnd2");
        texturesRoutes[TypeRoute.LIMITE][Orientation.SUD_OUEST]  = content.Load<Texture2D>("routes/blueEnd3");
        // INTERSECTION EN T
        texturesRoutes[TypeRoute.DIRECTION3][Orientation.NORD_EST]   = content.Load<Texture2D>("routes/blueT0");
        texturesRoutes[TypeRoute.DIRECTION3][Orientation.NORD_OUEST] = content.Load<Texture2D>("routes/blueT1");
        texturesRoutes[TypeRoute.DIRECTION3][Orientation.SUD_EST]    = content.Load<Texture2D>("routes/blueT2");
        texturesRoutes[TypeRoute.DIRECTION3][Orientation.SUD_OUEST]  = content.Load<Texture2D>("routes/blueT3");
        // INTERSECTION 4 DIRECTIONS
        Texture2D cross = content.Load<Texture2D>("routes/blueCross");
        texturesRoutes[TypeRoute.GIRATOIRE][Orientation.NORD_EST]  = cross;
        texturesRoutes[TypeRoute.GIRATOIRE][Orientation.SUD_OUEST] = cross;
        texturesRoutes[TypeRoute.GIRATOIRE][Orientation.SUD_EST]    = cross;
        texturesRoutes[TypeRoute.GIRATOIRE][Orientation.NORD_OUEST]  = cross;

        
        // ---------- Texture Routes---------------

        texturesMaisons = new Dictionary<TypeMaison, Texture2D>();
        texturesMaisons[TypeMaison.DEPART]    = content.Load<Texture2D>("maisons/building0");

        texturesMaisons[TypeMaison.MAISON_A]  = content.Load<Texture2D>("maisons/pinkHouse0");
        texturesMaisons[TypeMaison.MAISON_B]  = content.Load<Texture2D>("maisons/pinkHouse1");
        texturesMaisons[TypeMaison.MAISON_C]  = content.Load<Texture2D>("maisons/purpleHouse0");
        texturesMaisons[TypeMaison.MAISON_D]  = content.Load<Texture2D>("maisons/purpleHouse1");

        texturesMaisons[TypeMaison.BOUTIQUE_A] = content.Load<Texture2D>("maisons/shop0");
        texturesMaisons[TypeMaison.BOUTIQUE_B] = content.Load<Texture2D>("maisons/shop1");
    }
    
    // Affichage de la grille
    public void Draw(SpriteBatch spriteBatch)
    {
        DessinerHerbe(spriteBatch);
        DessinerRoutes(spriteBatch);
        DessinerMaisons(spriteBatch);
        DessinerLivreur(spriteBatch);
    }
    
    // Pour Herbe
    public void DessinerHerbe(SpriteBatch spriteBatch)
    {
        for (int col = 0; col < GameGrid.COLS; col++)
        {
            for (int row = 0; row < GameGrid.ROWS; row++)
            {
                Vector2 pos = grid.VersPositionEcran(col, row);
                spriteBatch.Draw(texHerbe, pos, null, Color.White, 0f, Vector2.Zero, Echelle, SpriteEffects.None, 0f);
            }
        }
    }
    
    
    private void DessinerRoutes(SpriteBatch sb)
    {
        // On dessine en mode isométrique, diagonale par diagonale.
        for (int profondeur = 0; profondeur < GameGrid.COLS + GameGrid.ROWS; profondeur++)
        {
            for (int col = 0; col < GameGrid.COLS; col++)
            {
                int row = profondeur - col;

                if (row < 0 || row >= GameGrid.ROWS)
                    continue;

                // Vérifier si cette cellule est une route
                if (grid.Cells[col, row] != CellType.ROUTE)
                    continue;

                // Récupérer la route correspondante
                Route route = routes.GetRoute(col, row);
                if (route == null)
                    continue;

                // Texture correspondant au type ET orientation
                Texture2D tex = texturesRoutes[route.Type][route.Orientation];

                // Position isométrique
                Vector2 pos = grid.VersPositionEcran(col, row);

                sb.Draw(tex, pos, null, Color.White, 0f, Vector2.Zero, Echelle, SpriteEffects.None, 0f);
            }
        }
    }

    
    private void DessinerMaisons(SpriteBatch sb)
    {
        foreach (Maison m in maisons.getToutesLesMaisons())
        {
            Vector2 grillePos = m.getPosition();

            // Conversion grille → écran
            Vector2 posEcran = grid.VersPositionEcran(
                (int)grillePos.X,
                (int)grillePos.Y
            );

            // Récupérer la texture selon TypeMaison
            Texture2D tex = texturesMaisons[m.getTypeMaison()];

            sb.Draw(
                tex,
                posEcran,
                null,
                Color.White,
                0f,
                Vector2.Zero,
                EchelleMaison,
                SpriteEffects.None,
                0f
            );
        }
    }
    
    private void DessinerLivreur(SpriteBatch sb)
    {
        if (livreur == null)
            return;

        // Position grille → position écran
        int col = (int)livreur.Position.X;
        int row = (int)livreur.Position.Y;

        Vector2 posEcran = grid.VersPositionEcran(col, row);

        Texture2D tex = texturesLivreur[orientationCouranteLivreur];

        sb.Draw(
            tex,
            posEcran,
            null,
            Color.White,
            0f,
            Vector2.Zero,
            EchelleLivreur,        
            SpriteEffects.None,
            1f
        );
    }
    
    
    private void GererDeplacementLivreur(GameTime gameTime)
    {
        KeyboardState ks = Keyboard.GetState();
        
        bool pressUp    = ks.IsKeyDown(Keys.Up)    && oldState.IsKeyUp(Keys.Up);
        bool pressDown  = ks.IsKeyDown(Keys.Down)  && oldState.IsKeyUp(Keys.Down);
        bool pressLeft  = ks.IsKeyDown(Keys.Left)  && oldState.IsKeyUp(Keys.Left);
        bool pressRight = ks.IsKeyDown(Keys.Right) && oldState.IsKeyUp(Keys.Right);
        
        
        // Position actuelle en grille
        int col = (int)livreur.Position.X;
        int row = (int)livreur.Position.Y;

        int newCol = col;
        int newRow = row;
        
        Orientation newOri = livreur.Orientation;
        
        // Gestion des directions 
        if (pressUp)
        {
            newRow -= 1;
            newOri = Orientation.NORD_EST;
        }
        else if (pressDown)
        {
            newRow += 1;
            newOri = Orientation.SUD_OUEST;
        }
        else if (pressLeft)
        {
            newCol -= 1;
            newOri = Orientation.NORD_OUEST;
        }
        else if (pressRight)
        {
            newCol += 1;
            newOri = Orientation.SUD_EST;
        }
        else
        {
            oldState = ks;
            return; // si aucune touche pressé, rien a faire
        }

        if (EstDemiTour(newOri, livreur.Orientation))
        {
            oldState = ks;
            return;  // on refuse le demi-tour
        }
        
        // Verifier bornes grille
        if (newCol < 0 || newCol >= GameGrid.COLS || newRow < 0 || newRow >= GameGrid.ROWS)
        {
            oldState = ks;
            return;
        }
        
        // Verifier route
        if (grid.Cells[newCol, newRow] != CellType.ROUTE)
        {
            oldState = ks;
            return;
        }
        
        // application du déplacement
        livreur.Position = new Vector2(newCol, newRow);
        livreur.Orientation = newOri;
        orientationCouranteLivreur = newOri;

        oldState = ks;  // mémoriser létat des touches
    }
    
    // Pour verifier si c'est sens opposé
    private bool EstDemiTour(Orientation nouvelle, Orientation ancienne)
    {
        return
            (ancienne == Orientation.NORD_EST  && nouvelle == Orientation.SUD_OUEST) ||
            (ancienne == Orientation.SUD_OUEST && nouvelle == Orientation.NORD_EST) ||
            (ancienne == Orientation.NORD_OUEST && nouvelle == Orientation.SUD_EST) ||
            (ancienne == Orientation.SUD_EST   && nouvelle == Orientation.NORD_OUEST);
    }
    
}