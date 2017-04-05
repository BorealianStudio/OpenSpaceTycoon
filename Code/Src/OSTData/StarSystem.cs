namespace OSTData {

    /// <summary>
    /// Classe representant un systeme solaire. Surtout utilisé pour gérer les positions
    /// </summary>
    public class StarSystem {

        /// <summary> Constructeur par type </summary>
        public StarSystem(int id, OSTTools.Vector3D position) {
            ID = id;
            Position = position;
        }

        /// <summary>L'id de ce systeme, doit être unique dans son univers </summary>
        public int ID { get; private set; }

        /// <summary>La position de ce systeme par rapport au centre de l'univers en Annees lumieres</summary>
        public OSTTools.Vector3D Position { get; private set; }
    }
}