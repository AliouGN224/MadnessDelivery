using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace MadnessDelivery.GameLogic;
[XmlRoot("niveau",Namespace = "http://www.univ-grenoble-alpes.fr/l3miage/delivery")]
[Serializable]
public class Niveau
{
    private Level difficulte;

    [XmlElement("difficulte")]
    public Level Difficulte
    {
        get => difficulte;
        set => difficulte = value;
    }
    private Scores scores;

    [XmlElement("scores")]
    public Scores _Scores
    {
        get => scores;
        set => scores = value;
    }

}

[XmlRoot("scores",Namespace = "http://www.univ-grenoble-alpes.fr/l3miage/delivery")]
[Serializable]
public class Scores
{
    private List<Score> score;

    [XmlElement("score")]
    public List<Score> _Score
    {
        get => score;
        set => score = value;
    }

    public void AjouterScore(Score score)
    {
        this.score.Add(score);
    }
    
    /*private XmlElement MakeScore(uint valeurScore)
    {
        XmlElement scoreElt = doc.CreateElement(null, "score", root.NamespaceURI);

        XmlElement valElt = doc.CreateElement(null, "valeurScore", root.NamespaceURI);
        valElt.InnerText = valeurScore.ToString();

        scoreElt.AppendChild(valElt);
        return scoreElt;
    }
    
    public void AddScore(
        string xmlPath,
        uint joueurId,
        string niveauChoisi,
        uint valeurScore
    )
    {
        doc.Load(xmlPath);

        XmlNamespaceManager ns = new XmlNamespaceManager(doc.NameTable);
        ns.AddNamespace("d", root.NamespaceURI);

        // Trouver le bon <scores>
        string xpath = $"//d:joueur[@id='{joueurId}']/d:niveaux/d:niveau[d:difficulte='{niveauChoisi}']/d:scores";

        XmlNode scoresNode = doc.SelectSingleNode(xpath, ns);
        if (scoresNode == null)
            return;

        // → Ajouter un score SANS PREFIXE
        XmlElement newScore = MakeScore(valeurScore);
        scoresNode.AppendChild(newScore);

        doc.Save(xmlPath);
    }*/
}

[XmlRoot("score", Namespace = "http://www.univ-grenoble-alpes.fr/l3miage/delivery")]
[Serializable]
public class Score
{
    private uint valeurScore;
     [XmlElement("valeurScore")]
    public uint ValeurScore
    {
        get => valeurScore;
        set => valeurScore = value;
    }
}