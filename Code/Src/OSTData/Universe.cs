using System;
using System.Collections.Generic;

namespace OSTData {

    /// <summary>
    /// Cette classe represente l'univers du jeu. Tout dans le jeu survient dans un univers
    /// L'univer est capable de se serialiser pour etre sauve ou construit a partir d'un fichier
    /// </summary>
    [Serializable]
    public class Universe {

        #region Events

        /// <summary>
        /// type de callback sans parametres
        /// </summary>
        public delegate void noParamAction();

        /// <summary>
        /// Event leve quand la fin d'une journée survient
        /// </summary>
        public event noParamAction onDayEnd = delegate { };

        #endregion Events

        private Universe() {
        }

        /// <summary> basic constructor </summary>
        /// <param name="seed"></param>
        public Universe(int seed) {
            _random = new OSTTools.Random(seed);

            Build();

            Day = 0;
            Hour = 0;
        }

        /// <summary>
        /// demande a l'univers de se creer les entitées necessaires.
        /// ne pas faire plus d'une fois, et pas besoin apres une deserialization
        /// </summary>
        public void Build() {
            HardCordedBuildUniverse();

            foreach (Station s in _stations.Values) {
                s.InitProduct();
            }
        }

        /// <summary>
        /// Effectuer une mise a jour d'un tour. c'est faire avancer le temps de 1/5 de jour
        /// </summary>
        public void Update() {
            Hour++;
            if (Hour >= _hourPerDay) {
                Day++;
                Hour -= _hourPerDay;
                EndDay(Day);
            }

            foreach (Ship s in Ships) {
                s.Update();
            }
        }

        /// <summary> Recuperer la liste de toutes les stations de l'univers </summary>
        /// <returns>Une collection de reference sur les stations</returns>
        public ICollection<Station> GetStations() {
            return new List<Station>(_stations.Values);
        }

        /// <summary>
        /// Le générateur aléatoire de cet univers.
        /// </summary>
        public OSTTools.Random Random {
            get { return _random; }
        }

        /// <summary> Recuperer une station par son ID</summary>
        /// <param name="ID">l'ID de la station a recuperer</param>
        /// <returns>la station ou null si aucune ne correspond</returns>
        public Station GetStation(int ID) {
            if (_stations.ContainsKey(ID))
                return _stations[ID];
            return null;
        }

        /// <summary> Les systemes contenus dans cet univers </summary>        ///
        public Dictionary<int, StarSystem> Systems { get; set; }

        /// <summary> liste des vaisseaux dans l'univers </summary>
        public List<Ship> Ships { get; set; }

        /// <summary> List des portail de cet univers </summary>
        public List<Portal> Portals { get; set; }

        /// <summary> nombre de jour ecoule depuis le debut du jeu </summary>
        public int Day { get; private set; }

        /// <summary> nombre d'heure ecoule dans le jour en cours</summary>
        public int Hour { get; private set; }

        /// <summary>
        /// recuperer une corporation par son ID
        /// </summary>
        /// <param name="corpID">l'identifiant de la corporation</param>
        /// <returns>la corporation si elle existe, null sinon</returns>
        public Corporation GetCorporation(int corpID) {
            if (_corporations.ContainsKey(corpID))
                return _corporations[corpID];
            return null;
        }

        /// <summary>
        /// permet de creer une nouvelle corporation dans l'univers
        /// </summary>
        /// <param name="ID">l'identifiant unique de la corp</param>
        /// <returns>la corporation creer si l'id etait libre, null sinon</returns>
        public Corporation CreateCorp(int ID) {
            if (_corporations.ContainsKey(ID))
                return null;

            Corporation corp = new Corporation(ID);
            _corporations.Add(ID, corp);
            return corp;
        }

        /// <summary>
        /// Compare si deux object sont identique.
        /// </summary>
        /// <param name="obj">l'autre objet a comparer</param>
        /// <returns></returns>
        public override bool Equals(object obj) {
            Universe other = obj as Universe;
            if (null == other)
                return false;

            //date
            if (other.Day != Day || other.Hour != Hour)
                return false;

            //tester la map (Systems + stations + portals)
            if (Systems.Count != other.Systems.Count)
                return false;
            for (int i = 0; i < Systems.Count; i++) {
                if (!Systems[i].Equals(other.Systems[i]))
                    return false;
            }

            if (_stations.Count != other._stations.Count)
                return false;
            foreach (int i in _stations.Keys) {
                if (!_stations[i].Equals(other._stations[i]))
                    return false;
            }

            if (Portals.Count != other.Portals.Count)
                return false;
            for (int i = 0; i < Portals.Count; i++) {
                if (!Portals[i].Equals(other.Portals[i]))
                    return false;
            }

            if (Ships.Count != other.Ships.Count)
                return false;
            for (int i = 0; i < Ships.Count; i++) {
                if (!Ships[i].Equals(other.Ships[i]))
                    return false;
            }
            return true;
        }

        /// <summary>
        /// override du GetHashCode pour eviter le warning
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode() {
            return base.GetHashCode();
        }

