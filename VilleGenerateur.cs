using MadnessDelivery.GameLogic;
using Microsoft.Xna.Framework;

namespace MadnessDelivery;

public class VilleGenerateur
{
    public static void ConstruireVilleManuelle(GameGrid grid, Routes routes, Maisons maisons)
    {
        // =========================================================
        // 2) 3 ROUTE PRINCIPALE (horizontale)
        // =========================================================
        int rowPrincipale2 = GameGrid.ROWS / 2;
        int rowPrincipale1 = rowPrincipale2 / 2;
        int rowPrincipale3 = rowPrincipale1 + rowPrincipale2;

        TracerRouteHorizontale(grid, routes, 4, GameGrid.COLS - 5, rowPrincipale1);
        TracerRouteHorizontale(grid, routes, 4, GameGrid.COLS - 5, rowPrincipale2);
        TracerRouteHorizontale(grid, routes, 4, GameGrid.COLS - 5, rowPrincipale3);



        // =========================================================
        // 3) 3 ROUTE SECONDAIRE VERTICALE
        // =========================================================
        int colVertical1 = GameGrid.COLS / 4;
        int colVertical2 = GameGrid.COLS / 2;
        int colVertical3 = (GameGrid.COLS / 4) * 3;

        TracerRouteVerticale(grid, routes, colVertical1, rowPrincipale1 - 5, rowPrincipale2);
        TracerRouteVerticale(grid, routes, colVertical2, rowPrincipale1 - 5, rowPrincipale3);
        TracerRouteVerticale(grid, routes, colVertical3, rowPrincipale2, rowPrincipale3);
        
        
        // 3 giratoire
        PlacerGiratoire(grid, routes, colVertical1, rowPrincipale1);
        PlacerGiratoire(grid, routes, colVertical2, rowPrincipale1);
        PlacerGiratoire(grid, routes, colVertical2, rowPrincipale2);

        // 4 T
        PlacerIntersectionT(grid, routes, colVertical1, rowPrincipale2, Orientation.SUD_OUEST);
        PlacerIntersectionT(grid, routes, colVertical3, rowPrincipale2, Orientation.NORD_OUEST);
        PlacerIntersectionT(grid, routes, colVertical2, rowPrincipale3, Orientation.SUD_OUEST);
        PlacerIntersectionT(grid, routes, colVertical3, rowPrincipale3, Orientation.SUD_OUEST);

        //
        TracerRouteHorizontale(grid, routes,
            colVertical2,         // depuis la route verticale
            colVertical2 + 6,     // longueur 6 cellules vers la droite
            rowPrincipale1 - 4    // ligne juste au-dessus du giratoire
        );
        
        TracerRouteVerticale(grid, routes,
            colVertical3 - 4,     // juste à droite de la rue principale
            rowPrincipale1 + 1,   // commence sous la route haute
            rowPrincipale2 - 6    // descend jusqu'au quart de route centrale
        );
        TracerRouteHorizontale(grid, routes,
            colVertical3 - 1,         // depuis la route verticale
            colVertical3 - 7,     // longueur 6 cellules vers la droite
            rowPrincipale2 - 6    // ligne juste au-dessus du giratoire
        );
        
        TracerRouteVerticale(grid, routes,
            colVertical3 - 4,     // juste à droite de la rue principale
            rowPrincipale1 + 6,   // commence sous la route haute
            rowPrincipale2 - 1    // descend jusqu'au quart de route centrale
        );
        TracerRouteHorizontale(grid, routes,
            colVertical3 - 1,         // depuis la route verticale
            colVertical3 - 7,     // longueur 6 cellules vers la droite
            rowPrincipale2 - 3    // ligne juste au-dessus du giratoire
        );
        
        PlacerVirage(grid, routes,
            colVertical3,             // la dernière case verticale
            rowPrincipale2 - 3,
            Orientation.SUD_OUEST           // orientation virage bas → droite
        );
        PlacerVirage(grid, routes,
            colVertical3,             // la dernière case verticale
            rowPrincipale2 - 6,
            Orientation.NORD_OUEST           // orientation virage bas → droite
        );
        TracerRouteVerticale(grid, routes,
            colVertical3,     // juste à droite de la rue principale
            rowPrincipale2 - 4,   // commence sous la route haute
            rowPrincipale2 - 5    // descend jusqu'au quart de route centrale
        );
        
        TracerRouteVerticale(grid, routes,
            colVertical3 + 3,     // juste à droite de la rue principale
            rowPrincipale2,   // commence sous la route haute
            rowPrincipale1    // descend jusqu'au quart de route centrale
        );
        
        TracerRouteVerticale(grid, routes,
            colVertical2 - 4,     // juste à droite de la rue principale
            rowPrincipale3,   // commence sous la route haute
            rowPrincipale3 - 5    // descend jusqu'au quart de route centrale
        );
        
        TracerRouteVerticale(grid, routes, colVertical3 + 4, rowPrincipale3 - 3, GameGrid.ROWS - 4);

        int r1 = rowPrincipale3 - 4;

        TracerRouteHorizontale(grid, routes, colVertical3, GameGrid.COLS - 7, r1);
        PlacerVirage(grid, routes, colVertical3, r1, Orientation.SUD_EST);
        PlacerVirage(grid, routes, GameGrid.COLS - 6, r1, Orientation.NORD_OUEST);
        
        
        int r2 = r1 - 3;

        TracerRouteVerticale(grid, routes, GameGrid.COLS - 7, r2, r1);
        //PlacerVirage(grid, routes, GameGrid.COLS - 8, r2, Orientation.NORD_EST);

        //TracerRouteHorizontale(grid, routes, colVertical3 + 4, GameGrid.COLS - 8, r2 + 2);
        //PlacerVirage(grid, routes, colVertical3 + 4, r2, Orientation.SUD_OUEST);
       
        
        
        // =========================================================
        // 4) GIRATOIRE AU CROISEMENT
        //    (on remplace la case de croisement par un GIRATOIRE)
        // =========================================================

        // =========================================================
        // 5) UNE PETITE RUE RÉSIDENTIELLE (à droite du croisement)
        // =========================================================
     
        // Intersection en T au point de jonction

        // Cul-de-sac au bout de la petite rue

        // =========================================================
        // 6) UNE AUTRE RUE RÉSIDENTIELLE (à gauche du croisement)
        // =========================================================
       
        // =========================================================
        // 7) PASSAGE PIÉTON à côté du GIRATOIRE
        // =========================================================

        for (int i = 4; i < colVertical2 - 1; i++)
        {
            PlacerMaison(grid, maisons, i, rowPrincipale2 + 1, TypeMaison.MAISON_C);
            i = i + 1;
        }
        for (int i = colVertical1 + 1; i < colVertical2 - 1; i++)
        {
            PlacerMaison(grid, maisons, i, rowPrincipale2 - 2, TypeMaison.MAISON_A);
            //i = i + 1;
        }
        
        for (int i = colVertical1 + 1; i < colVertical2 - 1; i++)
        {
            PlacerMaison(grid, maisons, i, rowPrincipale2 - 1, TypeMaison.MAISON_A);
            i = i + 1;
        }
        
        for (int i = rowPrincipale2 + 3; i < rowPrincipale3 - 2; i++)
        {
            PlacerMaison(grid, maisons, colVertical2 - 3, i, TypeMaison.MAISON_A);
            i = i + 1;
        }
        
        //PlacerMaison(grid, maisons, 18, rowPrincipale2 - 2, TypeMaison.BOUTIQUE_A);

        //PlacerMaison(grid, maisons, 8, rowPrincipale2 - 2, TypeMaison.MAISON_A);
        PlacerMaison(grid, maisons, 8, rowPrincipale2 - 2, TypeMaison.DEPART);
        
        for (int col = colVertical1 + 2; col < colVertical2 - 1; col += 2)
        {
            PlacerMaison(grid, maisons, col, rowPrincipale2 + 2, TypeMaison.MAISON_A);
        }
        
       /* for (int col = colVertical1 + 2; col < colVertical2 - 1; col += 2)
        {
            PlacerMaison(grid, maisons, col, rowPrincipale2 - 2, TypeMaison.MAISON_B);
        } */
       for (int col = colVertical1 + 2; col < colVertical3 - 1; col += 2)
       {
           PlacerMaison(grid, maisons, col, rowPrincipale3 + 1, TypeMaison.MAISON_A);
       }
       
       for (int row = rowPrincipale1 + 2; row < rowPrincipale3 - 1; row += 2)
       {
           PlacerMaison(grid, maisons, colVertical2 - 1, row, TypeMaison.MAISON_C);
       }
       
       for (int row = rowPrincipale1 + 2; row < rowPrincipale3 - 1; row += 2)
       {
           PlacerMaison(grid, maisons, colVertical2 + 1, row, TypeMaison.MAISON_D);
       }
       
       
       
       
       
       
       
       
       ////////////////////////////////////////////////////////////
       // Maisons SOUS la route du haut, entre la verticale gauche et la verticale droite
       for (int col = colVertical1 + 2; col < colVertical3 - 2; col += 2)
       {
           PlacerMaison(grid, maisons, col, rowPrincipale1 + 1, TypeMaison.MAISON_A);
       }
       
       // Maisons AU-DESSUS de la route du bas, dans la zone centrale
       for (int col = colVertical1 + 1; col < colVertical3 - 2; col += 2)
       {
           PlacerMaison(grid, maisons, col, rowPrincipale3 - 1, TypeMaison.MAISON_B);
       }
       
       // Maisons à DROITE de la verticale gauche, entre route du haut et route du bas
       for (int row = rowPrincipale1 + 3; row < rowPrincipale3 - 3; row += 2)
       {
           PlacerMaison(grid, maisons, colVertical1 + 2, row, TypeMaison.MAISON_C);
       }
       
       // Maisons à GAUCHE de la verticale droite, dans la partie haute de la ville
       for (int row = rowPrincipale1 + 4; row < rowPrincipale2 - 2; row += 2)
       {
           PlacerMaison(grid, maisons, colVertical3 - 2, row, TypeMaison.MAISON_D);
       }
       
       // Boutique près de l'intersection gauche au centre
       PlacerMaison(grid, maisons, colVertical1 - 4, rowPrincipale2 - 1, TypeMaison.BOUTIQUE_A);

       // Boutique près de l'intersection droite au centre
       PlacerMaison(grid, maisons, colVertical3 + 2, rowPrincipale2 - 2, TypeMaison.BOUTIQUE_B);
    }

