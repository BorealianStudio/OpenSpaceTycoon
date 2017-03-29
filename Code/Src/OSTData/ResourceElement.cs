namespace OSTData {

    /// <summary>
    /// cette classe représente un tas de ressource.
    /// Chaque tas ne peut avoir qu'un type et etre compose de ressource 
    /// provenant de la meme station et produite au meme moment.
    /// </summary>
    public class ResourceElement {

        /// <summary>
        /// Les different type de ressource present dans le jeu
        /// </summary>
        public enum ResourceType {
            /// <summary> Valeur par defaut </summary>
            Unknown,
            /// <summary> Eau</summary>
            Water,
            /// <summary> Dechets </summary>
            Wastes,
            /// <summary> dechets toxiques</summary>
            ToxicWaste
        }
        /// <summary> constructeur sans parametres </summary>
        public ResourceElement() { }

        /// <summary>
        /// constructeur avec paramètres
        /// </summary>
        /// <param name="Type"> le type de resource de cet element </param>
        /// <param name="From"> la station qui a produit cette resource </param>
        /// <param name="Qte"> la quantite en m3 </param>
        /// <param name="ProductionDate"> la date ou cette ressource est produite </param>
        /// <returns>l'element retire </returns>
        public ResourceElement(ResourceType Type, Station From, int Qte, int ProductionDate) {
            // todo 
        }

        #region getters
        /// <summary> le type de ressource de cet element </summary>
        public ResourceType Type{
            get { throw new System.NotImplementedException(); }
        }
        /// <summary> la station qui a produit les ressources de cet element</summary>
        public Station Station {
            get { throw new System.NotImplementedException(); }
        }

        /// <summary> la quantite de ressource dans cet element</summary>
        public int Qte { 
            get { throw new System.NotImplementedException(); }
        }

        /// <summary> la date ou les ressource de cet element ont ete produites</summary>
        public int DateProd { 
            get { throw new System.NotImplementedException(); }
        }
        #endregion

        #region methods

        /// <summary>
        /// Permet de diviser un element en 2 elements
        /// 
        /// </summary>
        /// <param name="Qte"> la quantite a retirer de l'element </param>
        /// <returns>l'element retire </returns>
        public ResourceElement Divide(int Qte) {
            throw new System.NotImplementedException();
        }

        #endregion

        #region private

        #endregion
    }
}