using System;
using System.Collections.Generic;

namespace OSTData {

    /// <summary>
    /// cette classe represente une destination pour un vaisseau. Elle contient egalement les actions que le vaisseau
    /// doit effectuer une fois arrive a la station cible
    /// </summary>
    [Serializable]
    public class ShipDestination {

        /// <summary> les details d'un chargement ou dechargement </summary>
        public struct LoadData {

            /// <summary>
            /// Constructeur avec valeurs
            /// </summary>
            /// <param name="iType"></param>
            /// <param name="iQte"></param>
            public LoadData(ResourceElement.ResourceType iType, int iQte) { type = iType; qte = iQte; }

            /// <summary> Le type de ressources </summary>
            public ResourceElement.ResourceType type;

            /// <summary> La quantite cible </summary>
            public int qte;
        }

        /// <summary>
        /// constructeur par parametre
        /// </summary>
        /// <param name="ship"></param>
        /// <param name="destination"></param>
        public ShipDestination(Ship ship, Station destination) {
            Destination = destination;
            Ship = ship;
            Done = true;
            UnloadDone = false;
        }

        /// <summary>
        /// update cette destination
        /// </summary>
        public void Update() {
            Ship.CurrentStation = Destination;

            if (_travelDone < 1.0f) {
                _travelDone = Math.Min(_travelDone + 0.1f, 1.0f);    //todo magicnumber
                if (_travelDone < 1.0f) {
                    Ship.CurrentStation = null;
                }
                return;
            }

            int m3toMove = 10;//todo magicnumber

            //dechargement
            if (!UnloadDone) {
                int qteUnloaded = Unload(m3toMove);
                m3toMove -= qteUnloaded;
                if (0 == qteUnloaded) {
                    UnloadDone = true;
                }
            }

            //chargement
            if (m3toMove > 0)
                m3toMove -= Load(m3toMove);

            if (m3toMove == 10) { // si on a rien chargé, on passe a l'etape suivante
                Done = true;
                Ship.NextDestination();
            }
        }

        /// <summary>
        /// Station de destination
        /// </summary>
        public Station Destination { get; private set; }

        /// <summary>
        /// Le vaisseau a qui appartient cette destination
        /// </summary>
        public Ship Ship { get; private set; }

        /// <summary> permet de savoir si cette tache est terminee </summary>
        public bool Done { get; private set; }

        /// <summary> Savoir si la phase de dechargement cette destination est faite </summary>
        public bool UnloadDone { get; private set; }

        /// <summary>
        /// methode a appeler quand on change de destination, sur la nouvelle destination
        /// </summary>
        public void Start() {
            _travelDone = 0.0f;
            _loaded.Clear();
            _unloaded.Clear();
            UnloadDone = false;

            if (Ship.CurrentStation.ID == Destination.ID) {
                _travelDone = 1.0f;
            } else {
                Done = false;
            }
        }

        /// <summary>
        /// Recuperer une chaine indiquant l'etat de cette "tache"
        /// </summary>
        /// <returns></returns>
        public string GetState() {
            if (_travelDone < 1.0f) {
                return "Moving to " + Destination.Name + " " + (_travelDone * 100.0f).ToString("N0") + "%";
            }
            if (!UnloadDone)
                return "Unloading";

            return "Loading";
        }

        /// <summary>
        /// indique au vaisseau qu'une fois a cette destination, charger une certaine qte d'une certaine ressource
        /// </summary>
        /// <param name="type">le type de ressource</param>
        /// <param name="qte">la quantite a charger</param>
        public void AddLoad(ResourceElement.ResourceType type, int qte) {
            LoadData l = new LoadData(type, qte);
            _loads.Add(l);
        }

        /// <summary>
        /// indique au vaisseau qu'une fois a cette destination, il devra decharger une certaine quantite de ressource
        /// </summary>
        /// <param name="type">le type de ressource</param>
        /// <param name="qte">la qte a decharger</param>
        public void AddUnload(ResourceElement.ResourceType type, int qte) {
            LoadData l = new LoadData(type, qte);
            _unloads.Add(l);
        }

