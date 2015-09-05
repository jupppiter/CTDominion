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


	    if (Player.IsDead)
                    {
		Player.Spellbook.CastSpell(SpellSlot.Recall);
                    }

//            if (Player.IsDead)
//		Game.PrintChat("I'm Alive!");


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
}
