using System;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;

namespace MadnessDelivery.GameLogic;

[XmlRoot("route", Namespace = "http://www.univ-grenoble-alpes.fr/l3miage/delivery")]
[Serializable]
public class Route
{
   private Vecteur2 position;

   [XmlElement("position")]
   public Vecteur2 Position
   {
       get => position;
       set => position = value;
   }
    private TypeRoute typeRoute;

    [XmlElement("type")]
    public TypeRoute Type
    {
        get => typeRoute; 
        set => typeRoute = value;
    }

    private Orientation orientation;

    [XmlElement("orientation")]
    public Orientation Orientation
    {
        get =>orientation; 
        set => orientation = value;
    }
    
}