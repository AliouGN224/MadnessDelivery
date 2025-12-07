using System;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;

namespace MadnessDelivery.GameLogic;
[XmlRoot("maison", Namespace = "http://www.univ-grenoble-alpes.fr/l3miage/delivery")]
[Serializable]
public class Maison
{
    private uint id;
    [XmlAttribute("id")]
    public uint _Id
    {
       get => id;
       set => id = value;
    }
    private Vecteur2 position;

    // Pour le XML
    [XmlElement("position")]
    public Vecteur2 Position
    {
        get => position;
        set => position = value;
    }
    
    // Propriété pour le jeu
    [XmlIgnore]
    public Vector2 PositionJeu
    {
        get => position.ToVector2();
        set => position = Vecteur2.FromVector2(value);
    }
    
    private bool estALivrer;

    [XmlElement("estALivrer")]
    public bool EstALivrer
    {
        get => estALivrer;
        set => estALivrer = value;
    }
    private bool aEteLivrer;

    [XmlElement("aEteLivrer")]
    public bool AEteLivrer
    {
        get => aEteLivrer;
        set => aEteLivrer = value;
    }
    private TypeMaison type;

    [XmlElement("type")]
    public TypeMaison _Type
    {
        get => type;
        set => type = value;
    }
    // Constructeur 1 
    
    // 
    public void livrer()
    {
        this.AEteLivrer = true;
        this.EstALivrer = false;
    }

    // 
}
[XmlRoot("Vecteur2", Namespace = "http://www.univ-grenoble-alpes.fr/l3miage/delivery")]
[Serializable]
public class Vecteur2
{
    private int _x;

    [XmlElement("X")]
    public int _X
    {
        get => _x;
        set => _x = value;
    }
    private int _y;

    [XmlElement("Y")]
    public int _Y
    {
        get => _y;
        set => _y = value;
    }

    public void Zero()
    {
        this._x = 0;
        this._y = 0;
    }
    
    public static Vecteur2 FromVector2(Vector2 v)
    {
        return new Vecteur2
        {
            _X = (int)v.X,
            _Y = (int)v.Y
        };
    }

    public Vector2 ToVector2()
    {
        return new Vector2(_X, _Y);
    }
}