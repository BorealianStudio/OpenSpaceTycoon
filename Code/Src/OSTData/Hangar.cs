

namespace OSTData {

    /// <summary>
    /// Un Hangar est une zone dans une station appartenant à une societe.
    /// Elle contient des ResourceStack.
    /// </summary>
    public class Hangar {

        /// <summary>
        /// Constructeur de base
        /// </summary>
        /// <param name="station">La station qui contient ce hangar</param>
        /// <param name="corporation">La corporation a qui appartient ce hangar</param>
        public Hangar(Station station, Corporation corporation) {
            Station = station;
            Corporation = corporation;
            ResourceElements = new ResourceElement[1];
        }

        /// <summary> The station this hangar is in </summary>
        public Station Station { get; private set; }

        /// <summary> Corporation owning this hangar </summary>
        public Corporation Corporation { get; private set; }

        /// <summary> All ResourceElement in this hangar </summary>
        public ResourceElement[] ResourceElements { get; private set; }

        /// <summary>
        /// Permet de connaitre la quantite d'une ressource dans ce hangar.
        /// </summary>
        /// <param name="type">la ressource a tester.</param>
        /// <returns>la quantite de m3 de la ressource demande.</returns>
        public int GetResourceQte(ResourceElement.ResourceType type) {
            for (int i = 0; i < ResourceElements.Length; i++) {
                if (ResourceElements[i].Type == type) {
                    return ResourceElements[i].Quantity;
                }
            }
            return 0;
        }

        /// <summary>
        /// Permet d'ajouter un stack dans ce hangar. Le stack sera vide dans la transaction
        /// </summary>
        /// <param name="stack">le stack a ajouter</param>
        public void Add(ResourceStack stack) {
            //trouver si un stack de ce type existe, sinon en creer un
            //les combiner
            //vider le stack
            throw new System.NotImplementedException();
        }
    }

}