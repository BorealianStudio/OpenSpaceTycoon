using System.Collections.Generic;

namespace OSTData {

    /// <summary>
    /// Un Hangar est une forme particuliere de ResourceHolder qui est dans un Ship
    /// </summary>
    public class ShipCargo : ResourceHolder {

        /// <summary>
        /// Constructeur de base
        /// </summary>
        /// <param name="ship">Le vaisseau qui contient ce hangar</param>
        public ShipCargo(Ship ship) {
            Ship = ship;
        }

        /// <summary> The Ship this hangar is in </summary>
        public Ship Ship { get; private set; }
    }
}