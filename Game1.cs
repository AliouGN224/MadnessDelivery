using System;
using System.Collections.Generic;
using System.IO;
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
    
    private EtatJeu etatCourant = EtatJeu.MENU_PRINCIPAL;
    
    string[] menu =
    {
        "Se connecter pour jouer",
        "S'inscrire pour jouer",
        "Voir les meilleurs scores",
        "Quitter"
    };
    int selectedIndex = 0;
    KeyboardState keyboardPrecedent;
    Texture2D menuBackground;
    SpriteFont menuFont;
    
    string nomSaisi = "";
    string prenomSaisi = "";
    bool saisieNom = true;   // true = on saisit le nom, false = prénom
    string messageConnexion = "";

    Joueur joueurConnecte;
    Joueurs joueurs = new Joueurs { _Joueurs = new List<Joueur>() };
    List<Joueur> listeJoueurs = new();
    int indexJoueurSelectionne = 0;
    
    string[] niveaux = { "FACILE", "MOYEN", "DIFFICILE" };
    int indexNiveauSelectionne = 0;
    string niveauChoisi;
    
    int tempsLimite;         
    float tempsRestant;
    int nbMaisonsALivrer;
    int scorePartie;
    bool partieInitialisee = false;
    bool victoire;
    KeyboardState kbPrevious;
    
    string joueursPath = Path.Combine(AppContext.BaseDirectory, "data/xml/joueurs.xml");
    
    Dictionary<string, BestScoresReader.ScoreInfo> meilleursScores;
    
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
        keyboardPrecedent = Keyboard.GetState();   
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
        menuBackground = Content.Load<Texture2D>("autres/menu");
        menuFont = Content.Load<SpriteFont>("font/FontMenu");
        gameMap.LoadContent(Content);
        //Texture2D shipTexture = Content.Load<Texture2D>("vegetations/lightGreen");
        //_ship = new Sprite(shipTexture, new Vector2(5, 5), 10);
    }

    protected override void Update(GameTime gameTime)
    {
        var keyboard = Keyboard.GetState();
        
        if (keyboard.IsKeyDown(Keys.Escape) && etatCourant == EtatJeu.MENU_PRINCIPAL)
            Exit();
        
        switch (etatCourant)
        {
            case EtatJeu.MENU_PRINCIPAL:
                UpdateMenu();
                break;
            
            case EtatJeu.CONNEXION:
                UpdateConnexion();
                break;
            
            case EtatJeu.CHOIX_NIVEAU:
                UpdateChoixNiveau();
                break;
            
            case EtatJeu.JEU:
                if (!partieInitialisee)
                    InitialiserPartieSelonNiveau();

                gameMap.Update(gameTime);
                
                
                if (gameMap.LivraisonEffectuee) //Si une livraison vient d’avoir lieu +10points
                    scorePartie += 10;
                
                MettreAJourTimer(gameTime);
                VerifierFinPartie();
                break;
            
            case EtatJeu.FIN_PARTIE:
                UpdateFinPartie();
                break;
            
            case EtatJeu.AFFICHAGE_SCORES:
                UpdateMeilleursScores();
                break;
        }

        base.Update(gameTime);
        
        
        
        /*if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();*/
        //_ship.Update(gameTime);
        //gameMap.Update(gameTime);
        //base.Update(gameTime);
    }
    
    


    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
        
        switch (etatCourant)
        {
            case EtatJeu.MENU_PRINCIPAL :
                DrawMenu();
                break;

            case EtatJeu.JEU:
                gameMap.Draw(_spriteBatch);
                DrawHUD();
                break;
            
            case EtatJeu.CONNEXION:
                DrawConnexion();
                break;
            
            case EtatJeu.CHOIX_NIVEAU:
                DrawChoixNiveau();
                break;
            case EtatJeu.FIN_PARTIE:
                DrawFinPartie();
                break;
            
            case EtatJeu.AFFICHAGE_SCORES:
                DrawMeilleursScores();
                break;
        }
        
        //_ship.Draw(_spriteBatch);
        //gameMap.Draw(_spriteBatch);
        _spriteBatch.End();

        base.Draw(gameTime);
    }
    
    // =======================   PARTIE MENU PRINCIPAL =======================

    
    void ValiderMenu()
    {
        switch (selectedIndex)
        {
            case 0:
                joueurs.DeserialiserJoueurs(joueursPath);
                listeJoueurs = joueurs._Joueurs;
                indexJoueurSelectionne = 0;
                etatCourant = EtatJeu.CONNEXION;
                break;

            case 1:
                etatCourant = EtatJeu.JEU; // temporaire
                break;

            case 2:
                var meilleurs = BestScoresReader.LireMeilleursScores("../../../data/xml/joueurs.xml");
                meilleursScores = meilleurs; 
                etatCourant = EtatJeu.AFFICHAGE_SCORES;
                break;

            case 3:
                Exit();
                break;
        }
    }
    
    void DrawMenu()
    {
        Vector2 positionTitre = new Vector2(250, 120);
        Vector2 positionDepart = new Vector2(300, 220);

        _spriteBatch.Draw(menuBackground,
            new Rectangle(0, 0,
                GraphicsDevice.Viewport.Width,
                GraphicsDevice.Viewport.Height),
            Color.White);
        
        for (int i = 0; i < menu.Length; i++)
        {
            Color color = (i == selectedIndex) ? Color.Blue : Color.Black;

            _spriteBatch.DrawString(
                menuFont,
                menu[i],
                positionDepart + new Vector2(0, i * 40),
                color
            );
        }
    }
    
    void UpdateMenu()
    {
        KeyboardState keyboard = Keyboard.GetState();

        if (keyboard.IsKeyDown(Keys.Down) && keyboardPrecedent.IsKeyUp(Keys.Down))
            selectedIndex = (selectedIndex + 1) % menu.Length;

        if (keyboard.IsKeyDown(Keys.Up) && keyboardPrecedent.IsKeyUp(Keys.Up))
            selectedIndex = (selectedIndex - 1 + menu.Length) % menu.Length;

        if (keyboard.IsKeyDown(Keys.Enter) && keyboardPrecedent.IsKeyUp(Keys.Enter))
            ValiderMenu();

        keyboardPrecedent = keyboard;
    }
    
    // =======================   PARTIE CONNEXION =======================
    void UpdateConnexion()
    {
        KeyboardState keyboard = Keyboard.GetState();

        if (listeJoueurs.Count == 0)
            return;

        if (keyboard.IsKeyDown(Keys.Down) && keyboardPrecedent.IsKeyUp(Keys.Down))
            indexJoueurSelectionne = (indexJoueurSelectionne + 1) % listeJoueurs.Count;

        if (keyboard.IsKeyDown(Keys.Up) && keyboardPrecedent.IsKeyUp(Keys.Up))
            indexJoueurSelectionne = (indexJoueurSelectionne - 1 + listeJoueurs.Count) % listeJoueurs.Count;

        if (keyboard.IsKeyDown(Keys.Enter) && keyboardPrecedent.IsKeyUp(Keys.Enter))
        {
            joueurConnecte = listeJoueurs[indexJoueurSelectionne];
            etatCourant = EtatJeu.CHOIX_NIVEAU;
        }

        if (keyboard.IsKeyDown(Keys.Escape) && keyboardPrecedent.IsKeyUp(Keys.Escape))
            etatCourant = EtatJeu.MENU_PRINCIPAL;
        
        keyboardPrecedent = keyboard;
    }
    
    void ValiderConnexion()
    {
        joueurs.DeserialiserJoueurs(joueursPath);

        foreach (var j in joueurs._Joueurs)
        {
            if (j.Nom.Equals(nomSaisi, StringComparison.OrdinalIgnoreCase) &&
                j.Prenom.Equals(prenomSaisi, StringComparison.OrdinalIgnoreCase))
            {
                joueurConnecte = j;
                etatCourant = EtatJeu.CHOIX_NIVEAU;
                return;
            }
        }

        messageConnexion = "Joueur introuvable";
    }
    
    void DrawConnexion()
    {
        _spriteBatch.Draw(menuBackground,
            new Rectangle(0, 0,
                GraphicsDevice.Viewport.Width,
                GraphicsDevice.Viewport.Height),
            Color.White);

        Vector2 pos = new Vector2(300, 200);

        _spriteBatch.DrawString(menuFont, "SELECTION DU JOUEUR", pos, Color.Black);
        pos.Y += 60;

        if (listeJoueurs.Count == 0)
        {
            _spriteBatch.DrawString(menuFont,
                "Aucun joueur enregistre",
                pos, Color.Red);
            return;
        }

        for (int i = 0; i < listeJoueurs.Count; i++)
        {
            Color color = (i == indexJoueurSelectionne) ? Color.Blue : Color.Black;

            string texte = $"{listeJoueurs[i].Nom} {listeJoueurs[i].Prenom}";

            _spriteBatch.DrawString(
                menuFont,
                texte,
                pos + new Vector2(0, i * 40),
                color
            );
        }

        pos.Y += listeJoueurs.Count * 40 + 40;

        _spriteBatch.DrawString(menuFont,
            "ENTREE : valider | ESC : retour",
            pos, Color.DarkGray);
    }
    
    // =======================   PARTIE Choix du niveau =======================
    
    void UpdateChoixNiveau()
    {
        KeyboardState keyboard = Keyboard.GetState();

        if (keyboard.IsKeyDown(Keys.Down) && keyboardPrecedent.IsKeyUp(Keys.Down))
            indexNiveauSelectionne = (indexNiveauSelectionne + 1) % niveaux.Length;

        if (keyboard.IsKeyDown(Keys.Up) && keyboardPrecedent.IsKeyUp(Keys.Up))
            indexNiveauSelectionne = (indexNiveauSelectionne - 1 + niveaux.Length) % niveaux.Length;

        if (keyboard.IsKeyDown(Keys.Enter) && keyboardPrecedent.IsKeyUp(Keys.Enter))
        {
            niveauChoisi = niveaux[indexNiveauSelectionne];
            etatCourant = EtatJeu.JEU; // la partie commence
        }

        if (keyboard.IsKeyDown(Keys.Escape) && keyboardPrecedent.IsKeyUp(Keys.Escape))
            etatCourant = EtatJeu.CONNEXION;

        keyboardPrecedent = keyboard;
    }
    
    
    void DrawChoixNiveau()
    {
        _spriteBatch.Draw(menuBackground,
            new Rectangle(0, 0,
                GraphicsDevice.Viewport.Width,
                GraphicsDevice.Viewport.Height),
            Color.White);

        Vector2 pos = new Vector2(300, 220);

        _spriteBatch.DrawString(menuFont, "CHOIX DU NIVEAU", pos, Color.Black);
        pos.Y += 60;

        for (int i = 0; i < niveaux.Length; i++)
        {
            Color color = (i == indexNiveauSelectionne) ? Color.Blue : Color.Black;

            _spriteBatch.DrawString(
                menuFont,
                niveaux[i],
                pos + new Vector2(0, i * 40),
                color
            );
        }

        pos.Y += niveaux.Length * 40 + 40;

        _spriteBatch.DrawString(menuFont,
            "ENTREE : valider | ESC : retour",
            pos, Color.DarkGray);
    }
    
    // =======================   INITIALISATION DE LA PARTIE SELON LE NIVEAU =======================
    void InitialiserPartieSelonNiveau()
    {
        switch (niveauChoisi)
        {
            case "FACILE":
                nbMaisonsALivrer = 2;
                tempsLimite = 150;
                break;

            case "MOYEN":
                nbMaisonsALivrer = 4;
                tempsLimite = 110;
                break;

            case "DIFFICILE":
                nbMaisonsALivrer = 5;
                tempsLimite = 90;
                break;
        }

        tempsRestant = tempsLimite;
        scorePartie = 0;

        gameMap.InitialiserMaisonsALivrer(nbMaisonsALivrer);

        partieInitialisee = true;
    }
    
    void MettreAJourTimer(GameTime gameTime)
    {
        tempsRestant -= (float)gameTime.ElapsedGameTime.TotalSeconds;
    }
    
    void VerifierFinPartie()
    {
        if (tempsRestant <= 0)
        {
            // Défaite
            SauvegarderScoreFinal();
            etatCourant = EtatJeu.FIN_PARTIE;
            partieInitialisee = false;
        }

        if (gameMap.ToutesMaisonsLivrees())
        {
            // Victoire
            victoire = true;
            SauvegarderScoreFinal();
            etatCourant = EtatJeu.FIN_PARTIE;
            partieInitialisee = false;
        }
    }
    
    //  Tableau d'affichage 
    
    void DrawHUD()
    {
        if (!partieInitialisee || nbMaisonsALivrer == 0)
            return;
        
        Vector2 pos = new Vector2(20, 20);

        string timerTxt = "Temps : " + Math.Max(0, (int)tempsRestant) + " s";
        string maisonsTxt = "Maisons : " + gameMap.GetNbMaisonsRestantes() + " / " + nbMaisonsALivrer;
        string scoreTxt = "Score : " + scorePartie;

        _spriteBatch.DrawString(menuFont, timerTxt, pos, Color.White);
        pos.Y += 30;

        _spriteBatch.DrawString(menuFont, maisonsTxt, pos, Color.White);
        pos.Y += 30;

        _spriteBatch.DrawString(menuFont, scoreTxt, pos, Color.White);
    }
    
    // ================================= FIN DE MANCHE
    void UpdateFinPartie()
    {
        KeyboardState keyboard = Keyboard.GetState();

        if (keyboard.IsKeyDown(Keys.Enter) && keyboardPrecedent.IsKeyUp(Keys.Enter))
        {
            etatCourant = EtatJeu.MENU_PRINCIPAL;
        }

        keyboardPrecedent = keyboard;
    }
    
    
    void DrawFinPartie()
    {
        _spriteBatch.Draw(menuBackground,
            new Rectangle(0, 0,
                GraphicsDevice.Viewport.Width,
                GraphicsDevice.Viewport.Height),
            Color.White);

        Vector2 pos = new Vector2(300, 250);

        string titre = gameMap.ToutesMaisonsLivrees() ? "VICTOIRE !" : "DEFAITE";

        _spriteBatch.DrawString(menuFont, titre, pos, Color.Blue);
        pos.Y += 60;

        _spriteBatch.DrawString(menuFont,
            "Score final : " + scorePartie,
            pos, Color.Black);

        pos.Y += 60;

        _spriteBatch.DrawString(menuFont,
            "ENTREE : retour menu",
            pos, Color.DarkGray);
    }
    
    void SauvegarderScoreFinal()
    {
        ScoresDOM scoresDom = new ScoresDOM("../../../data/xml/joueurs.xml");

        scoresDom.AddScore(
            joueurConnecte.JoueurId,
            niveauChoisi,
            (uint)scorePartie
        );
        
        
        /*Scores scores = new Scores();
        string path = Path.GetFullPath("../../../data/xml/joueurs.xml");
        Console.WriteLine("XML PATH = " + path);
        scores.AddScore(
            "../../../data/xml/joueurs.xml",
            joueurConnecte.JoueurId,
            niveauChoisi,
            (uint)scorePartie
        );*/
    }
    
    void DrawMeilleursScores()
    {
        _spriteBatch.Draw(menuBackground,
            new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height),
            Color.White);

        Vector2 pos = new Vector2(250, 200);

        _spriteBatch.DrawString(menuFont, "MEILLEURS SCORES", pos, Color.Black);
        pos.Y += 80;

        foreach (var niveau in new[] {"FACILE", "MOYEN", "DIFFICILE"})
        {
            var info = meilleursScores[niveau];

            string txt = $"{niveau} : {info.Score} points — par {info.Nom} {info.Prenom}";
            foreach (char c in txt)
            {
                if (!menuFont.Characters.Contains(c))
                    Console.WriteLine("CARACTÈRE MANQUANT : '" + c + "'  (code: " + (int)c + ")");
            }
            
            
            _spriteBatch.DrawString(menuFont, txt, pos, Color.Blue);

            pos.Y += 50;
        }

        pos.Y += 40;
        _spriteBatch.DrawString(menuFont, "ENTREE : retour", pos, Color.DarkGray);
    }
    
    void UpdateMeilleursScores()
    {
        var kb = Keyboard.GetState();
        if (kb.IsKeyDown(Keys.Enter) && kbPrevious.IsKeyUp(Keys.Enter))
        {
            etatCourant = EtatJeu.MENU_PRINCIPAL;
        }

        kbPrevious = kb;
    }
    
}