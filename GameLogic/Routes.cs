using System.Collections.Generic;
using System.Linq;

namespace MadnessDelivery.GameLogic;

public class Routes
{
    private Dictionary<(int,int), Route> routes;

    public Routes()
    {
        routes = new Dictionary<(int, int), Route>();
    }

    // Ajouter une route
    public void AjouterRoute(Route route)
    {
        routes[(route.Col, route.Row)] = route;
    }

    // Récupérer une route à une position (col, row)
    public Route GetRoute(int col, int row)
    {
        routes.TryGetValue((col, row), out Route route);
        return route;
    }

    // Récupérer toutes les routes
    public IEnumerable<Route> GetToutesLesRoutes()
    {
        return routes.Values;
    }
}