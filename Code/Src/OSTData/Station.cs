namespace OSTData {
    /// <summary>
    /// Classe representant une station
    /// </summary>
    public class Station {
        /// <summary> les types que peuvent avoir les stations </summary>
        public enum StationType {
            /// <summary> mine de roche</summary>
            Mine,
            /// <summary> station agricole</summary>
            Agricultural,
            /// <summary> cit�e habit�e </summary>
            City,
            /// <summary> Rafinerie d'uranium </summary>
            FuelRefinery,
            /// <summary> Rafinerie de roche </summary>
            RockRefinery,
            /// <summary> Traitement de dechets </summary>
            Reprocessing,
            /// <summary> champs de glace</summary>
            IceField,
            /// <summary> Chantier naval</summary>
            Shipyard
        }

        /// <summary> Constructeur par type </summary>
        /// <param name="type">le type de la station</param>
        /// <param name="starSystem">Le systeme qui contient cette station</param>
        /// <param name="position">La position de la station dans sons syteme, en AU</param>
        public Station(StationType type, StarSystem starSystem, OSTTools.Vector3D position) {
            Type = type;
            Position = position;
        }

        /// <summary> Le type de cette station </summary>
        public StationType Type { get; private set;}

        /// <summary> La position de la station dans ce systeme, en Unite astronomique</summary>
        public OSTTools.Vector3D Position { get; private set; }
    }
}