        #region private

        [Newtonsoft.Json.JsonProperty]
        private OSTTools.Random _random = null;

        [Newtonsoft.Json.JsonProperty]
        private Dictionary<int, Station> _stations = null;

        [Newtonsoft.Json.JsonProperty]
        private Dictionary<int, Corporation> _corporations = null;

        private int _nbSystemPerMap = 5; // nombre de systeme dans une map
        private int _nbMine = 6;
        private int _nbIceField = 2;
        private int _nbStation = 60;

        private int _hourPerDay = 5;  // nombre d'heure par jour

        /// <summary>
        /// Methode qui genere l'univers alleatoirement a partir du seed stocke
        /// </summary>
        private void BuildUniverse() {
            throw new NotImplementedException();
        }

        private void HardCordedBuildUniverse() {
            _stations = new Dictionary<int, Station>();
            _corporations = new Dictionary<int, Corporation>();
            Systems = new Dictionary<int, StarSystem>();
            Portals = new List<Portal>();
            Ships = new List<Ship>();

            //creaction de la corp NPC

            Corporation npcCorp = new Corporation(-1);
            _corporations.Add(-1, npcCorp);

            //creation des systemes
            for (int i = 0; i < _nbSystemPerMap; i++) {
                StarSystem sys = new StarSystem(i, this, GetRandomSystemPosition());
                sys.Name = "System " + (i + 1);
                Systems.Add(i, sys);

                //creation d'une cite
                OSTTools.Vector3 stationPos = new OSTTools.Vector3(1.0, 1.0, 1.0);
                Station s = new Station(Station.StationType.City, Systems[i], stationPos, _stations.Count + 1);
                s.Name = "city " + i;
                _stations.Add(s.ID, s);
                Systems[i].Stations.Add(s);
            }

            //creation des stations mines
            for (int i = 0; i < _nbMine; i++) {
                StarSystem sys = Systems[_random.Next(_nbSystemPerMap)];
                Station s = new Station(Station.StationType.Mine, sys, GetRandomStationPosition(), _stations.Count + 1);
                s.Name = "mine " + i;
                _stations.Add(s.ID, s);
                sys.Stations.Add(s);
            }

            for (int i = 0; i < _nbIceField; i++) {
                StarSystem sys = Systems[_random.Next(_nbSystemPerMap)];
                Station s = new Station(Station.StationType.IceField, sys, GetRandomStationPosition(), _stations.Count + 1);
                s.Name = "ice field " + i;
                _stations.Add(s.ID, s);
                sys.Stations.Add(s);
            }

            Array allTypes = Enum.GetValues(typeof(Station.StationType));

            while (_stations.Count < _nbStation) {
                StarSystem sys = Systems[_random.Next(_nbSystemPerMap)];
                Station.StationType type = (Station.StationType)allTypes.GetValue(_random.Next(allTypes.Length - 3) + 3);
                Station s = new Station(type, sys, GetRandomStationPosition(), _stations.Count + 1);
                s.Name = type.ToString() + " " + (_stations.Count + 1);
                _stations.Add(s.ID, s);
                sys.Stations.Add(s);
            }

            //creation des links entre systeme
            for (int i = 0; i < _nbSystemPerMap; i++) {
                for (int j = i + 1; j < _nbSystemPerMap; j++) {
                    Station from = Systems[i].Stations[_random.Next(Systems[i].Stations.Count)];
                    Station to = Systems[j].Stations[_random.Next(Systems[j].Stations.Count)];
                    Portal p = new Portal(from, to, Portal.PortalType.StarToStar);
                    from.AddGate(p);
                    to.AddGate(p);
                    Portals.Add(p);
                }
                //creer les liens internes
                List<Station> connected = new List<Station>();
                foreach (Station s in Systems[i].Stations) {
                    Station closest = null;
                    double bestDist = 0.0;
                    foreach (Station s2 in connected) {
                        if (s2 != s) {
                            double dist = (s2.Position.X - s.Position.X) * (s2.Position.X - s.Position.X) +
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
                        s.AddGate(p);
                        closest.AddGate(p);
                        Portals.Add(p);
                    }
                    connected.Add(s);
                }
            }
        }

        private OSTTools.Vector3 GetRandomStationPosition() {
            OSTTools.Vector3 result = new OSTTools.Vector3((_random.NextDouble() * 100.0) - 50.0,
                                                           0.0,
                                                           (_random.NextDouble() * 100.0) - 50.0);
            return result;
        }

        private OSTTools.Vector3 GetRandomSystemPosition() {
            OSTTools.Vector3 result = new OSTTools.Vector3((_random.NextDouble() * 250.0) - 125.0,
                                                           0.0,
                                                           (_random.NextDouble() * 250.0) - 125.0f);
            return result;
        }

        private void EndDay(int timestamp) {
            foreach (Station s in _stations.Values) {
                s.EndDays(timestamp);
            }
            onDayEnd();
        }

        #endregion private
    }
}