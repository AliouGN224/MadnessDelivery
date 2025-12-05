using System;
using System.Collections.Generic;
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