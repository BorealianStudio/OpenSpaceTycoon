using System;
using System.Collections.Generic;

namespace OSTData {

    /// <summary>
    /// Classe representant une station
    /// </summary>
    [Serializable]
    public class Station {
        public const float defaultStanding = 0.0f;

        /// <summary> les types que peuvent avoir les stations </summary>
        public enum StationType {
#pragma warning disable CS1591
            City,
            Mine,
            IceField,
            Agricultural,
            FuelRefinery,
            RockRefinery,
            Reprocessing,
            Shipyard
#pragma warning restore CS1591
        }

        /// <summary> Constructeur par type </summary>
        /// <param name="type">le type de la station</param>
        /// <param name="starSystem">Le systeme qui contient cette station</param>
        /// <param name="position">La position de la station dans sons syteme, en AU</param>
        /// <param name="iID">un identifiant pour cette station</param>
        public Station(StationType type, StarSystem starSystem, OSTTools.Vector3 position, int iID) {
            Type = type;
            Position = position;
            _gates = new List<Portal>();
            Name = "StationName";
            System = starSystem;
            ID = iID;
            Hangar h = new Hangar(this, starSystem.Universe.GetCorporation(-1));
            _hangars.Add(-1, h);
        }

        private Station() {
        }

        /// <summary> Le type de cette station </summary>
        [Newtonsoft.Json.JsonProperty]
        public StationType Type { get; private set; }

        /// <summary> Le systeme solaire qui contient cette station </summary>
        [Newtonsoft.Json.JsonProperty]
        public StarSystem System { get; private set; }

        /// <summary> L'identifiant de cette station </summary>
        [Newtonsoft.Json.JsonProperty]
        public int ID { get; private set; }

        /// <summary> La position de la station dans ce systeme, en Unite astronomique</summary>
        [Newtonsoft.Json.JsonProperty]
        public OSTTools.Vector3 Position;// { get; private set; }

        /// <summary> le nom de la station </summary>
        public string Name { get; set; }

        /// <summary>
        /// appele une fois par frame, quand les updates des vaisseaux sont fini.
        /// </summary>
        public void Update() {
        }

        /// <summary>
        /// methode utilise par un vaisseau pour indiquer qu'il est en train de s echarger d'un certain type de ressource
        /// </summary>
        /// <param name="ship">le vaisseau qui se charge</param>
        /// <param name="type">le type de ressource qu'il charge</param>
        public void InformLoading(Ship ship, ResourceElement.ResourceType type) {
            if (!_currentLoaders.ContainsKey(ship.Owner.ID)) {
                _currentLoaders.Add(ship.Owner.ID, new HashSet<ResourceElement.ResourceType>());
            }
            _currentLoaders[ship.Owner.ID].Add(type);
        }

        /// <summary> Liste des portails reliant cette station </summary>
        [Newtonsoft.Json.JsonIgnore]
        public List<Portal> Gates {
            get { return new List<Portal>(_gates); }
        }

        /// <summary> Ajoute un portal aux alentour de cette station </summary>
        /// <param name="portal">le portail a ajouter </param>
        public void AddGate(Portal portal) {
            if (portal.Station1.ID == ID || portal.Station2.ID == ID)
                _gates.Add(portal);
        }

        /// <summary>
        /// creer un hangar dans la station pour la owner donne, ou retourner celui existant
        /// s'il existe deja
        /// </summary>
        /// <param name="owner">la corporation a qui appartient ce hangar</param>
        /// <returns>le hangar creer, ou celui qui existait deja</returns>
        public Hangar CreateHangar(Corporation owner) {
            Hangar result = GetHangar(owner.ID);
            if (null == result) {
                result = new Hangar(this, owner);
                _hangars.Add(owner.ID, result);
            }

            return result;
        }

