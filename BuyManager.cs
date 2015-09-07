using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeagueSharp;
using LeagueSharp.Common;
using SharpDX;

namespace CTDominion
{
    class BuyManager : Helper
    {
        static List<CItem> ItemsToBuy = new List<CItem>();
        static int Index = 0;
        public static void InitItems()
        {
            ItemsToBuy.Clear();
            switch (Player.ChampionName)
            {
                case "Annie":
                    ItemsToBuy.Add(new CItem(3020, 1100, false)); // Sorcer Bots [1]
                    ItemsToBuy.Add(new CItem(1058, 1250, false)); // Staf1250
                    ItemsToBuy.Add(new CItem(3090, 2250, false)); // Wooglets (Hat)[2]
                    ItemsToBuy.Add(new CItem(3029, 2790, false)); // ROD [3]
                    ItemsToBuy.Add(new CItem(3135, 2500, false)); // Void Staff [4]
                    ItemsToBuy.Add(new CItem(3165, 2300, false)); // Morello [5]
                    ItemsToBuy.Add(new CItem(3116, 3000, false)); // Rylais [6]
                    break;
                case "Ryze":
                    ItemsToBuy.Add(new CItem(3020, 1100, false)); // Sorcer Bots [1]
//                    ItemsToBuy.Add(new CItem(3070, 720, false)); // Tear of the Goddess [2]
                    ItemsToBuy.Add(new CItem(3003, 3000, false)); // Archangel's Staff [3]
                    ItemsToBuy.Add(new CItem(3090, 3500, false)); // Wooglet's Witchcap [4]
//                    ItemsToBuy.Add(new CItem(3135, 2500, false)); // Void Staff [5]
                    ItemsToBuy.Add(new CItem(3165, 2300, false)); // Morello [6]
                    break;
                case "Garen":
                    ItemsToBuy.Add(new CItem(3047, 1000, false));  // Ninja Tabi[1]
                    ItemsToBuy.Add(new CItem(3071, 3000, false)); // The Black Cleaver[2]
                    ItemsToBuy.Add(new CItem(3084, 2500, false)); // Overlord's Bloodmail[3]
                    ItemsToBuy.Add(new CItem(3181, 2275, false)); // Sanguine Blade[4]
                    ItemsToBuy.Add(new CItem(3035, 2300, false)); // Last Whisper[5]
                    ItemsToBuy.Add(new CItem(3022, 3300, false)); // Frozen Mallets[6]
                    break;
                case "Warwick":
                    ItemsToBuy.Add(new CItem(3047, 1000, false));  // Ninja Tabi[1]
                    ItemsToBuy.Add(new CItem(1043, 1100, false)); // Recurve Bow
                    ItemsToBuy.Add(new CItem(3091, 1500, false)); // Wit's End[2]
                    ItemsToBuy.Add(new CItem(3153, 3000, false)); // BOTKR[3]
                    ItemsToBuy.Add(new CItem(3022, 3300, false)); // Frozen Mallets[4]
                    ItemsToBuy.Add(new CItem(3110, 2450, false)); // Frozen Heart[5]
                    ItemsToBuy.Add(new CItem(3065, 1000, false)); // Spirit Visage
                    break;
                case "MasterYi":
                    ItemsToBuy.Add(new CItem(3006, 1000, false));  // Berseker's Boots[1]
                    ItemsToBuy.Add(new CItem(3134, 1337, false)); // Brutalizer
                    ItemsToBuy.Add(new CItem(3142, 1363, false)); // Yoummu [2]
                    ItemsToBuy.Add(new CItem(1043, 1100, false)); // Bow
                    ItemsToBuy.Add(new CItem(3153, 2100, false)); // BOTKR [3]
                    ItemsToBuy.Add(new CItem(3104, 3800, false)); // Infinity [4]
                    ItemsToBuy.Add(new CItem(3035, 2300, false)); // Last Whisper [5]
                    ItemsToBuy.Add(new CItem(3046, 2800, false)); // Phantom Dancer [6]
                    break;
                default:
                    ItemsToBuy.Add(new CItem(3073, 720, false));
                    ItemsToBuy.Add(new CItem(3020, 1100, false));
                    ItemsToBuy.Add(new CItem(3007, 2280, false));
                    ItemsToBuy.Add(new CItem(3029, 2790, false));
                    ItemsToBuy.Add(new CItem(3110, 2450, false));
                    ItemsToBuy.Add(new CItem(3135, 2500, false));
                    ItemsToBuy.Add(new CItem(3090, 3500, false));
                    break;

            }
        }

        public static void Buy()
        {

            for(int i=0; i < ItemsToBuy.Count; i++)
            {
                if (!ItemsToBuy[i].Bought && 
                    Player.Gold > ItemsToBuy[i].Gold &&
                    !Items.HasItem(ItemsToBuy[i].ID))
                {
                    Items.Item Item = new Items.Item(ItemsToBuy[i].ID);
                    Item.Buy();
                    ItemsToBuy[i].Bought = true;
                }
            }

        }

        class CItem
        {
            public int ID;
            public int Gold;
            public bool Bought;

            public CItem(int id, int gold, bool bought)
            {
                this.ID = id;
                this.Gold = gold;
                this.Bought = bought;
            }
        }//CItem
    }
}
