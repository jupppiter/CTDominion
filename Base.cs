using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CTDominion.Plugins;

namespace CTDominion
{
    class Base
    {
        public static void GetCombo(string championName)
        {
            switch (championName)
            {
                case "Ryze":
                    Ryze.FightHard();
                    break;
                case "Ahri":
                    Ahri.FightHard();
                    break;
                case "Warwick":
                    Warwick.FightHard();
                    break;
                case "MasterYi":
                    MasterYi.FightHard();
                    break;
                case "Annie":
                    Annie.FightHard();
                    break;
            }

        }//GetCombo
    }
}
