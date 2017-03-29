using System.Collections.Generic;

namespace OSTData {
    /// <summary>
    /// Cette classe represente un stack, un stack est un tas d'element du meme type
    /// Un stack peut donc contenir des elements de provenances diverse, mais tous du
    /// meme type.
    /// </summary>
    public class ResourceStack {

        /// <summary>
        /// Constructeur de base
        /// </summary>
        /// <param name="type">Le type de ressource que pourra contenir ce stack </param>
        public ResourceStack(ResourceElement.ResourceType type) {
        }

        /// <summary>
        /// Construction a partir d'un ResourceElement
        /// </summary>
        /// <param name="elem">L'élement a ajouter au stack a la construction </param>
        public ResourceStack(ResourceElement elem) {
        }

        /// <summary> Le type de ressource dans ce Stack </summary>
        public ResourceElement.ResourceType Type {
            get { throw new System.NotImplementedException(); }
        }

        /// <summary> La quantite de ressource dans ce Stack en m3</summary>
        public int Qte {
            get { throw new System.NotImplementedException(); }
        }

        /// <summary>
        /// Ajout d'un ResourceElement a ce stack. 
        /// </summary>
        /// <param name="Elem"> le resourceElement a ajouter </param>
        public void Add(ResourceElement Elem) {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Permet d'extraire un ResourceStack de ce stack.
        /// Les ressources les plus anciennes seront priorisées et Qte sera retire de ce stack
        /// </summary>
        /// <param name="Qte"></param>
        /// <returns>un ResourceStack contenant Qte m3 ou null si ce n'est pas possible</returns>
        public ResourceStack GetSubStack(int Qte) {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Permet de récuperer tout les ResourceElement qui constituent ce Stack
        /// </summary>
        /// <returns>Une list de reference vers les ResourceElement de ce Stack</returns>
        public List<ResourceElement> GetElements() {
            throw new System.NotImplementedException();
        }
    }
}