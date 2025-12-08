using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace MadnessDelivery.GameLogic;
[XmlRoot("routes", Namespace = "http://www.univ-grenoble-alpes.fr/l3miage/delivery")]
[Serializable]
public class Routes
{
    private List<Route> routes;
    
    [XmlElement("route")]
    public List<Route> ListeRoute 
    { 
        get => routes; 
        set => routes = value; 
    }
    

    // Ajouter une route
    public void AjouterRoute(Route route)
    {
        routes.Add(route);
    }

    // Récupérer une route à une position (col, row)
    public Route GetRoute(int col, int row)
    {
        Route route = null;
        if (this.routes != null)
        {
            int i = 0;
            while ((i < this.routes.Count) && ((this.routes[i].Position._X != col) || (this.routes[i].Position._Y != row)))
            {
                i++;
            }

            if (i < this.routes.Count)
            {
                route = this.routes[i];
            }
            else
            {
                Console.WriteLine("Aucune route n'est à cette position");
            }
        }
        return route;
    }

    // Récupérer toutes les routes
   /* public IEnumerable<Route> GetToutesLesRoutes()
    {
        return routes.Values;
    }*/
}