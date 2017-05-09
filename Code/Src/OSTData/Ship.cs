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
        }

        /// <summary> l'identifiant unique de ce vaisseau </summary>
        public int ID { get; private set; }

        /// <summary> mettre a jour le  vaisseau en faisant avancer d'un frame (1/5e de jour)</summary>
        public void Update() {
        }

        /// <summary> La liste ordonee des destinations de ce vaisseau </summary>
        public List<Station> Destinations
        {
            get { return new List<Station>(_dest); }
            private set { _dest = value; }
        }

        /// <summary>
        /// Ajouter une destination a la fin de la liste des destinatinos
        /// </summary>
        /// <param name="station">la destination a ajouter</param>
        public void AddDestination(Station station) {
            _dest.Add(station);
        }

        /// <summary>
        /// permet d'ajouter une destination dans la liste de destination
        /// </summary>
        /// <param name="station">la station a rejoindre</param>
        /// <param name="index">l'ordre dans la liste de destination</param>
        public void AddDestination(Station station, int index) {
            if (index < 0 || index > _dest.Count)
                return;
            _dest.Insert(index, station);
        }

        /// <summary> Les stations a rejoindres /// </summary>
        private List<Station> _dest = new List<Station>();

        /// <summary> l'index dans _dest de la cible en cours </summary>
        private int currentDest = -1;

        //cette methode lance le calcul du chemin qui sera parcouru par le vaisseau.
        private void PreparePath() {
        }
    }
}