        /// <summary> permet de creer un vaisseau dans cette station </summary>
        /// <param name="corp">la corporation proprietaire de ce vaisseau</param>
        /// <returns>le vaisseau cree</returns>
        public Ship CreateShip(Corporation corp) {
            Ship result = new Ship(System.Universe.Ships.Count + 1, corp);
            result.CurrentStation = this;
            System.Universe.Ships.Add(result);
            return result;
        }

        /// <summary>
        /// Recuperer le hangar d'un joueur.
        /// </summary>
        /// <param name="playerID">l'id du joueur ou -1 pour récupérer le hangar de la station</param>
        /// <returns>le hangar du joueur s'il exist, null sinon</returns>
        public Hangar GetHangar(int playerID) {
            if (_hangars.ContainsKey(playerID))
                return _hangars[playerID];

            return null;
        }

        /// <summary> Recupere la liste des ressource achetes par cette station </summary>
        public HashSet<ResourceElement.ResourceType> Buyings {
            get { return new HashSet<ResourceElement.ResourceType>(_buyingPrices.Keys); }
        }

        /// <summary> ajuster le prix d'une ressource </summary>
        /// <param name="type">le type de ressource a varier le prix</param>
        /// <param name="newPrice">le nouveau prix d'achat</param>
        public void SetBuyingPrice(ResourceElement.ResourceType type, int newPrice) {
            if (!_buyingPrices.ContainsKey(type))
                _buyingPrices.Add(type, newPrice);
            else
                _buyingPrices[type] = newPrice;
        }

        /// <summary> recuperer le prix d'achat actuel pour une ressource dans cette station</summary>
        /// <param name="type">la type a verifier</param>
        /// <returns>le prix d'achat actuel en ICU</returns>
        public int GetBuyingPrice(ResourceElement.ResourceType type) {
            if (_buyingPrices.ContainsKey(type))
                return _buyingPrices[type];
            return 0;
        }

        /// <summary>
        /// permet de recuperer le standing d'une corporation pour une ressource donnee dans cette station
        /// </summary>
        /// <param name="type">le type de ressource</param>
        /// <param name="corporationID">l'ID de la corporation dont on veut le standing</param>
        /// <returns>le standing de la corporation pour le type de ressource, defaultStanding si pas de standing</returns>
        public float GetStanding(ResourceElement.ResourceType type, int corporationID) {
            if (_standings.ContainsKey(type)) {
                if (_standings[type].ContainsKey(corporationID)) {
                    return _standings[type][corporationID];
                }
            }
            return defaultStanding;
        }

        /// <summary>
        /// permet de modifier le standing d'une corporation en rapport a un type de ressource donne
        /// </summary>
        /// <param name="type">le type de ressource en question</param>
        /// <param name="corporationID">le corpotation pour lequel le standing doit etre modifie</param>
        /// <param name="standing">le nouveau standing de la corporation</param>
        public void SetStanding(ResourceElement.ResourceType type, int corporationID, float standing) {
            if (!_standings.ContainsKey(type))
                _standings.Add(type, new Dictionary<int, float>());
            if (!_standings[type].ContainsKey(corporationID))
                _standings[type].Add(corporationID, defaultStanding);
            _standings[type][corporationID] = standing;
        }

        /// <summary>
        /// Indique a la station d'effectuer le travail qu'elle doit faire a la fin d'une journee
        /// </summary>
        /// <param name="timestamp">le timestamp en cours</param>
        public void EndDays(int timestamp) {
            Hangar stationHangar = GetHangar(-1);

            foreach (Receipe r in _receipies) {
                for (int qte = 0; qte < r.MaxFreq; qte++) {
                    if (!r.ProduceOneBatch(this, timestamp))
                        break;
                }
            }

            //pour tout les corp qui ont chargé dans ce jour une certaine ressources, on leur boost leur standing
            foreach (int i in _currentLoaders.Keys) {
                foreach (ResourceElement.ResourceType t in _currentLoaders[i]) {
                    Corporation corp = System.Universe.GetCorporation(i);
                    if (null != corp) {
                        float oldStanding = GetStanding(t, i);
                        float newStanding = oldStanding + (1.0f - oldStanding) * 0.05f;
                        SetStanding(t, i, newStanding);
                    }
                }
            }
            _currentLoaders.Clear();
        }

