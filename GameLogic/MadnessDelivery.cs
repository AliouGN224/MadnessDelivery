using System;
using System.IO;
using System.Xml.Serialization;

namespace MadnessDelivery.GameLogic;

[XmlRoot("madnessDelivery", Namespace = "http://www.univ-grenoble-alpes.fr/l3miage/delivery")]
[Serializable]
public class MadnessDelivery
{
    private Routes routes;

    [XmlElement("routes")]
    public Routes _Routes
    {
        get => routes;
        set
        {
            if (value != null)
            {
                routes = value;
            }
        }
    }
    private Maisons maisons;

    [XmlElement("maisons")]
    public Maisons _Maisons
    {
        get => maisons;
        set
        {
            if (value != null)
            {
                maisons = value;
            }
            
        }
    }

    private Joueurs joueurs;

    [XmlElement("joueurs")]
    public Joueurs _Joueurs
    {
        get => joueurs;
        set
        {
            if (value != null)
            {
                joueurs = value;
            }
        }
    }
    
    public void serialiserMadnessDelivery(string filepath)
    {
        using (var writer = new StreamWriter(filepath))
        {
            var xmlMadness = new XmlSerializer(typeof(MadnessDelivery));
            xmlMadness.Serialize(writer, this);
        }
    } 
}