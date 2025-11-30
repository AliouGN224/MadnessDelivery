public class GameGrid
{
    public const int TILE_SIZE = 64;

    public const int COLS = 20;
    public const int ROWS = 12;

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
                Cells[x, y] = CellType.Herbe;
    }

    // Convertir case → position écran
    public Vector2 ToWorld(int col, int row)
    {
        return new Vector2(col * TILE_SIZE, row * TILE_SIZE);
    }
}
