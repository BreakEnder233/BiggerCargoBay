using Newtonsoft.Json;
using PeterHan.PLib;
using PeterHan.PLib.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BiggerCargoBay
{
    [JsonObject(MemberSerialization.OptIn)]
    public class BiggerCargoBayConfiguration : POptions.SingletonOptions<BiggerCargoBayConfiguration>
    {
        [Option("CargoBayCapacity", "How many kgs the solid resource could a cargo bay bring.")]
        [Limit(1000, 100000)]
        [JsonProperty]
        public int CargoBayCapacity { get; set; }

        [Option("LiquidCargoBayCapacity", "How many kgs the solid resource could a cargo bay bring.")]
        [Limit(1000, 100000)]
        [JsonProperty]
        public int LiquidCargoBayCapacity { get; set; }

        [Option("GasCargoBayCapacity", "How many kgs the solid resource could a cargo bay bring.")]
        [Limit(1000, 100000)]
        [JsonProperty]
        public int GasCargoBayCapacity { get; set; }

        [Option("Always Reset Planet Mass", "Reset Planet Mass after mission complete")]
        [JsonProperty]
        public bool ResetPlanetMass { get; set; }


        public BiggerCargoBayConfiguration()
        {
            CargoBayCapacity = 10000;
            LiquidCargoBayCapacity = 5000;
            GasCargoBayCapacity = 2000;
            ResetPlanetMass = false;
        }
    }
}
