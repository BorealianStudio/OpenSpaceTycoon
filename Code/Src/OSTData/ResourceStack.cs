
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
    }
}