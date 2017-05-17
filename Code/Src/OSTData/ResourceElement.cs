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
#pragma warning disable CS1591
            Electronics,
            Food,
            Iron,
            MechanicalPart,
            Unknown,
            Water,
            Wastes,
            ToxicWaste,
            Tennantite,
            Tobernite
#pragma warning restore CS1591
        }

        /// <summary>
        /// constructeur avec paramètres
        /// </summary>
        /// <param name="type"> le type de resource de cet element </param>
        /// <param name="from"> la station qui a produit cette resource </param>
        /// <param name="qte"> la quantite en m3 </param>
        /// <param name="productionDate"> la date ou cette ressource est produite </param>
        /// <returns>l'element retire </returns>
        public ResourceElement(ResourceType type, Station from, int qte, int productionDate) {
            Type = type;
            Station = from;
            Quantity = qte;
            DateProd = productionDate;
        }

        #region getters

        /// <summary> le type de ressource de cet element </summary>
        public ResourceType Type { get; private set; }

        /// <summary> la station qui a produit les ressources de cet element</summary>
        public Station Station { get; private set; }

        /// <summary> la quantite de ressource dans cet element</summary>
        public int Quantity { get; private set; }

        /// <summary> la date ou les ressource de cet element ont ete produites</summary>
        public int DateProd { get; private set; }

        #endregion getters

        #region methods

        /// <summary>
        /// Permet de diviser un element en 2 elements en creant un nouveau ResourceElement
        /// et en enlevant la qte necessaire a cet element.
        /// </summary>
        /// <param name="newElementQte">La quantite a retirer de l'element et mettre dans
        /// l'element cree</param>
        /// <returns>Un nouveau ResourceElement avec la quantite demande dedans
        ///  ou null si l'operation n'est pas possible</returns>
        public ResourceElement Split(int newElementQte) {
            if (Quantity >= newElementQte && newElementQte > 0) {
                Quantity -= newElementQte;
                return new ResourceElement(Type, Station, newElementQte, DateProd);
            } else {
                return null;
            }
        }

        #endregion methods

        public override string ToString() {
            return Quantity + " of " + Type + " time : " + DateProd;
        }
    }
}