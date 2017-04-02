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
        }

        /// <summary> Constructeur depuis une savegame</summary>
        /// <param name="pathToSaveFile">le chemin de la savegame</param>
        public Universe(string pathToSaveFile) {
        }

        /// <summary>
        /// Effectuer une mise a jour d'un tour. c'est faire avancer le temps de 1/5 de jour
        /// </summary>
        public void Update() {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Sauve l'etat actuel de la partie dans une fichier
        /// </summary>
        /// <param name="pathToSaveFile">Chemin du ficher de sauvegarde</param>
        public void Save(string pathToSaveFile) {
            throw new System.NotImplementedException();
        }

        /// <summary> Recuperer la liste de toutes les stations de l'univers </summary>
        /// <returns>Une collection de reference sur les stations</returns>
        public ICollection<Station> GetStations() {
            throw new System.NotImplementedException();
        }

    }
}