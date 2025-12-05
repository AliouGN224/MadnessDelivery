using Microsoft.Xna.Framework;

namespace MadnessDelivery.GameLogic;

public class Route
{
    public int Col { get; private set; }
    public int Row { get; private set; }
    public TypeRoute Type { get; private set; }
    public Orientation Orientation { get; private set; }

    public Route(int col, int row, TypeRoute type, Orientation orientation)
    {
        Col = col;
        Row = row;
        Type = type;
        Orientation = orientation;
    }
}