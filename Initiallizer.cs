using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CTDominion.Plugins;

namespace CTDominion
{
    class Initiallizer
    {
        public static void Init()
        {
            RandomWalk.Init();
            BuyManager.InitItems();
            Ryze.InitSpells();
            Warwick.InitSpells();
            MasterYi.InitSpells();
            Ahri.InitSpells();
            Annie.InitSpells();
        }
    }
}
