namespace OSTData {

    /// <summary>
    /// Un portail entre 2 stations
    /// </summary>
    public class Portal {

        /// <summary> Les types que peuvent avoir les portails </summary>
        public enum PortalType {
#pragma warning disable CS1591
            StationToStation,
            StarToStar
#pragma warning restore CS1591
        }

        /// <summary> basic constructor </summary>
        /// <param name="s1">1ere station </param>
        /// <param name="s2">2e station</param>
        public Portal(Station s1, Station s2) {
            Station1 = s1;
            Station2 = s2;
        }

        /// <summary> La station a la 1ere extremite de ce portail</summary>
        public Station Station1 { get; private set; }

        /// <summary> La station a l'autre extremite de ce portail</summary>
        public Station Station2 { get; private set; }
    }
}