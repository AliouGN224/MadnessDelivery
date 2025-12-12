using System.Collections.Generic;
using MadnessDelivery.GameLogic;

Joueurs j = new Joueurs();
j.interfaceGraphique();

Score score =  new Score{ValeurScore = 3};
Score score1 =  new Score{ValeurScore = 4};
Score score2 =  new Score{ValeurScore = 5};
Score score3 =  new Score{ValeurScore = 2};
Scores scores = new Scores{_Score = new List<Score>{score, score1}};
Scores scores1 = new Scores{_Score = new List<Score>{score, score3,score1}};

Scores scores2 = new Scores{_Score = new List<Score>{score2, score1}};
Scores scores3 = new Scores{_Score = new List<Score>{score, score3}};



Niveau niveau = new Niveau{Difficulte = Level.FACILE, _Scores = scores};
Niveau niveau1 = new Niveau{Difficulte = Level.MOYEN, _Scores = scores1};

Niveau niveau2 = new Niveau{Difficulte = Level.DIFFICILE, _Scores = scores2};
Niveau niveau3 = new Niveau{Difficulte = Level.MOYEN, _Scores = scores3};



Niveaux niveaux = new Niveaux{Niveau = new List<Niveau>{niveau,niveau1}};
Niveaux niveaux1 = new Niveaux { Niveau = new List<Niveau> { niveau2, niveau3 } };

Joueur joueur1 = new Joueur { JoueurId = 111, Nom = "diallo", Prenom = "python", _Niveaux = niveaux };
Joueur joueur2 = new Joueur{JoueurId = 222, Nom = "diallo", Prenom = "scorpion", _Niveaux = niveaux1 };

Joueurs listeJoueur = new Joueurs{_Joueurs = new List<Joueur>{joueur1,joueur2}};

listeJoueur.serialiserJoueurs("../../../data/xml/joueurs.xml");

using var game = new MadnessDelivery.Game1();
game.Run();

