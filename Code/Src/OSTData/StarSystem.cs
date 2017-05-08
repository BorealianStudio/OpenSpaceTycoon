using System;
using System.Collections.Generic;

namespace OSTData {

    /// <summary>
    /// Classe representant un systeme solaire. Surtout utilisé pour gérer les positions
    /// </summary>
    [Serializable]
    public class StarSystem {

        /// <summary> Constructeur par type </summary>
        public StarSystem(int id, Universe universe, OSTTools.Vector3 position) {
            ID = id;
            Position = position;
            Stations = new List<Station>();
            Universe = universe;
        }

        /// <summary>L'id de ce systeme, doit être unique dans son univers </summary>
        public int ID { get; private set; }

        /// <summary> l'univers dans lequel se situe ce systeme </summary>
        public Universe Universe { get; private set; }

        /// <summary>Les stations contenue dans ce systeme </summary>
        public List<Station> Stations { get; set; }

        /// <summary> The name of this system </summary>
        public string Name { get; set; }

        /// <summary>La position de ce systeme par rapport au centre de l'univers en Annees lumieres</summary>
        public OSTTools.Vector3 Position { get; private set; }
    }
}