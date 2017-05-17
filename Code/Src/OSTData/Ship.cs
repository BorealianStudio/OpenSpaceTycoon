using System;
using System.Collections.Generic;

namespace OSTData {

    /// <summary>
    /// C'est un vaisseau, il peut etre dans une station ou en deplacement
    /// dans l'espace
    /// </summary>
    [Serializable]
    public class Ship {

        /// <summary> constructeur de base </summary>
        /// <param name="id">l'id unique de ce vaisseau</param>
        public Ship(int id) {
            ID = id;
            Cargo = new ShipCargo(this);
        }

        /// <summary> l'identifiant unique de ce vaisseau </summary>
        public int ID { get; private set; }

        /// <summary> C'est la zone qui contient les ressources dans le vaisseau </summary>
        public ShipCargo Cargo { get; private set; }

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
        }

        /// <summary>
        /// Ajouter une destination a la fin de la liste des destinatinos
        /// </summary>
        /// <param name="station">la destination a ajouter</param>
        public ShipDestination AddDestination(Station station) {
            ShipDestination dest = new ShipDestination(this, station);
            _dest.Add(dest);
            return dest;
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
                return dest;
            } else {
                ShipDestination dest = new ShipDestination(this, station);
                _dest.Insert(index, dest);
                return dest;
            }
        }

        /// <summary>
        /// Recuperer la destion a l'index donnee (index base 0)
        /// </summary>
        /// <param name="index">l'index, doit etre entre 0 et le nombre de destination - 1</param>
        /// <returns>la destination ou null si pas de reponse</returns>
        public ShipDestination GetDestinations(int index) {
            if (index < 0 || index >= _dest.Count)
                return null;

            return _dest[index];
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