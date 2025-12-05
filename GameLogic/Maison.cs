using Microsoft.Xna.Framework;

namespace MadnessDelivery.GameLogic;

public class Maison
{
    private uint id;
    private Vector2 position;
    private bool estALivrer;
    private bool aEteLivrer;
    private TypeMaison type;

    private TypeMaison typeMaison;
    // Constructeur 1 
    public Maison(uint id)
    {
        this.id = id;
        this.position = Vector2.Zero;
        this.estALivrer = false;
        this.aEteLivrer = false;
    }

    // Constructeur 2
    public Maison(uint id, Vector2 position, TypeMaison typeMaison)
    {
        this.id = id;
        this.position = position;
        this.type = typeMaison;
        this.estALivrer = false;
        this.aEteLivrer = false;
    }

    // 
    public uint getId() => id;

    public Vector2 getPosition() => position;

    public bool getEstAlivrer() => estALivrer;

    public bool getAEteLivrer() => aEteLivrer;
    
    public TypeMaison getTypeMaison() => type;

    // 
    public void setPosition(Vector2 pos)
    {
        position = pos;
    }

    public void setEstALivrer(bool est)
    {
        estALivrer = est;
    }

    public void setAEteLivrer(bool livre)
    {
        aEteLivrer = livre;
    }

    // 
    public void livrer()
    {
        aEteLivrer = true;
        estALivrer = false;
    }

    // 
    public Vector2 Position
    {
        get => position;
        set => position = value;
    }
}