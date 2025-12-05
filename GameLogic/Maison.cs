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

    [XmlElement("position")]
    public Vecteur2 Position
    {
        get => position;
        set => position = value;
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
[XmlRoot("Vector2", Namespace = "http://www.univ-grenoble-alpes.fr/l3miage/delivery")]
[Serializable]
public class Vecteur2
{
    private double _x;

    [XmlElement("X")]
    public double _X
    {
        get => _x;
        set => _x = value;
    }
    private double _y;

    [XmlElement("Y")]
    public double _Y
    {
        get => _y;
        set => _y = value;
    }

    public void Zero()
    {
        this._x = 0;
        this._y = 0;
    }
}