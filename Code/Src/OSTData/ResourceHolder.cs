using System.Collections.Generic;

namespace OSTData {

    /// <summary>
    /// Un REsourceHolder est une zone, Elle contient des ResourceStack. C'est un peu une forme d'inventaire.
    /// </summary>
    public class ResourceHolder {

        #region events

        /// <summary> format delegate avec un stack en param </summary>
        /// <param name="stack">le parametre stack</param>
        public delegate void ResourceStackAction(ResourceStack stack);

        /// <summary> Event triggered quand un nouveau stack apparait dans ce holder</summary>
        public event ResourceStackAction onNewStack = delegate { };

        /// <summary> Event triggered un stack present disparait</summary>
        public event ResourceStackAction onRemoveStack = delegate { };

        #endregion events

        /// <summary>
        /// Constructeur de base
        /// </summary>
        public ResourceHolder() {
            ResourceStacks = new List<ResourceStack>();
        }

        /// <summary> Tous les ResourceStack dans ce hangar. Il ne doit y avoir qu'un ResourceStack de chaque type.</summary>
        public List<ResourceStack> ResourceStacks { get; private set; }

        /// <summary>
        /// Permet de connaitre la quantite d'une ressource dans ce hangar.
        /// </summary>
        /// <param name="type">la ressource a tester.</param>
        /// <returns>la quantite de m3 de la ressource demande.</returns>
        public int GetResourceQte(ResourceElement.ResourceType type) {
            for (int i = 0; i < ResourceStacks.Count; i++) {
                if (ResourceStacks[i].Type == type) {
                    return ResourceStacks[i].Qte;
                }
            }
            return 0;
        }

        /// <summary>
        /// retire une certaine quantite d'une ressource dans un hangar. Les resources retirees sont toujours celles des
        /// stacks les plus anciens.
        /// </summary>
        /// <param name="type">le type de resource a retirer</param>
        /// <param name="qte"></param>
        /// <returns>un stack qui contient la qte de resource du type si possible, null sinon</returns>
        public ResourceStack GetStack(ResourceElement.ResourceType type, int qte) {
            if (GetResourceQte(type) < qte)
                return null;

            ResourceStack currentStack = null;
            foreach (ResourceStack s in ResourceStacks) {
                if (s.Type == type)
                    currentStack = s;
            }

            ResourceStack result = currentStack.GetSubStack(qte);
            if (currentStack.Qte == 0) {
                onRemoveStack(currentStack);
                ResourceStacks.Remove(currentStack);
            }
            return result;
        }

        /// <summary>
        /// Permet d'ajouter un stack dans ce hangar. Le stack sera vide dans la transaction.
        /// </summary>
        /// <param name="stack">Le stack a ajouter.</param>
        public void Add(ResourceStack stack) {
            ResourceStack inHangar = null;
            for (int i = 0; i < ResourceStacks.Count; i++) { //trouver si un stack de ce type existe, sinon en creer un
                if (ResourceStacks[i].Type == stack.Type) {
                    inHangar = ResourceStacks[i];
                }
            }
            if (inHangar == null) { // Il n'existait pas de stack dans le hangar.
                ResourceStacks.Add(stack.GetSubStack(stack.Qte)); // Fait une copie dans le hangar et vide l'ancien stack
                onNewStack(stack);
            } else {
                inHangar.Add(stack); // Combine les stacks
            }
        }
    }
}