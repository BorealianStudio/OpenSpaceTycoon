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
            /// <summary> citée habitée </summary>
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
        public Station(StationType type) {

        }

        /// <summary> Le type de cette station </summary>
        public StationType Type { get; private set;}
    }
}