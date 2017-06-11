namespace OSTData {

    /// <summary>
    /// Une corporation est une entreprise dans space tycoon. Elle est proprietaire
    /// des vaisseaux/hangars. Un joueur contrôle une corporation.
    /// </summary>
    public class Corporation {

        /// <summary>
        /// Constructeur de base
        /// </summary>
        /// <param name="id">l'identifiant unique de cette corporation</param>
        public Corporation(int id) {
            ID = id;
        }

        /// <summary> identifiant unique pour cette corporation </summary>
        public int ID { get; private set; }
    }
}