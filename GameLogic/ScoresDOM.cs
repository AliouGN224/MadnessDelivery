using System;
using System.Xml;

namespace MadnessDelivery.GameLogic;

public class ScoresDOM
{
    private XmlDocument doc;
    private XmlNode root;
    private XmlNamespaceManager nsmgr;

    public ScoresDOM(string xmlPath)
    {
        doc = new XmlDocument();
        doc.Load(xmlPath);

        root = doc.DocumentElement;

        nsmgr = new XmlNamespaceManager(doc.NameTable);
        nsmgr.AddNamespace("d", root.NamespaceURI);

        XmlPath = xmlPath;
    }

    public string XmlPath { get; }

    // -------------------- CREATION SCORE SANS PREFIXE --------------------
    private XmlElement MakeScore(uint valeurScore)
    {
        // aucun prefixe -> null
        XmlElement scoreElt = doc.CreateElement(null, "score", root.NamespaceURI);

        XmlElement valeurElt = doc.CreateElement(null, "valeurScore", root.NamespaceURI);
        valeurElt.InnerText = valeurScore.ToString();

        scoreElt.AppendChild(valeurElt);
        return scoreElt;
    }

    // -------------------- AJOUT SCORE --------------------
    public void AddScore(uint joueurId, string niveauChoisi, uint valeurScore)
    {
        string xpath =
            $"//d:joueur[@id='{joueurId}']/d:niveaux/d:niveau[d:difficulte='{niveauChoisi}']/d:scores";

        XmlNode scoresNode = doc.SelectSingleNode(xpath, nsmgr);
        if (scoresNode == null)
        {
            Console.WriteLine("Impossible de trouver le noeud <scores>");
            return;
        }

        XmlElement nouveauScore = MakeScore(valeurScore);

        scoresNode.AppendChild(nouveauScore);

        doc.Save(XmlPath);

        Console.WriteLine($"Score {valeurScore} ajouté dans {XmlPath}");
    }
}