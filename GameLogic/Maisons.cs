using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace MadnessDelivery.GameLogic;
[XmlRoot("maisons",Namespace="http://www.univ-grenoble-alpes.fr/l3miage/delivery")]
[Serializable]
public class Maisons
{
    private List<Maison> listeMaisons = new();

    [XmlElement("maison")]
    public List<Maison> _ListeMaisons
    {
        get => listeMaisons;
        set => listeMaisons = value;
    }

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
   /* public Maison getMaisonParId(uint id)
    {
        return listeMaisons.FirstOrDefault(m => m.getId() == id);
    }*/
}