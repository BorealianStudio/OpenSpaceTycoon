namespace OSTData {

    /// <summary>
    /// cette classe représente un element de ressource.
    /// Chaque element ne peut avoir qu'un type et etre compose de ressource 
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

        /// <summary>
        /// constructeur avec paramètres
        /// </summary>
        /// <param name="Type"> le type de resource de cet element </param>
        /// <param name="From"> la station qui a produit cette resource </param>
        /// <param name="Qte"> la quantite en m3 </param>
        /// <param name="ProductionDate"> la date ou cette ressource est produite </param>
        /// <returns>l'element retire </returns>
        public ResourceElement(ResourceType Type, Station From, int Qte, int ProductionDate) {
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
        /// Permet de diviser un element en 2 elements en creant un nouveau ResourceElement
        /// et en enlevant la qte necessaire a cet element.
        /// On ne peut pas diviser un element si cela aurait pour but de le vider
        /// </summary>
        /// <param name="NewElementQte"> la quantite a retirer de l'element et mettre dans
        /// l'element cree</param>
        /// <returns>Un nouveau ResourceElement avec la quantite demande dedans
        ///  ou null si l'operation n'est pas possible</returns>
        public ResourceElement Split(int NewElementQte) {
            throw new System.NotImplementedException();
        }

        #endregion

        #region private

        #endregion
    }
}