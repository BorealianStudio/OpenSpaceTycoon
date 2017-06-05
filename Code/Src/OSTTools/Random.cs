using System;

namespace OSTTools{	
    [Serializable]
	public class Random {

        [Newtonsoft.Json.JsonProperty]
		private int _seed = 0;

		public Random(){
			_seed = 0;
		}

		public Random(int seed){
			_seed = seed % 32749;
		}

		public int Next(){
			_seed = (_seed * 32719 + 3) % 32749;
			return _seed;
		}

		//random number from 0 to maxValue-1 included
		public int Next(int maxValue){
			return(Next() % maxValue);
		}

		public int Range(int minValue ,int maxValue){
			return (Next(maxValue - minValue) + minValue);
		}

        public double NextDouble(){
            return (Next() / 32749.0);
        }
	}
}