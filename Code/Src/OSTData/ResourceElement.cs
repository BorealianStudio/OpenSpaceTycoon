// @class ResourceStack
// Cette classe représente un "tas" de ressource. 
// Chaque tas de ressource ne peux avoir qu'un type de ressource,
// Un ResourceStack peut contenir des ResourceElement provenants de differentes
// stations et produits à un moment different. Ces informations ne doivent pas être perdues.

namespace OSTData {
    public class ResourceElement {

        public enum ResourceType {
            Unknown,
            Water,
            Wastes,
            ToxicWaste
        }

        public ResourceElement() { }

        /// constructeur avec paramètres
        /// @param type le type de resource de cet element
        /// @param from la station qui a produit cette resource
        /// @param qte la quantite en m3
        /// @param productionDate la date ou cette ressource est produite
        public ResourceElement(ResourceType Type, Station From, int Qte, int ProductionDate) {
        }

        public ResourceType Type{
            get { throw new System.NotImplementedException(); }
        }

        public Station Station {
            get { throw new System.NotImplementedException(); }
        }

        public int Qte { 
            get { throw new System.NotImplementedException(); }
        }

        public int DateProd { 
            get { throw new System.NotImplementedException(); }
        }

        /// <summary>
        /// Permet de diviser un element en 2 elements
        /// 
        /// </summary>
        /// <param name="Qte"> la quantite a retirer de l'element </param>
        /// <returns>l'element retire </returns>
        public ResourceElement Divide(int Qte) {
            throw new System.NotImplementedException();
        }
    }
}