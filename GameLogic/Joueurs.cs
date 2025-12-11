using System;
using System.Collections.Generic;
using System.IO;
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
        bool estvrai = false;
        if (this.joueurs != null)
        {
            int i = 0;
            while ((i < this.joueurs.Count) && (this.joueurs[i].JoueurId != joueur.JoueurId))
            {
                i++;
            }

            if (i < this.joueurs.Count)
            {
                Console.WriteLine("Le joueur existe déjà, veillez donc vous connecter avec vos identifiant");
                
            }
            else
            {
                this.joueurs.Add(joueur);
                Console.WriteLine("Le joueur est ajouté avec succées"); 
            }
        }
        else
        {
            this.joueurs.Add(joueur);
            Console.WriteLine("Le joueur est ajouté avec succées"); 
        }
    }

    public string interfaceGraphique()
    {
        String choix;
        Console.WriteLine("__________________________________________________________");
        Console.WriteLine("*           Faites un choix                               *");
        Console.WriteLine("__________________________________________________________");
        Console.WriteLine("*          1. Connectez vous pour jouer                   ");
        Console.WriteLine("*          2. Inscrivez vous pour jouer                   ");
        Console.WriteLine("__________________________________________________________");
        choix = Console.ReadLine();
        return choix;
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
                Console.WriteLine("Aucun joueur n'a ce identifiant");
            }
        }
        return joueur;
    }
    
    public void serialiserJoueurs(string filepath)
    {
        using (var writer = new StreamWriter(filepath))
        {
            var xmlJoueur = new XmlSerializer(typeof(Joueurs));
            xmlJoueur.Serialize(writer, this);
        }
    } 
    
    public void DeserialiserJoueurs(String path)
    {
        using (TextReader reader = new StreamReader(path))
        {
            var xmlJoueurs = new XmlSerializer(typeof(Joueurs));
            var deserialized = (Joueurs)xmlJoueurs.Deserialize(reader);
            this._Joueurs = deserialized._Joueurs;
            
            Console.WriteLine("Déserialisation effectuée avec succées !");

        }
    }
    
}