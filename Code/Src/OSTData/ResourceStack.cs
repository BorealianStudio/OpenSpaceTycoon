using System.Collections.Generic;

namespace OSTData {
    /// <summary>
    /// Cette classe represente un stack, un stack est un tas d'element du meme type
    /// Un stack peut donc contenir des elements de provenances diverse, mais tous du
    /// meme type.
    /// </summary>
    public class ResourceStack {

        /// <summary>
        /// Constructeur de base.
        /// </summary>
        /// <param name="type">Le type de ressource que pourra contenir ce stack </param>
        public ResourceStack(ResourceElement.ResourceType type) {
            Type = type;
            Qte = 0;
            mResourceElementsInto = new List<ResourceElement>();
        }

        /// <summary>
        /// Construction a partir d'un ResourceElement.
        /// </summary>
        /// <param name="elem">L'élement a ajouter au stack a la construction </param>
        public ResourceStack(ResourceElement elem) {
            Type = elem.Type;
            Qte = 0;
            mResourceElementsInto = new List<ResourceElement>();
            Add(elem);
        }

        /// <summary> Le type de ressource dans ce Stack </summary>
        public ResourceElement.ResourceType Type { get; private set; }

        /// <summary> La quantite total de ressource dans ce Stack en m3</summary>
        public int Qte { get; private set; }

        /// <summary> L'ensemble des ResourceElement composant le stack </summary>
        private List<ResourceElement> mResourceElementsInto;

        /// <summary>
        /// Ajout d'un ResourceElement a ce stack. 
        /// </summary>
        /// <param name="elem"> le resourceElement a ajouter</param>
        public void Add(ResourceElement elem) {
            // il faut ajouter le Elem a ce stack si et seulement si ils ont le meme
            // type de ressource.
            if (elem.Type == Type) {
                ResourceElement newElement = new ResourceElement(elem.Type, elem.Station, elem.Quantity, elem.DateProd);
                mResourceElementsInto.Add(newElement);
                Qte += newElement.Quantity;
                mResourceElementsInto.Sort( // Triage de la liste par date croissante (élément plus vieux en premier)
                    delegate (ResourceElement r1, ResourceElement r2) {
                        return r1.DateProd - r2.DateProd;
                    }
                );
                elem.Remove(elem.Quantity);
            } else {
                //Erreur
            }
        }

        /// <summary>
        /// Ajout d'un ResourceStac a ce stack. Le stack passe en parametre sera vide 
        /// dans l'operation si le type de ressource est le meme.
        /// </summary>
        /// <param name="stack">le stack a ajouter</param>
        public void Add(ResourceStack stack) {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Permet d'extraire un ResourceStack de ce stack.
        /// Les ressources les plus anciennes seront priorisées et Qte sera retire de ce stack
        /// </summary>
        /// <param name="qte"></param>
        /// <returns>un ResourceStack contenant Qte m3 ou null si ce n'est pas possible</returns>
        public ResourceStack GetSubStack(int qte) {
            // Il faut si possible creer un stack qui contient les ressources les plus anciennes
            // de ce stack et les enlever de ce stack. Il ne faut pas garder de resourceElement vide
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Permet de récuperer tout les ResourceElement qui constituent ce Stack
        /// </summary>
        /// <returns>Une list de reference vers les ResourceElement de ce Stack</returns>
        public List<ResourceElement> GetElements() {
            // Il faut retourner les ResourceElement de ce stack dans l'ordre de date de production.
            return mResourceElementsInto; // La liste est déjà triée à chaque ajout.
        }
    }
}