    // ---------------------------------------------------------
    // OUTILS SIMPLES
    // ---------------------------------------------------------

    private static void TracerRouteHorizontale(GameGrid grid, Routes routes,
                                               int colDebut, int colFin, int row)
    {
        if (row < 0 || row >= GameGrid.ROWS) return;
        if (colDebut > colFin) (colDebut, colFin) = (colFin, colDebut);

        for (int col = colDebut; col <= colFin; col++)
        {
            if (col < 0 || col >= GameGrid.COLS) continue;

            grid.Cells[col, row] = CellType.ROUTE;
            routes.AjouterRoute(
                new Route {
                    Orientation = Orientation.NORD_EST,
                    Position = new Vecteur2 { _X = col, _Y = row },
                    Type = TypeRoute.DROITE
                }
            );
        }
    }

    private static void TracerRouteVerticale(GameGrid grid, Routes routes,
                                             int col, int rowDebut, int rowFin)
    {
        if (col < 0 || col >= GameGrid.COLS) return;
        if (rowDebut > rowFin) (rowDebut, rowFin) = (rowFin, rowDebut);

        for (int row = rowDebut; row <= rowFin; row++)
        {
            if (row < 0 || row >= GameGrid.ROWS) continue;

            grid.Cells[col, row] = CellType.ROUTE;
            routes.AjouterRoute(
                new Route {
                    Orientation = Orientation.NORD_OUEST,
                    Position = new Vecteur2 { _X = col, _Y = row },
                    Type = TypeRoute.DROITE
                }
            );
        }
    }

