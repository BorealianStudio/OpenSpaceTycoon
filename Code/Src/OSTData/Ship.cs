using System;
using System.Collections.Generic;

namespace OSTData {

    /// <summary>
    /// C'est un vaisseau, il peut etre dans une station ou en deplacement
    /// dans l'espace
    /// </summary>
    [Serializable]
    public class Ship {

        #region events

        /// <summary> format delegate sans parametre</summary>
        public delegate void voidAction();

        /// <summary> event quand on ajoute, supprime une destination ou change la destination actuelle</summary>
        public event voidAction onDestinationChange = delegate { };

        #endregion events

        /// <summary> constructeur de base </summary>
        /// <param name="id">l'id unique de ce vaisseau</param>
        /// <param name="owner">la corporation qui possede ce vaisseau</param>
        public Ship(int id, Corporation owner) {
            ID = id;
            Cargo = new ShipCargo(this);
            Owner = owner;
        }

        /// <summary> l'identifiant unique de ce vaisseau </summary>
        public int ID { get; private set; }

        /// <summary> C'est la zone qui contient les ressources dans le vaisseau </summary>
        public ShipCargo Cargo { get; private set; }

        /// <summary> The corporation owning this ship </summary>
        public Corporation Owner { get; private set; }

        /// <summary> la station ou est actuellement le vaisseau, null si dans l'espace </summary>
        public Station CurrentStation { get; set; }

        /// <summary> mettre a jour le  vaisseau en faisant avancer d'un frame (1/5e de jour)</summary>
        public void Update() {
            if (_running) {
                if (_currentDest >= 0) {
                    _dest[_currentDest].Update();
                }
            }
        }

        /// <summary>
        /// demande au vaisseau de commencer(ou reprendre) le travail
        /// </summary>
        public void Start() {
            _running = true;
            if (_dest.Count > 0) {
                _currentDest = 0;
            }
            _dest[_currentDest].Start();
        }

        /// <summary>
        /// la destination en cours, ou null si arrete
        /// </summary>
        public ShipDestination CurrentDestination {
            get {
                if (_running)
                    return _dest[_currentDest];
                else
                    return null;
            }
        }

        /// <summary>
        /// permet de demander au vaisseau de passer a la prochaine destinatin de sa liste
        /// </summary>
        public void NextDestination() {
            if (_dest[_currentDest].Done) {
                _currentDest++;
                if (_currentDest >= _dest.Count)
                    _currentDest = 0;
                _dest[_currentDest].Start();
                onDestinationChange();
            }
        }

        /// <summary>
        /// Ajouter une destination a la fin de la liste des destinatinos
        /// </summary>
        /// <param name="station">la destination a ajouter</param>
        public ShipDestination AddDestination(Station station) {
            ShipDestination dest = new ShipDestination(this, station);
            _dest.Add(dest);
            onDestinationChange();
            return dest;
        }

        /// <summary>
        /// permet de supprimer une destination de la liste de destination.
        /// on ne peux pas supprimer la destination en cours.
        /// </summary>
        /// <param name="dest">la destination a supprimer</param>
        public void RemoveDestination(ShipDestination dest) {
            int index = _dest.IndexOf(dest);
            if (index != -1 && index != _currentDest) {
                if (index < _currentDest)
                    _currentDest--;
                _dest.RemoveAt(index);
                onDestinationChange();
            }
        }

        /// <summary>
        /// Ajouter une destination a un index precis de la liste en cours
        /// </summary>
        /// <param name="station">la destination</param>
        /// <param name="index">l'index ou l'ajouter, 0 au debut</param>
        public ShipDestination AddDestination(Station station, int index) {
            if (index < 0)
                return null;
            else if (index >= _dest.Count) {
                ShipDestination dest = new ShipDestination(this, station);
                _dest.Add(dest);
                onDestinationChange();
                return dest;
            } else {
                ShipDestination dest = new ShipDestination(this, station);
                _dest.Insert(index, dest);
                onDestinationChange();
                return dest;
            }
        }

        /// <summary>
        /// Recuperer la destion a l'index donnee (index base 0)
        /// </summary>
        /// <param name="index">l'index, doit etre entre 0 et le nombre de destination - 1</param>
        /// <returns>la destination ou null si pas de reponse</returns>
        public ShipDestination GetDestination(int index) {
            if (index < 0 || index >= _dest.Count)
                return null;

            return _dest[index];
        }

        /// <summary>
        /// recuperer une liste ordonnee des destinations actuelle du vaisseau
        /// </summary>
        /// <returns> une copie de liste des destinations</returns>
        public List<ShipDestination> GetDestinations() {
            return new List<ShipDestination>(_dest);
        }

        /// <summary>
        /// Permet de connaitre une chaine qui indique l'etat du vaisseau
        /// </summary>
        /// <returns>La tache en cours du vaisseau</returns>
        public string GetState() {
            if (_running) {
                return _dest[_currentDest].GetState();
            }
            return "stopped";
        }

        /// <summary> Les stations a rejoindres /// </summary>
        private List<ShipDestination> _dest = new List<ShipDestination>();

        /// <summary>
        /// index dans _dest qui est la destination en cours
        /// </summary>
        private int _currentDest = -1;

        /// <summary> indique si le vaisseau est en marche (false indique qu'il est a l'arret ou qu'on a demandé son arret a la prochaine station) </summary>
        private bool _running = false;
    }
}