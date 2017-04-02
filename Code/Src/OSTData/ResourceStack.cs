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
            mResourceElementsInto = new List<ResourceElement>();
        }

        /// <summary>
        /// Construction a partir d'un ResourceElement.
        /// </summary>
        /// <param name="elem">L'élement a ajouter au stack a la construction </param>
        public ResourceStack(ResourceElement elem) {
            Type = elem.Type;
            mResourceElementsInto = new List<ResourceElement>();
            Add(elem);
        }


        /// <summary> Le type de ressource dans ce Stack. </summary>
        public ResourceElement.ResourceType Type { get; private set; }

        /// <summary> L'ensemble des ResourceElement dans le stack. </summary>
        private List<ResourceElement> mResourceElementsInto;

        /// <summary> La quantite total de ressource dans ce Stack en m3. </summary>
        public int Qte {
            get {
                int qte = 0;
                for (int i = 0; i < mResourceElementsInto.Count; i++) {
                    qte += mResourceElementsInto[i].Quantity;
                }
                return qte;
            }
        }



        #region methods
        /// <summary>
        /// Ajout d'un ResourceElement à ce stack. 
        /// </summary>
        /// <param name="elem"> Le resourceElement a ajouter. </param>
        public void Add(ResourceElement elem) {
            // il faut ajouter le Elem a ce stack si et seulement si ils ont le meme
            // type de ressource.
            if (elem.Type == Type) {
                ResourceElement newElement = elem.Split(elem.Quantity);
                mResourceElementsInto.Add(newElement);
                mResourceElementsInto.Sort( // Tris de la liste par date croissante (élément plus vieux en premier)
                    delegate (ResourceElement r1, ResourceElement r2) {
                        return r1.DateProd - r2.DateProd;
                    }
                );
            } else {
                //Erreur
            }
        }

        /// <summary>
        /// Retire un ResourceElement de ce stack.
        /// </summary>
        /// <param name="elem"> Le ResourceElement à retirer.</param>
        public void Remove(ResourceElement elem) {
            if (mResourceElementsInto.Contains(elem)) {
                mResourceElementsInto.Remove(elem);
            }
        }

        /// <summary>
        /// Ajout d'un ResourceStac a ce stack. Le stack passe en parametre sera vide 
        /// dans l'operation si le type de ressource est le meme.
        /// </summary>
        /// <param name="stack">Le stack a ajouter</param>
        public void Add(ResourceStack stack) {
            if (stack.Type == Type) {
                for (int i = 0; i < stack.mResourceElementsInto.Count; i++) {
                    Add(stack.mResourceElementsInto[i]);
                    stack.Remove(mResourceElementsInto[i]);
                }
            } else {
                //Erreur
            }
            
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
            if (qte > 0 && qte >= Qte) {
                ResourceStack subStack = new ResourceStack(Type);

                return subStack;

            } else {
                return null;
            }
        }

        /// <summary>
        /// Permet de récuperer tout les ResourceElement qui constituent ce Stack
        /// </summary>
        /// <returns>Une list de reference vers les ResourceElement de ce Stack</returns>
        public List<ResourceElement> GetElements() {
            // Il faut retourner les ResourceElement de ce stack dans l'ordre de date de production.
            return mResourceElementsInto; // La liste est déjà triée à chaque ajout.
        }
        #endregion
    }
}