        /// <summary>
        /// recupere la list ordonnes des chargement pour cette destination
        /// </summary>
        /// <returns></returns>
        public List<LoadData> GetLoads() {
            return new List<LoadData>(_loads);
        }

        /// <summary>
        /// recupere la liste ordonne des dechargements demandes pour cette destination
        /// </summary>
        /// <returns></returns>
        public List<LoadData> GetUnloads() {
            return new List<LoadData>(_unloads);
        }

        private List<LoadData> _loads = new List<LoadData>();
        private List<LoadData> _unloads = new List<LoadData>();
        private Dictionary<ResourceElement.ResourceType, int> _loaded = new Dictionary<ResourceElement.ResourceType, int>();
        private Dictionary<ResourceElement.ResourceType, int> _unloaded = new Dictionary<ResourceElement.ResourceType, int>();
        private float _travelDone = 0.0f;

        /// <summary>
        /// le vaisseau decharge sa cargaison avant de pouvoir en charger.
        /// </summary>
        /// <param name="possibleUnload">nombre de m3 qu'on peut decharger cette fois</param>
        /// <returns>le nombre de m3 decharge</returns>
        private int Unload(int possibleUnload) {
            int result = 0;
            Station station = Ship.CurrentStation;
            if (null == station)
                return result;

            int qteUnloaded = 0;

            Hangar myHangarInStation = station.GetHangar(Ship.Owner.ID);
            if (null != myHangarInStation) {
                foreach (LoadData l in _unloads) {
                    Hangar targetHangar = myHangarInStation;
                    bool buying = false;
                    if (Ship.CurrentStation.Buyings.Contains(l.type)) {
                        targetHangar = station.GetHangar(-1);
                        buying = true;
                    }

                    if (!_unloaded.ContainsKey(l.type)) {
                        _unloaded.Add(l.type, 0);
                    }
                    int present = Ship.Cargo.GetResourceQte(l.type);
                    int toLoad = Math.Min(present, l.qte - _unloaded[l.type]);
                    toLoad = Math.Min(toLoad, possibleUnload);

                    if (toLoad > 0) {
                        targetHangar.Add(Ship.Cargo.GetStack(l.type, toLoad));
                        if (buying) {
                            int total = toLoad * station.GetBuyingPrice(l.type);
                            Ship.Owner.AddICU(total, "selling stuff");
                        }
                        qteUnloaded += toLoad;
                        _unloaded[l.type] += toLoad;
                        possibleUnload -= toLoad;
                    }
                }
            }
            return qteUnloaded;
        }

        /// <summary>
        /// le vaisseau est en train de se charger dans la station. Il partira quand il aura fini de charger ce qu'il doit
        /// ou quand il sera plein.
        /// <param name="possibleLoad">nombre de m3 qu'on peut charger cette fois</param>
        /// </summary>
        /// <returns>le nombre de m3 charge</returns>
        private int Load(int possibleLoad) {
            int qteLoaded = 0;
            Station station = Ship.CurrentStation;
            if (null == station)
                return qteLoaded;

            Hangar myHangarInStation = station.GetHangar(Ship.Owner.ID);
            if (null != myHangarInStation) {
                foreach (LoadData l in _loads) {
                    if (!_loaded.ContainsKey(l.type)) {
                        _loaded.Add(l.type, 0);
                    }
                    int present = myHangarInStation.GetResourceQte(l.type);
                    int toLoad = Math.Min(present, l.qte - _loaded[l.type]);
                    toLoad = Math.Min(toLoad, possibleLoad);

                    Destination.InformLoading(Ship, l.type);
                    if (toLoad > 0) {
                        Ship.Cargo.Add(myHangarInStation.GetStack(l.type, toLoad));
                        qteLoaded += toLoad;
                        _loaded[l.type] += toLoad;
                        possibleLoad -= toLoad;
                    }
                }
            }

            return qteLoaded;
        }
    }
}