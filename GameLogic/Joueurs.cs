using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace MadnessDelivery.GameLogic;
[XmlRoot("joueurs", Namespace = "http://www.univ-grenoble-alpes.fr/l3miage/delivery")]
[Serializable]
public class Joueurs
{
    private List<Joueur> joueurs;
    [XmlElement("joueur")]
    public List<Joueur> _Joueurs
    {
        get => joueurs;
        set => joueurs = value;
    }

    public void AddJoueur(Joueur joueur)
    {
        joueurs.Add(joueur);
    }

    public Joueur GetJoueur(uint id)
    {
        Joueur joueur = null;
        if (this.joueurs != null)
        {
            int i = 0;
            while ((i < this.joueurs.Count) && (this.joueurs[i].JoueurId != id))
            {
                i++;
            }

            if (i < this.joueurs.Count)
            {
                joueur = this.joueurs[i];
            }
            else
            {
                Console.WriteLine("Aucune route n'est à cette position");
            }
        }
        return joueur;
    }
}