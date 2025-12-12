using System.Collections.Generic;
using System.Xml;

namespace MadnessDelivery.GameLogic;

public static class BestScoresReader
{
    public class ScoreInfo
    {
        public uint Score;
        public string Nom;
        public string Prenom;
    }

    public static Dictionary<string, ScoreInfo> LireMeilleursScores(string xmlPath)
    {
        var best = new Dictionary<string, ScoreInfo>
        {
            ["FACILE"] = new ScoreInfo {Score = 0},
            ["MOYEN"] = new ScoreInfo {Score = 0},
            ["DIFFICILE"] = new ScoreInfo {Score = 0}
        };

        XmlReader reader = XmlReader.Create(xmlPath);

        string nom = "";
        string prenom = "";
        string difficulte = "";
        uint valeurScore = 0;

        while (reader.Read())
        {
            if (reader.NodeType == XmlNodeType.Element)
            {
                switch (reader.Name)
                {
                    case "nom":
                        nom = reader.ReadElementContentAsString();
                        break;

                    case "prenom":
                        prenom = reader.ReadElementContentAsString();
                        break;

                    case "difficulte":
                        difficulte = reader.ReadElementContentAsString();
                        break;

                    case "valeurScore":
                        valeurScore = (uint)reader.ReadElementContentAsInt();

                        if (best.ContainsKey(difficulte) &&
                            valeurScore > best[difficulte].Score)
                        {
                            best[difficulte].Score = valeurScore;
                            best[difficulte].Nom = nom;
                            best[difficulte].Prenom = prenom;
                        }
                        break;
                }
            }
        }

        reader.Close();
        return best;
    }
}