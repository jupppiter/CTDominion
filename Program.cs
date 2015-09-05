using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeagueSharp;
using LeagueSharp.Common;
using SharpDX;
using System.Threading;

namespace CTDominion
{
    class Program : Helper
    {

        public static Menu Config;

        static void Main(string[] args)
        {
            // Events
            CustomEvents.Game.OnGameLoad += Game_OnGameLoad;
        }

        static void Game_OnGameLoad(EventArgs args)
        {
            Initiallizer.Init();

            // Menu
            Config = new Menu("Dominion", "Dominion", true);
            Config.AddSubMenu(new Menu("Orbwalker", "Orbwalking"));
            Orb = new Orbwalking.Orbwalker(Config.SubMenu("Orbwalking"));
            Config.AddToMainMenu();

            Game.OnUpdate += Game_OnUpdate;
        }

        static void Game_OnUpdate(EventArgs args)
        {
            SkillManager.AutoLevelUp();
            if (Player.IsDead || Player.Distance(TEAM_POS) < 500)
                BuyManager.Buy();


	    if (Player.IsDead && Items.HasItem(3345) && Items.CanUseItem(3345))
	    {	
		Items.UseItem(3345);
//		Game.PrintChat("Use Soul_Anchor_Trinket");
	    }	
                else
	    {
//		Game.PrintChat("wating...");
            }	

            if (!Player.IsDead)
            {

                if (Player.Health < 300 && Player.CountEnemiesInRange(1200) > 3)
                {
                    MoveBase();
                }
                else
                {
                    if (Player.CountEnemiesInRange(Range) < 3)
                    {
                        //LeagueSharp.Utils.DelayAction.Add(100, PathWalker.WalkAndFight);
                        LeagueSharp.Common.Utility.DelayAction.Add(500, PathWalker.WalkAndFight);
                    }
                    else
                    {
                        Range = MaxRange;

                        if (Player.CountEnemiesInRange(MaxRange) <= 1)
                            Range = 900;

                        LeagueSharp.Common.Utility.DelayAction.Add(1000, RandomWalk.Walk);
                    } 
                }

            }// Dead


        } //OnUpdate

        static int Range = 900;
        static int MaxRange = 1200;
        public static void MoveBase()
        {
            if (Player.Distance(TEAM_POS) > 100)
            {
                Player.IssueOrder(GameObjectOrder.MoveTo, TEAM_POS);
                Orb.SetOrbwalkingPoint(TEAM_POS);
            }

        } //MoveBase


    }
















public static class HealingBuffs
    {
        private static List<GameObject> _healingBuffs;
        private static int LastUpdate = 0;

        public static List<GameObject> AllyBuffs
        {
            get { return _healingBuffs.FindAll(hb => hb.IsValid && LeagueSharp.Common.Geometry.Distance(hb.Position, HeadQuarters.AllyHQ.Position) < 5400).OrderBy(buff => buff.Position.Distance(Heroes.Player.Position)).ToList(); }
        }

        public static List<GameObject> EnemyBuffs
        {
            get { return _healingBuffs.FindAll(hb => hb.IsValid && LeagueSharp.Common.Geometry.Distance(hb.Position, HeadQuarters.AllyHQ.Position) > 5400); }
        }

        public static void Load()
        {
            _healingBuffs = ObjectManager.Get<GameObject>().Where(h=>h.Name.Contains("healingBuff")).ToList();
            GameObject.OnCreate += OnCreate;
            GameObject.OnDelete += OnDelete;
            Game.OnUpdate += UpdateBuffs;
        }

        private static void UpdateBuffs(EventArgs args)
        {
            if (Environment.TickCount > LastUpdate + 1000)
            {
                foreach (var buff in _healingBuffs)
                {
                    if (Heroes.Player.ServerPosition.Distance(buff.Position) < 80) _healingBuffs.Remove(buff);
                }
                LastUpdate = Environment.TickCount;
            }
        }

        private static void OnCreate(GameObject sender, EventArgs args)
        {
            if (sender.Name.Contains("healingBuff"))
            {
                _healingBuffs.Add(sender);
            }
        }

        private static void OnDelete(GameObject sender, EventArgs args)
        {
            var iList = _healingBuffs.Where(buff => buff.NetworkId == sender.NetworkId);
            foreach (var i in iList)
            {
                _healingBuffs.Remove(i);
            }
        }
    }






























}
