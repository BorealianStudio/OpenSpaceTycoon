namespace OSTData {

    /// <summary>
    /// Une corporation est une entreprise dans space tycoon. Elle est proprietaire
    /// des vaisseaux/hangars. Un joueur contrôle une corporation.
    /// </summary>
    public class Corporation {

        #region events

        /// <summary> format delegate avec un long en param </summary>
        /// <param name="val">le parametre entier</param>
        public delegate void LongAction(long val);

        /// <summary> Event triggered quand ce ResourceStack change </summary>
        public event LongAction onICUChange = delegate { };

        #endregion events

        /// <summary>
        /// Constructeur de base
        /// </summary>
        /// <param name="id">l'identifiant unique de cette corporation</param>
        public Corporation(int id) {
            ID = id;
        }

        /// <summary> Identifiant unique pour cette corporation </summary>
        public int ID { get; private set; }

        private long _ICU = 0;

        /// <summary> Le nombre d'ICU presentement disponible dans la corporation </summary>
        public long ICU {
            get { return _ICU; }
            private set {
                _ICU = value; onICUChange(_ICU);
            }
        }

        /// <summary>
        /// Ajouter des ICU dans le compte de la corporation.
        /// </summary>
        /// <param name="qte"> La quantite a ajouter</param>
        /// <param name="reason"> Le poste budgetaire ou ajouter les credits (inutilise pour le moment) </param>
        public void AddICU(int qte, string reason) {
            ICU += qte;
        }

        /// <summary>
        /// Retirer des ICU du compte de la corp et ajouter une ligne dans les depenses
        /// </summary>
        /// <param name="qte"> La quantite a retirer</param>
        /// <param name="reason"> La raison de la depense</param>
        public void RemoveICU(int qte, string reason) {
            ICU -= qte;
        }
    }
}