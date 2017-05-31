using System.Collections.Generic;

namespace OSTData {

    /// <summary>
    /// Un Hangar est une forme particuliere de ResourceHolder qui est dans une station
    /// </summary>
    public class Hangar : ResourceHolder {

        /// <summary>
        /// Constructeur de base
        /// </summary>
        /// <param name="station">La station qui contient ce hangar</param>
        /// <param name="corporation">La corporation a qui appartient ce hangar</param>
        public Hangar(Station station, Corporation corporation) {
            Station = station;
            Corporation = corporation;
        }

        /// <summary> The station this hangar is in </summary>
        public Station Station { get; private set; }

        /// <summary> Corporation owning this hangar </summary>
        public Corporation Corporation { get; private set; }
    }
}