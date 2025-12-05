using System.Xml.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MadnessDelivery.GameLogic;

public class Livreur
{
    private Vector2 position;   
    private Orientation orientation; 
    private float vitesse;
    
    [XmlElement("position")]
    public Vector2 Position
    {
        get => position;
        set => position = value;
    }
    
    [XmlElement("orientation")]
    public Orientation Orientation
    {
        get => orientation;
        set => orientation = value;
    }
    
    [XmlElement("vitesse")]
    public float Vitesse
    {
        get => vitesse;
        set => vitesse = value;
    }
}