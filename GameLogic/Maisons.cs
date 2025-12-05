using System.Collections.Generic;
using System.Linq;

namespace MadnessDelivery.GameLogic;

public class Maisons
{
    private List<Maison> listeMaisons = new();

    private uint nextId = 1; // pratique pour générer des IDs

    public uint GetNextId()
    {
        return nextId++;
    }

    public void AjouterMaison(Maison m)
    {
        listeMaisons.Add(m);
    }

    // 
    public List<Maison> getToutesLesMaisons()
    {
        return listeMaisons;
    }

    // 
    public Maison getMaisonParId(uint id)
    {
        return listeMaisons.FirstOrDefault(m => m.getId() == id);
    }
}