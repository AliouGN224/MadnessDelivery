using MadnessDelivery.GameLogic;
using Microsoft.Xna.Framework;

namespace MadnessDelivery;

public class VilleGenerateur01
{
    public static void GenererQuartierSimple(GameGrid grid, Routes routes)
    {
        // -------------------------------
        // 1) Route PRINCIPALE (horizontale ISO)
        // -------------------------------
        int rowPrincipale = GameGrid.ROWS / 2;
        int marge = 4;

        for (int col = marge; col < GameGrid.COLS - marge; col++)
        {
            grid.Cells[col, rowPrincipale] = CellType.ROUTE;

            routes.AjouterRoute(
                new Route(col, rowPrincipale, TypeRoute.DROITE, Orientation.NORD_EST)
            );
        }

        // -------------------------------
        // 2) Routes SECONDAIRES (verticales ISO)
        // -------------------------------
        int[] colonnesSecondaires = {
            GameGrid.COLS / 4,
            GameGrid.COLS / 2,
            3 * GameGrid.COLS / 4
        };

        int demiHauteur = 8;

        foreach (int c in colonnesSecondaires)
        {
            for (int row = rowPrincipale - demiHauteur; row <= rowPrincipale + demiHauteur; row++)
            {
                if (row < 0 || row >= GameGrid.ROWS)
                    continue;

                grid.Cells[c, row] = CellType.ROUTE;

                routes.AjouterRoute(
                    new Route(c, row, TypeRoute.DROITE, Orientation.NORD_OUEST) // verticale
                );
            }
        }

        // -------------------------------
        // 3) GIRATOIRES aux croisements
        // -------------------------------
        foreach (int c in colonnesSecondaires)
        {
            grid.Cells[c, rowPrincipale] = CellType.ROUTE;

            routes.AjouterRoute(
                new Route(c, rowPrincipale, TypeRoute.GIRATOIRE, Orientation.NORD_EST)
            );
        }

        // -------------------------------
        // 4) CUL-DE-SAC en haut des routes secondaires
        // -------------------------------
        foreach (int c in colonnesSecondaires)
        {
            int culRow = rowPrincipale - demiHauteur;
            if (culRow < 0) continue;

            grid.Cells[c, culRow] = CellType.ROUTE;

            routes.AjouterRoute(
                new Route(c, culRow, TypeRoute.LIMITE, Orientation.SUD_EST)
            );
        }

        // -------------------------------
        // 5) VIRAGES en bas des routes secondaires
        // -------------------------------
        foreach (int c in colonnesSecondaires)
        {
            int virageRow = rowPrincipale + demiHauteur;
            if (virageRow >= GameGrid.ROWS) continue;

            grid.Cells[c, virageRow] = CellType.ROUTE;

            routes.AjouterRoute(
                new Route(c, virageRow, TypeRoute.VIRAGE, Orientation.NORD_EST)
            );
        }

        // -------------------------------
        // 6) RUES RÉSIDENTIELLES à partir des routes secondaires
        // -------------------------------
        AjouterRueResidentielleHorizontale(grid, routes, colonnesSecondaires[0], rowPrincipale - 4, 5, true);
        AjouterRueResidentielleHorizontale(grid, routes, colonnesSecondaires[0], rowPrincipale + 4, 5, true);

        AjouterRueResidentielleHorizontale(grid, routes, colonnesSecondaires[1], rowPrincipale - 6, 6, false);
        AjouterRueResidentielleHorizontale(grid, routes, colonnesSecondaires[1], rowPrincipale + 6, 6, true);

        AjouterRueResidentielleHorizontale(grid, routes, colonnesSecondaires[2], rowPrincipale - 5, 5, false);
        AjouterRueResidentielleHorizontale(grid, routes, colonnesSecondaires[2], rowPrincipale + 5, 5, false);

        // -------------------------------
        // 7) PASSAGE PIÉTON sur la route principale
        // -------------------------------
        foreach (int c in colonnesSecondaires)
        {
            int rowCross = rowPrincipale - 2;
            if (rowCross < 0 || rowCross >= GameGrid.ROWS) continue;

            grid.Cells[c, rowCross] = CellType.ROUTE;

            routes.AjouterRoute(
                new Route(c, rowCross, TypeRoute.PASSAGEPIETON, Orientation.NORD_EST)
            );
        }
    }

    // ------------------------------------------------------
    // Rue horizontale partant d’une route secondaire
    // ------------------------------------------------------
    private static void AjouterRueResidentielleHorizontale(
        GameGrid grid,
        Routes routes,
        int colDepart,      // colonne où la rue touche la route verticale
        int row,            // ligne de la rue
        int longueur,       // nombre de cases de la rue
        bool versDroite     // true : va vers la droite, false : vers la gauche
    )
    {
        if (row < 0 || row >= GameGrid.ROWS)
            return;

        int direction = versDroite ? 1 : -1;

        // 1) Case de jonction : T (DIRECTION3)
        if (colDepart >= 0 && colDepart < GameGrid.COLS)
        {
            grid.Cells[colDepart, row] = CellType.ROUTE;

            routes.AjouterRoute(
                new Route(
                    colDepart,
                    row,
                    TypeRoute.DIRECTION3,
                    versDroite ? Orientation.SUD_EST : Orientation.SUD_OUEST
                )
            );
        }

        // 2) Corps de la rue
        for (int i = 1; i < longueur; i++)
        {
            int col = colDepart + i * direction;
            if (col < 0 || col >= GameGrid.COLS)
                break;

            grid.Cells[col, row] = CellType.ROUTE;

            routes.AjouterRoute(
                new Route(
                    col,
                    row,
                    TypeRoute.DROITE,
                    Orientation.NORD_EST    // horizontale
                )
            );
        }

        // 3) Cul-de-sac au bout
        int colCul = colDepart + longueur * direction;
        if (colCul >= 0 && colCul < GameGrid.COLS)
        {
            grid.Cells[colCul, row] = CellType.ROUTE;

            routes.AjouterRoute(
                new Route(
                    colCul,
                    row,
                    TypeRoute.LIMITE,
                    versDroite ? Orientation.SUD_EST : Orientation.SUD_OUEST
                )
            );
        }
    }
}