    private static void PlacerGiratoire(GameGrid grid, Routes routes,
                                        int col, int row)
    {
        if (col < 0 || col >= GameGrid.COLS || row < 0 || row >= GameGrid.ROWS) return;

        grid.Cells[col, row] = CellType.ROUTE;
        routes.AjouterRoute(
            new Route {
                Orientation = Orientation.NORD_EST,
                Position = new Vecteur2 { _X = col, _Y = row },
                Type = TypeRoute.GIRATOIRE
            }
        );
    }

    private static void PlacerIntersectionT(GameGrid grid, Routes routes,
                                            int col, int row, Orientation orientation)
    {
        if (col < 0 || col >= GameGrid.COLS || row < 0 || row >= GameGrid.ROWS) return;

        grid.Cells[col, row] = CellType.ROUTE;
        routes.AjouterRoute(
            new Route {
                Orientation = orientation,
                Position = new Vecteur2 { _X = col, _Y = row },
                Type = TypeRoute.DIRECTION3
            }
        );
    }

    private static void PlacerVirage(GameGrid grid, Routes routes,
                                     int col, int row, Orientation orientation)
    {
        if (col < 0 || col >= GameGrid.COLS || row < 0 || row >= GameGrid.ROWS) return;

        grid.Cells[col, row] = CellType.ROUTE;
        routes.AjouterRoute(
            new Route {
                Orientation = orientation,
                Position = new Vecteur2 { _X = col, _Y = row },
                Type = TypeRoute.VIRAGE
            }
        );
    }

    private static void PlacerCulDeSac(GameGrid grid, Routes routes,
                                       int col, int row, Orientation orientation)
    {
        if (col < 0 || col >= GameGrid.COLS || row < 0 || row >= GameGrid.ROWS) return;

        grid.Cells[col, row] = CellType.ROUTE;
        routes.AjouterRoute(
            new Route {
                Orientation = orientation,
                Position = new Vecteur2 { _X = col, _Y = row },
                Type = TypeRoute.LIMITE
            }
        );
    }

    private static void PlacerPassagePieton(GameGrid grid, Routes routes,
                                            int col, int row, Orientation orientation)
    {
        if (col < 0 || col >= GameGrid.COLS || row < 0 || row >= GameGrid.ROWS) return;

        grid.Cells[col, row] = CellType.ROUTE;
        routes.AjouterRoute(
            new Route {
                Orientation = orientation,
                Position = new Vecteur2 { _X = col, _Y = row },
                Type = TypeRoute.PASSAGEPIETON
            }
        );
    }
    
    public static void PlacerMaison(
        GameGrid grid,
        Maisons maisons,
        int col,
        int row,
        TypeMaison typeMaison
    )
    {
        // On ne place jamais une maison sur une route
        if (grid.Cells[col, row] == CellType.ROUTE)
            return;

        grid.Cells[col, row] = CellType.MAISON;

        Maison m = new Maison
        {
            _Id = maisons.GetNextId(),
            _Type = typeMaison,
            AEteLivrer = false,
            EstALivrer = false,
            Position = new Vecteur2 { _X = col, _Y = row },
        };
        maisons.AjouterMaison(m);
    }
}