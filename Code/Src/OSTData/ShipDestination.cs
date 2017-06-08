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
            m3toMove -= Unload(m3toMove);

            //chargement
            Load(m3toMove);
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

        /// <summary>
        /// methode a appeler quand on change de destination, sur la nouvelle destination
        /// </summary>
        public void Start() {
            _travelDone = 0.0f;
            _loaded.Clear();
            _unloaded.Clear();

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
            if(_travelDone < 1.0f) {
                return "Moving (" + (_travelDone * 100.0f) + "%";
            }
            
            return "Unloading";
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
            return 0;
        }

        /// <summary>
        /// le vaisseau est en train de se charger dans la station. Il partira quand il aura fini de charger ce qu'il doit
        /// ou quand il sera plein.
        /// <param name="possibleLoad">nombre de m3 qu'on peut charger cette fois</param>
        /// </summary>
        private void Load(int possibleLoad) {
            Station station = Ship.CurrentStation;
            if (null == station)
                return;

            int qteLoaded = 0;
            int qteLeft = 0;

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
                    qteLeft += l.qte - _loaded[l.type];
                }
            }

            if (qteLoaded == 0 || qteLeft == 0) {
                Done = true;
                Ship.NextDestination();
            }
        }
    }
}