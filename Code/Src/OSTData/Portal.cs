using System;
using OSTTools;

namespace OSTData {

    /// <summary>
    /// Un portail entre 2 stations
    /// </summary>
    [Serializable]
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
        /// <param name="type">le type de ce portail</param>
        public Portal(Station s1, Station s2, PortalType type) {
            Station1 = s1;
            Station2 = s2;
            TypePortal = type;
        }

        /// <summary> le type de ce portail </summary>
        [Newtonsoft.Json.JsonProperty]
        public PortalType TypePortal { get; private set; }

        /// <summary> La station a la 1ere extremite de ce portail</summary>
        [Newtonsoft.Json.JsonProperty]
        public Station Station1 { get; private set; }

        /// <summary> La station a l'autre extremite de ce portail</summary>
        [Newtonsoft.Json.JsonProperty]
        public Station Station2 { get; private set; }

        /// <summary>
        /// Donne la position du portail adns la zone autour de la station
        /// </summary>
        /// <param name="station">la station utilise comme reference</param>
        /// <returns>la position en m par rapport a station</returns>
        public Vector3 Position(Station station) {
            Station other = null;
            if (station != Station1 && station != Station2)
                return new Vector3();

            if (Station1 == station)
                other = Station2;
            else
                other = Station1;

            switch (TypePortal) {
                case PortalType.StarToStar: {
                    Vector3 direction = other.System.Position - station.System.Position;
                    return direction.Normalize() * 2000.0;
                }
                case PortalType.StationToStation: {
                    Vector3 direction = other.Position - station.Position;
                    return direction.Normalize() * 2000.0;
                }
            }
            return new Vector3();
        }

        /// <summary>
        /// tester si deux portals sont egaux (meme ID de station aux extremites)
        /// </summary>
        /// <param name="obj">l'autre Portal</param>
        /// <returns>true si les portals sont identiques</returns>
        public override bool Equals(object obj) {
            Portal other = obj as Portal;
            if (null == other)
                return false;

            if (Station1.ID != other.Station1.ID || Station2.ID != other.Station2.ID)
                return false;

            if (TypePortal != other.TypePortal)
                return false;

            return true;
        }

        /// <summary>
        /// custom hash code
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode() {
            return Station1.ID + (Station2.ID << 8);
        }
    }
}