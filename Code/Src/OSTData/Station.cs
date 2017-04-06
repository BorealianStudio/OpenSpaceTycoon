using System.Collections.Generic;

namespace OSTData {

    /// <summary>
    /// Classe representant une station
    /// </summary>
    public class Station {

        /// <summary> les types que peuvent avoir les stations </summary>
        public enum StationType {
#pragma warning disable CS1591
            Mine,
            Agricultural,
            City,
            FuelRefinery,
            RockRefinery,
            Reprocessing,
            IceField,
            Shipyard
#pragma warning restore CS1591
        }

        /// <summary> Constructeur par type </summary>
        /// <param name="type">le type de la station</param>
        /// <param name="starSystem">Le systeme qui contient cette station</param>
        /// <param name="position">La position de la station dans sons syteme, en AU</param>
        public Station(StationType type, StarSystem starSystem, OSTTools.Vector3D position) {
            Type = type;
            Position = position;
            Gates = new List<Portal>();
        }

        /// <summary> Le type de cette station </summary>
        public StationType Type { get; private set; }

        /// <summary> La position de la station dans ce systeme, en Unite astronomique</summary>
        public OSTTools.Vector3D Position { get; private set; }

        /// <summary> Liste des portails reliant cette station </summary>
        public List<Portal> Gates { get; set; }
    }
}