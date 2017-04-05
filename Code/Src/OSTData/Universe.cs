using System.Collections.Generic;

namespace OSTData {

    /// <summary>
    /// Cette classe represente l'univers du jeu. Tout dans le jeu survient dans un univers
    /// L'univer est capable de se serialiser pour etre sauve ou construit a partir d'un fichier
    /// </summary>
    public class Universe {

        /// <summary> basic constructor </summary>
        /// <param name="seed"></param>
        public Universe(int seed) {
            _Seed = seed;
            _Random = new System.Random(seed);
            BuildUniverse();
        }

        /// <summary> Constructeur depuis une savegame</summary>
        /// <param name="pathToSaveFile">le chemin de la savegame</param>
        public Universe(string pathToSaveFile) {
        }

        /// <summary>
        /// Effectuer une mise a jour d'un tour. c'est faire avancer le temps de 1/5 de jour
        /// </summary>
        public void Update() {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Sauve l'etat actuel de la partie dans une fichier
        /// </summary>
        /// <param name="pathToSaveFile">Chemin du ficher de sauvegarde</param>
        public void Save(string pathToSaveFile) {
            throw new System.NotImplementedException();
        }

        /// <summary> Recuperer la liste de toutes les stations de l'univers </summary>
        /// <returns>Une collection de reference sur les stations</returns>
        public ICollection<Station> GetStations() {
            return new List<Station>(_Stations);
        }

        #region private

        private int _Seed = 0;
        private System.Random _Random = null;
        private List<Station> _Stations = null;
        private int _NbSystemPerMap = 5; // nombre de systeme dans une map
        private int _NbMine = 6;
        private int _NbIceField = 2;
        private int _NbStation = 60;

        /// <summary>
        /// Methode qui genere l'univers alleatoirement a partir du seed stocke
        /// </summary>
        private void BuildUniverse() {
            _Stations = new List<Station>();

            //creation des systemes
            Dictionary<int, StarSystem> systemes = new Dictionary<int, StarSystem>();
            for (int i = 0; i < _NbSystemPerMap; i++) {
                OSTTools.Vector3D pos = new OSTTools.Vector3D();
                systemes.Add(i, new StarSystem(i, pos));

                //creation d'une cite
                OSTTools.Vector3D stationPos = new OSTTools.Vector3D();
                Station s = new Station(Station.StationType.City, systemes[i], stationPos);
                _Stations.Add(s);
            }

            //creation des stations mines
            for (int i = 0; i < _NbMine; i++) {
                Station s = new Station(Station.StationType.Mine, systemes[_Random.Next(_NbSystemPerMap)], GetRandomStationPosition());
                _Stations.Add(s);
            }
            for (int i = 0; i < _NbIceField; i++) {
                Station s = new Station(Station.StationType.IceField, systemes[_Random.Next(_NbSystemPerMap)], GetRandomStationPosition());
                _Stations.Add(s);
            }

            while (_Stations.Count < _NbStation) {
                Station.StationType type = Station.StationType.Agricultural;
                Station s = new Station(type, systemes[_Random.Next(_NbSystemPerMap)], GetRandomStationPosition());
                _Stations.Add(s);
            }
        }

        private OSTTools.Vector3D GetRandomStationPosition() {
            OSTTools.Vector3D result = new OSTTools.Vector3D();
            result.X = (float)(_Random.NextDouble() * 100.0);
            result.Y = 0.0f;
            result.Z = (float)(_Random.NextDouble() * 100.0);
            return result;
        }

        #endregion private
    }
}