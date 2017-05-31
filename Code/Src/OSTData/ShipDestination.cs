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
        }

        /// <summary>
        /// update cette destination
        /// </summary>
        public void Update() {
            //approche de la station

            //dechargement

            //chargement
            Load();
        }

        /// <summary>
        /// Station de destination
        /// </summary>
        public Station Destination { get; private set; }

        /// <summary>
        /// Le vaisseau a qui appartient cette destination
        /// </summary>
        public Ship Ship { get; private set; }

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

        /// <summary>
        /// le vaisseau est en train de se charger dans la station. Il partira quand il aura fini de charger ce qu'il doit
        /// ou quand il sera plein.
        /// </summary>
        private void Load() {
            Station station = Ship.CurrentStation;
            if (null == station)
                return;

            Hangar myHangarInStation = station.GetHangar(Ship.Owner.ID);
            if (null == myHangarInStation)
                return;

            int qteLoaded = 0;
            foreach (LoadData l in _loads) {
                int present = myHangarInStation.GetResourceQte(l.type);
                int toLoad = Math.Min(present, 5);//todo magicnumber

                if (toLoad > 0) {
                    Ship.Cargo.Add(myHangarInStation.GetStack(l.type, toLoad));
                    Destination.InformLoading(Ship, l.type);
                    qteLoaded += toLoad;
                }
            }

            if (qteLoaded == 0) {
                throw new NotImplementedException("ends of loading not implemented");
            }
        }
    }
}