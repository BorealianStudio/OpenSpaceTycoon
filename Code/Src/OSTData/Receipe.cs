using System;
using System.Collections.Generic;

namespace OSTData {

    /// <summary>
    /// Classe representant une recette de production
    /// </summary>
    [Serializable]
    public class Receipe {

        /// <summary>
        /// Constructeur de base
        /// </summary>
        /// <param name="maxFreq">le nombre de fois que la recette peut etre effectue par jour</param>
        public Receipe(int maxFreq) {
            MaxFreq = maxFreq;
        }

        /// <summary>
        /// Ajouter un parametre d'entree de cette recette. C'est a dire une qantite d'un certain type de ressource qui doit etre present pour que
        /// la recette puisse etre lance
        /// </summary>
        /// <param name="type">type de la ressource</param>
        /// <param name="qte">qte necessaire</param>
        public void AddInput(ResourceElement.ResourceType type, int qte) {
            if (!_inputs.ContainsKey(type))
                _inputs.Add(type, 0);
            _inputs[type] = qte;
        }

        /// <summary>
        /// Ajouter un output a cette recette. C'est a dire qu'une fois complete c'est qu'une quantite de ressource sera produite
        /// </summary>
        /// <param name="type">le type de ressource a produire</param>
        /// <param name="qte">la qte de la ressource produite</param>
        public void AddOutput(ResourceElement.ResourceType type, int qte) {
            if (!_outputs.ContainsKey(type))
                _outputs.Add(type, 0);
            _outputs[type] = qte;
        }

        /// <summary> le nombre de fois maximum que peut etre effectue une recette par jour </summary>
        public int MaxFreq { get; private set; }

        [Newtonsoft.Json.JsonProperty]
        private Dictionary<ResourceElement.ResourceType, int> _inputs = new Dictionary<ResourceElement.ResourceType, int>();

        [Newtonsoft.Json.JsonProperty]
        private Dictionary<ResourceElement.ResourceType, int> _outputs = new Dictionary<ResourceElement.ResourceType, int>();
    }
}