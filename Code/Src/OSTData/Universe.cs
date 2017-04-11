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

            Day = 0;
            Hour = 0;
        }

        /// <summary>
        /// Effectuer une mise a jour d'un tour. c'est faire avancer le temps de 1/5 de jour
        /// </summary>
        public void Update() {
            Hour++;
            if (Hour >= _hourPerDay) {
                Day++;
                Hour -= _hourPerDay;
            }

            foreach (Ship s in Ships) {
                s.Update();
            }
        }

        /// <summary> Recuperer la liste de toutes les stations de l'univers </summary>
        /// <returns>Une collection de reference sur les stations</returns>
        public ICollection<Station> GetStations() {
            return new List<Station>(_stations);
        }

        /// <summary> liste des vaisseaux dans l'univers </summary>
        public ICollection<Ship> Ships { get; set; }

        /// <summary> Les systemes contenus dans cet univers </summary>
        public Dictionary<int, StarSystem> Systems { get; private set; }

        /// <summary> List des portail de cet univers </summary>
        public List<Portal> Portals { get; private set; }

        /// <summary> nombre de jour ecoule depuis le debut du jeu </summary>
        public int Day { get; private set; }

        /// <summary> nombre d'heure ecoule dans le jour en cours</summary>
        public int Hour { get; private set; }

        #region private

        private int _seed = 0;
        private System.Random _random = null;
        private List<Station> _stations = null;
        private int _nbSystemPerMap = 5; // nombre de systeme dans une map
        private int _nbMine = 6;
        private int _nbIceField = 2;
        private int _nbStation = 60;

        private int _hourPerDay = 5;  // nombre d'heure par jour

        /// <summary>
        /// Methode qui genere l'univers alleatoirement a partir du seed stocke
        /// </summary>
        private void BuildUniverse() {
            throw new System.NotImplementedException();
            /*
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
            */
        }

        private void HardCordedBuildUniverse() {
            _stations = new List<Station>();
            Systems = new Dictionary<int, StarSystem>();
            Portals = new List<Portal>();
            Ships = new List<Ship>();

            //creation des systemes
            for (int i = 0; i < _nbSystemPerMap; i++) {
                StarSystem sys = new StarSystem(i, this, GetRandomSystemPosition());
                sys.Name = "System " + (i + 1);
                Systems.Add(i, sys);

                //creation d'une cite
                OSTTools.Vector3D stationPos = new OSTTools.Vector3D();
                Station s = new Station(Station.StationType.City, Systems[i], stationPos, _stations.Count + 1);
                s.Name = "city " + i;
                _stations.Add(s);
                Systems[i].Stations.Add(s);
            }

            //creation des stations mines
            for (int i = 0; i < _nbMine; i++) {
                StarSystem sys = Systems[_random.Next(_nbSystemPerMap)];
                Station s = new Station(Station.StationType.Mine, sys, GetRandomStationPosition(), _stations.Count + 1);
                s.Name = "mine " + i;
                _stations.Add(s);
                sys.Stations.Add(s);
            }

            for (int i = 0; i < _nbIceField; i++) {
                StarSystem sys = Systems[_random.Next(_nbSystemPerMap)];
                Station s = new Station(Station.StationType.IceField, sys, GetRandomStationPosition(), _stations.Count + 1);
                s.Name = "ice field " + i;
                _stations.Add(s);
                sys.Stations.Add(s);
            }

            System.Array allTypes = System.Enum.GetValues(typeof(Station.StationType));

            while (_stations.Count < _nbStation) {
                StarSystem sys = Systems[_random.Next(_nbSystemPerMap)];
                Station.StationType type = (Station.StationType)allTypes.GetValue(_random.Next(allTypes.Length - 3) + 3);
                Station s = new Station(type, sys, GetRandomStationPosition(), _stations.Count + 1);
                s.Name = type.ToString() + " " + (_stations.Count + 1);
                _stations.Add(s);
                sys.Stations.Add(s);
            }

            //creation des links entre systeme
            for (int i = 0; i < _nbSystemPerMap; i++) {
                for (int j = i + 1; j < _nbSystemPerMap; j++) {
                    Station from = Systems[i].Stations[_random.Next(Systems[i].Stations.Count)];
                    Station to = Systems[j].Stations[_random.Next(Systems[j].Stations.Count)];
                    Portal p = new Portal(from, to, Portal.PortalType.StarToStar);
                    from.Gates.Add(p);
                    to.Gates.Add(p);
                    Portals.Add(p);
                }
                //creer les liens internes
                List<Station> connected = new List<Station>();
                foreach (Station s in Systems[i].Stations) {
                    Station closest = null;
                    float bestDist = 0.0f;
                    foreach (Station s2 in connected) {
                        if (s2 != s) {
                            float dist = (s2.Position.X - s.Position.X) * (s2.Position.X - s.Position.X) +
                                         (s2.Position.Y - s.Position.Y) * (s2.Position.Y - s.Position.Y) +
                                         (s2.Position.Z - s.Position.Z) * (s2.Position.Z - s.Position.Z);
                            if (closest == null || dist < bestDist) {
                                bestDist = dist;
                                closest = s2;
                            }
                        }
                    }
                    if (closest != null) {
                        Portal p = new Portal(s, closest, Portal.PortalType.StationToStation);
                        s.Gates.Add(p);
                        closest.Gates.Add(p);
                        Portals.Add(p);
                    }
                    connected.Add(s);
                }
            }
        }

        private OSTTools.Vector3D GetRandomStationPosition() {
            OSTTools.Vector3D result = new OSTTools.Vector3D();
            result.X = (float)(_random.NextDouble() * 100.0) - 50.0f;
            result.Y = 0.0f;
            result.Z = (float)(_random.NextDouble() * 100.0) - 50.0f;
            return result;
        }

        private OSTTools.Vector3D GetRandomSystemPosition() {
            OSTTools.Vector3D result = new OSTTools.Vector3D();
            result.X = (float)(_random.NextDouble() * 250.0) - 125.0f;
            result.Y = 0.0f;
            result.Z = (float)(_random.NextDouble() * 250.0) - 125.0f;
            return result;
        }

        #endregion private
    }
}