using System;
using System.Collections.Generic;

namespace OSTData {

    /// <summary>
    /// Classe representant une station
    /// </summary>
    [Serializable]
    public class Station {

        /// <summary> les types que peuvent avoir les stations </summary>
        public enum StationType {
#pragma warning disable CS1591
            City,
            Mine,
            IceField,
            Agricultural,
            FuelRefinery,
            RockRefinery,
            Reprocessing,
            Shipyard
#pragma warning restore CS1591
        }

        /// <summary> Constructeur par type </summary>
        /// <param name="type">le type de la station</param>
        /// <param name="starSystem">Le systeme qui contient cette station</param>
        /// <param name="position">La position de la station dans sons syteme, en AU</param>
        /// <param name="iID">un identifiant pour cette station</param>
        public Station(StationType type, StarSystem starSystem, OSTTools.Vector3 position, int iID) {
            Type = type;
            Position = position;
            _gates = new List<Portal>();
            Name = "StationName";
            System = starSystem;
            ID = iID;
        }

        /// <summary> Le type de cette station </summary>
        public StationType Type { get; private set; }

        /// <summary> Le systeme solaire qui contient cette station </summary>
        public StarSystem System { get; private set; }

        /// <summary> L'identifiant de cette station </summary>
        public int ID { get; private set; }

        /// <summary> La position de la station dans ce systeme, en Unite astronomique</summary>
        public OSTTools.Vector3 Position { get; private set; }

        /// <summary> le nom de la station </summary>
        public string Name { get; set; }

        /// <summary> Liste des portails reliant cette station </summary>
        [Newtonsoft.Json.JsonIgnore]
        public List<Portal> Gates
        {
            get { return new List<Portal>(_gates); }
        }

        /// <summary> Ajoute un portal aux alentour de cette station </summary>
        /// <param name="portal">le portail a ajouter </param>
        public void AddGate(Portal portal) {
            if (portal.Station1.ID == ID || portal.Station2.ID == ID)
                _gates.Add(portal);
        }

        /// <summary>
        /// La liste des vaisseau dans la station au moment ou on la recupere
        /// cette liste peut changer apres un update
        /// </summary>
        public List<Ship> Ships
        {
            get { return new List<Ship>(_ships); }
            private set { _ships = value; }
        }

        /// <summary> permet de creer un vaisseau dans cette station </summary>
        /// <returns>le vaisseau cree</returns>
        public Ship CreateShip() {
            Ship result = new Ship(System.Universe.Ships.Count + 1);
            System.Universe.Ships.Add(result);
            _ships.Add(result);
            return result;
        }

        /// <summary> Recupere la liste des ressource achetes par cette station </summary>
        public HashSet<ResourceElement.ResourceType> Buyings
        {
            get { return new HashSet<ResourceElement.ResourceType>(_buyingPrices.Keys); }
        }

        /// <summary> ajuster le prix d'une ressource </summary>
        /// <param name="type">le type de ressource a varier le prix</param>
        /// <param name="newPrice">le nouveau prix d'achat</param>
        public void SetBuyingPrice(ResourceElement.ResourceType type, int newPrice) {
            if (!_buyingPrices.ContainsKey(type))
                _buyingPrices.Add(type, newPrice);
            else
                _buyingPrices[type] = newPrice;
        }

        /// <summary> recuperer le prix d'achat actuel pour une ressource dans cette station</summary>
        /// <param name="type">la type a verifier</param>
        /// <returns>le prix d'achat actuel en ICU</returns>
        public int GetBuyingPrice(ResourceElement.ResourceType type) {
            if (_buyingPrices.ContainsKey(type))
                return _buyingPrices[type];
            return 0;
        }

        private List<Ship> _ships = new List<Ship>();
        private List<Portal> _gates = new List<Portal>();
        private Dictionary<ResourceElement.ResourceType, int> _buyingPrices = new Dictionary<ResourceElement.ResourceType, int>();
    }
}