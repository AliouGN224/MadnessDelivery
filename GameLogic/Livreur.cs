using System.Xml.Serialization;
using Microsoft.Xna.Framework;
namespace MadnessDelivery.GameLogic;

public class Livreur
{
    private Vecteur2 position;   
    private Orientation orientation; 
    private float vitesse;
    
    [XmlElement("position")]
    public Vecteur2 Position
    {
        get => position;
        set => position = value;
    }
    
    [XmlIgnore]
    public Vector2 PositionJeu
    {
        get => position.ToVector2();
        set => position = Vecteur2.FromVector2(value);
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