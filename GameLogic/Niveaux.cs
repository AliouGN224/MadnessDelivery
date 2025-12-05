using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace MadnessDelivery.GameLogic;
[XmlRoot("niveaux", Namespace = "http://www.univ-grenoble-alpes.fr/l3miage/delivery")]
[Serializable]
public class Niveaux
{
    private List<Niveau>  niveau;

    [XmlElement("niveau")]
    public List<Niveau> Niveau
    {
        get => niveau;
        set => niveau = value;
    }

    public void AjouterNiveau(Niveau niveau)
    {
        if (niveau != null)
        {
            int i = 0;
            while ((i < this.niveau.Count) && this.niveau[i].Difficulte != niveau.Difficulte)
            {
                i++;
            }

            if (i >= this.niveau.Count)
            {
                this.niveau.Add(niveau);
            }
            else
            {
                Console.WriteLine("Ce niveau existe déjà veillez donc modifier son score");
            }
            
        }
    }
}