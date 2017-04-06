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
            _seed = seed;
            _random = new System.Random(seed);
            HardCordedBuildUniverse();
        }

        /// <summary>
        /// Effectuer une mise a jour d'un tour. c'est faire avancer le temps de 1/5 de jour
        /// </summary>
        public void Update() {
            throw new System.NotImplementedException();
        }

        /// <summary> Recuperer la liste de toutes les stations de l'univers </summary>
        /// <returns>Une collection de reference sur les stations</returns>
        public ICollection<Station> GetStations() {
            return new List<Station>(_stations);
        }

        #region private

        private int _seed = 0;
        private System.Random _random = null;
        private List<Station> _stations = null;
        private Dictionary<int, StarSystem> _systems = null;
        private List<Portal> _portals = null;
        private int _nbSystemPerMap = 5; // nombre de systeme dans une map
        private int _nbMine = 6;
        private int _nbIceField = 2;
        private int _nbStation = 60;

        /// <summary>
        /// Methode qui genere l'univers alleatoirement a partir du seed stocke
        /// </summary>
        private void BuildUniverse() {
            _stations = new List<Station>();

            //creation des systemes
            Dictionary<int, StarSystem> systemes = new Dictionary<int, StarSystem>();
            for (int i = 0; i < _nbSystemPerMap; i++) {
                OSTTools.Vector3D pos = new OSTTools.Vector3D();
                systemes.Add(i, new StarSystem(i, pos));

                //creation d'une cite
                OSTTools.Vector3D stationPos = new OSTTools.Vector3D();
                Station s = new Station(Station.StationType.City, systemes[i], stationPos);
                _stations.Add(s);
            }

            //creation des stations mines
            for (int i = 0; i < _nbMine; i++) {
                Station s = new Station(Station.StationType.Mine, systemes[_random.Next(_nbSystemPerMap)], GetRandomStationPosition());
                _stations.Add(s);
            }
            for (int i = 0; i < _nbIceField; i++) {
                Station s = new Station(Station.StationType.IceField, systemes[_random.Next(_nbSystemPerMap)], GetRandomStationPosition());
                _stations.Add(s);
            }

            while (_stations.Count < _nbStation) {
                Station.StationType type = Station.StationType.Agricultural;
                Station s = new Station(type, systemes[_random.Next(_nbSystemPerMap)], GetRandomStationPosition());
                _stations.Add(s);
            }
        }

        private void HardCordedBuildUniverse() {
            _stations = new List<Station>();
            _systems = new Dictionary<int, StarSystem>();
            _portals = new List<Portal>();

            //creation des systemes
            for (int i = 0; i < _nbSystemPerMap; i++) {
                OSTTools.Vector3D pos = new OSTTools.Vector3D();
                StarSystem sys = new StarSystem(i, pos);
                sys.Name = "System " + (i + 1);
                _systems.Add(i, sys);

                //creation d'une cite
                OSTTools.Vector3D stationPos = new OSTTools.Vector3D();
                Station s = new Station(Station.StationType.City, _systems[i], stationPos);
                _stations.Add(s);
                _systems[i].Stations.Add(s);
            }

            //creation des stations mines
            for (int i = 0; i < _nbMine; i++) {
                StarSystem sys = _systems[_random.Next(_nbSystemPerMap)];
                Station s = new Station(Station.StationType.Mine, sys, GetRandomStationPosition());
                _stations.Add(s);
                sys.Stations.Add(s);
            }

            for (int i = 0; i < _nbIceField; i++) {
                StarSystem sys = _systems[_random.Next(_nbSystemPerMap)];
                Station s = new Station(Station.StationType.IceField, sys, GetRandomStationPosition());
                _stations.Add(s);
                sys.Stations.Add(s);
            }

            while (_stations.Count < _nbStation) {
                StarSystem sys = _systems[_random.Next(_nbSystemPerMap)];
                Station.StationType type = Station.StationType.Agricultural;
                Station s = new Station(type, sys, GetRandomStationPosition());
                _stations.Add(s);
                sys.Stations.Add(s);
            }

            //creation des links entre systeme
            for (int i = 0; i < _nbSystemPerMap; i++) {
                for (int j = i + 1; j < _nbSystemPerMap; j++) {
                    Station from = _systems[i].Stations[_random.Next(_systems[i].Stations.Count)];
                    Station to = _systems[j].Stations[_random.Next(_systems[j].Stations.Count)];
                    Portal p = new Portal(from, to);
                    from.Gates.Add(p);
                    to.Gates.Add(p);
                    _portals.Add(p);
                }
            }
        }

        private OSTTools.Vector3D GetRandomStationPosition() {
            OSTTools.Vector3D result = new OSTTools.Vector3D();
            result.X = (float)(_random.NextDouble() * 100.0);
            result.Y = 0.0f;
            result.Z = (float)(_random.NextDouble() * 100.0);
            return result;
        }

        #endregion private
    }
}