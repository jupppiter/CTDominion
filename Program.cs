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

        public static List<Obj_AI_Turret> AllTurrets;
        public static List<Obj_AI_Turret> AllyTurrets;
        public static List<Obj_AI_Turret> EnemyTurrets;

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

//                if (Player.Health < 300 && Player.CountEnemiesInRange(1200) > 3)
                if (Player.HealthPercent <= 30 || Player.ManaPercent <= 20 && Player.CountEnemiesInRange(1200) > 2)
                {
                    MoveBase();
//                    Game.PrintChat("Home!");
                }
                else
                {
                    if (Player.CountEnemiesInRange(Range) < 3 && Turrets.ClosestEnemyTurret.Distance(Player) < 950)
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
            if (Player.Distance(TEAM_POS) > 100 && Player.CountEnemiesInRange(1200) < 1)
//&& !Minions.EnemyMinions.Any(m => m.Distance(Heroes.Player) < 950)
            {
           	Player.Spellbook.CastSpell(SpellSlot.Recall);
            }
            else
            {
                Player.IssueOrder(GameObjectOrder.MoveTo, TEAM_POS);
                Orb.SetOrbwalkingPoint(TEAM_POS);
            }

        } //MoveBase


    }

}
