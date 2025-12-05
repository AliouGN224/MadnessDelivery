using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace MadnessDelivery.GameLogic;
[XmlRoot("joueur", Namespace = "http://www.univ-grenoble-alpes.fr/l3miage/delivery")]
[Serializable]
public class Joueur
{
    private uint _joueurId;

    [XmlAttribute("id")]
    public uint JoueurId
    {
        get => _joueurId;
        set => _joueurId = value;
    }
    private String nom;

    [XmlElement("nom")]
    public String Nom
    {
        get => nom;
        set => nom = value;
    }
    private String prenom;

    [XmlElement("prenom")]
    public String Prenom
    {
        get => prenom;
        set => prenom = value;
    }
    private Niveaux niveaux;

    [XmlElement("niveaux")]
    public Niveaux _Niveaux
    {
        get => niveaux;
        set => niveaux = value;
        
    }
    
    
}