        /// <summary>
        /// Override du equals, test si les stations sont identique
        /// </summary>
        /// <param name="obj">autre station</param>
        /// <returns>true si les attributs sont identiques</returns>
        public override bool Equals(object obj) {
            Station other = obj as Station;
            if (null == other)
                return false;

            if (ID != other.ID || Type != other.Type || Name != other.Name || Position.Distance(other.Position) > 0.1)
                return false;

            return true;
        }

        /// <summary>
        /// custom hash code
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode() {
            return ID;
        }

        private List<Portal> _gates = new List<Portal>();
        private Dictionary<int, HashSet<ResourceElement.ResourceType>> _currentLoaders = new Dictionary<int, HashSet<ResourceElement.ResourceType>>();

        [Newtonsoft.Json.JsonProperty]
        private Dictionary<int, Hangar> _hangars = new Dictionary<int, Hangar>();

        [Newtonsoft.Json.JsonProperty]
        private List<Receipe> _receipies = new List<Receipe>();

        private Dictionary<ResourceElement.ResourceType, int> _buyingPrices = new Dictionary<ResourceElement.ResourceType, int>();
        private Dictionary<ResourceElement.ResourceType, Dictionary<int, float>> _standings = new Dictionary<ResourceElement.ResourceType, Dictionary<int, float>>();

        /// <summary>
        /// demande a la station de preparer les recette de base en fonction de son type
        /// </summary>
        public void InitProduct() {
            switch (Type) {
                case StationType.Agricultural:
                break;

                case StationType.City: {
                    Receipe r1 = new Receipe(1);
                    r1.AddInput(ResourceElement.ResourceType.Water, 50);
                    _receipies.Add(r1);

                    Receipe r2 = new Receipe(1);
                    r2.AddInput(ResourceElement.ResourceType.Food, 100);
                    _receipies.Add(r2);

                    Receipe r3 = new Receipe(1);
                    r3.AddOutput(ResourceElement.ResourceType.Wastes, 100);
                    _receipies.Add(r3);

                    Receipe r4 = new Receipe(10);
                    r4.AddInput(ResourceElement.ResourceType.Iron, 10);
                    r4.AddOutput(ResourceElement.ResourceType.MechanicalPart, 10);
                    _receipies.Add(r4);

                    Receipe r5 = new Receipe(10);
                    r5.AddInput(ResourceElement.ResourceType.Iron, 10);
                    r5.AddOutput(ResourceElement.ResourceType.Electronics, 10);
                    _receipies.Add(r5);
                }
                break;

                case StationType.FuelRefinery:
                break;

                case StationType.IceField:
                break;

                case StationType.Mine: {
                    Receipe r1 = new Receipe(1);
                    r1.AddOutput(ResourceElement.ResourceType.Tobernite, 20);
                    _receipies.Add(r1);

                    Receipe r2 = new Receipe(13);
                    r2.AddInput(ResourceElement.ResourceType.MechanicalPart, 10);
                    r2.AddOutput(ResourceElement.ResourceType.Tobernite, 10);
                    _receipies.Add(r2);

                    Receipe r3 = new Receipe(1);
                    r3.AddOutput(ResourceElement.ResourceType.Tennantite, 20);
                    _receipies.Add(r3);

                    Receipe r4 = new Receipe(13);
                    r4.AddInput(ResourceElement.ResourceType.MechanicalPart, 10);
                    r4.AddOutput(ResourceElement.ResourceType.Tennantite, 10);
                    _receipies.Add(r4);
                }
                break;

                case StationType.Reprocessing:
                break;

                case StationType.RockRefinery:
                break;

                case StationType.Shipyard:
                break;
            }
        }
    }
}