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
                Console.WriteLine("Le joueur existe déjà, veillez essayez un autre joueur");
                
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

    public Joueur interfaceGraphique()
    {
        Joueur joueurfinal = null;
        String choix;
        Console.WriteLine("__________________________________________________________");
        Console.WriteLine("*           Faites un choix                               *");
        Console.WriteLine("__________________________________________________________");
        Console.WriteLine("*          1. Connectez vous pour jouer                   ");
        Console.WriteLine("*          2. Inscrivez vous pour jouer                   ");
        Console.WriteLine("__________________________________________________________");
        Console.WriteLine("saisir ici : ");
        choix = Console.ReadLine();
        uint nb = uint.Parse(choix);


        if (nb == 1)
        {
            string id;
            Console.WriteLine("*___________Donnez votre identifiant : ");
            id = Console.ReadLine();
            uint identifiant = uint.Parse(id);
            this.DeserialiserJoueurs("../../../data/xml/joueurs.xml");
            Joueur joueur = this.GetJoueur(identifiant);
            
            if (joueur == null)
            {
              Console.WriteLine("Ce identifiant ne correspond à aucun joueur");
              interfaceGraphique();
            }
            else
            {
                Console.WriteLine("___Merci de vous avoir identifier");
                joueurfinal = joueur;
            }
            
        }else if (nb == 2)
        {
            joueurfinal = inscription();
        }
        else
        {
            Console.WriteLine("Attention mauvais choix (1 ou 2) !");
            interfaceGraphique();
        } 
        return joueurfinal;
    }

    private Joueur inscription()
    {
        Joueur joueurfinal = null;
        while (joueurfinal == null)
        {
            Console.WriteLine("______________Votre identifiant (en entier positif) : ");
            string id = Console.ReadLine();
            Console.WriteLine("______________Votre Nom : ");
            string nom = Console.ReadLine();
            Console.WriteLine("______________Votre Prenom :");
            string prenom = Console.ReadLine();
            
            Joueur joueur = new Joueur{JoueurId = uint.Parse(id),Nom = nom,Prenom = prenom};
            this.DeserialiserJoueurs("../../../data/xml/joueurs.xml");
            int i = 0;
            while ((i < this.joueurs.Count) && (this.joueurs[i].JoueurId != joueur.JoueurId))
            {
                i++;
            }

            if (i < this.joueurs.Count)
            {
                Console.WriteLine("Le joueur existe déjà, essayer un autre identifiant");
                
            }
            else
            {
                Score score =  new Score{ValeurScore = 2};
                Scores scores = new Scores{_Score = new List<Score>{score}};
                
                Niveau niveau = new Niveau{Difficulte = Level.FACILE, _Scores = scores};
                Niveau niveau1 = new Niveau{Difficulte = Level.MOYEN, _Scores = scores};
                Niveau niveau2 = new Niveau{Difficulte = Level.DIFFICILE, _Scores = scores};
                
                Niveaux niveaux = new Niveaux{Niveau = new List<Niveau>{niveau,niveau1,niveau2}};
                
                Joueur joueur2 = new Joueur{JoueurId = joueur.JoueurId,Nom = joueur.Nom,Prenom = joueur.Prenom, _Niveaux = niveaux};
                
                this.AddJoueur(joueur);
                this.serialiserJoueurs("../../../data/xml/joueurs.xml");
                Console.WriteLine("Fichier écrit ici : " + Path.GetFullPath("../../../data/xml/joueurs.xml"));

                joueurfinal = joueur;
                Console.WriteLine("Enregistrement réussi avec succés"); 
            }
        }
        
        return joueurfinal;
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