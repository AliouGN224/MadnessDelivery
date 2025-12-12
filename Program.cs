using System.Collections.Generic;
using MadnessDelivery.GameLogic;

Joueurs j = new Joueurs();
j.interfaceGraphique();

using var game = new MadnessDelivery.Game1();
game.Run();

