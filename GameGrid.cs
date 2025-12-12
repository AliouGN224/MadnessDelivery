using MadnessDelivery.GameLogic;
using Microsoft.Xna.Framework;

namespace MadnessDelivery;

public class GameGrid
{
    public const int TILE_SIZE = 49;

    public const int COLS = 40;
    public const int ROWS = 34;

    public Vector2 offset;
    public CellType[,] Cells { get; set; }

    public GameGrid()
    {
        Cells = new CellType[COLS, ROWS];
        ResetEnHerbe();
    }

    public void ResetEnHerbe()
    {
        for (int x = 0; x < COLS; x++)
        for (int y = 0; y < ROWS; y++)
            Cells[x, y] = CellType.HERBRE;
    }

    // Convertir case → position écran
    public Vector2 VersPositionEcran(int col, int row)
    {
        float isoX = (col - row) * (TILE_SIZE / 2f);
        float isoY = (col + row) * (TILE_SIZE / 4f);

        return new Vector2(isoX + offset.X, isoY + offset.Y);
    }
     

    public void CenterALaFenetre(int windowWidth, int windowHeight)
    {
        // 1) Dimensions projetées d'une tuile isométrique
        float demiLargeurTuile = TILE_SIZE / 2f;
        float quartHauteurTuile = TILE_SIZE / 4f;

        // 2) Calcul des bornes min/max de la carte PROJETÉE en isométrique
        // X = (col - row) * demiLargeurTuile
        float xMinCarte = -(ROWS - 1) * demiLargeurTuile;
        float xMaxCarte =  (COLS - 1) * demiLargeurTuile;

        // Y = (col + row) * quartHauteurTuile
        float yMinCarte = 0f;
        float yMaxCarte = (COLS + ROWS - 2) * quartHauteurTuile;

        // 3) Centre de la carte (dans l'espace projeté)
        float centreCarteX = (xMinCarte + xMaxCarte) / 2f;
        float centreCarteY = (yMinCarte + yMaxCarte) / 2f;

        // 4) Centre de la fenêtre d'affichage
        float centreFenetreX = windowWidth / 2f;
        float centreFenetreY = windowHeight / 2f;

        // 5) Offset pour aligner le centre carte ↔ centre fenêtre
        float offsetX = centreFenetreX - centreCarteX;
        float offsetY = centreFenetreY - centreCarteY;

        offset = new Vector2(offsetX, offsetY);
    }
}