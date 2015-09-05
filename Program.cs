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











                    else
                    {
	    if (Heroes.Player.HealthPercent >= 75) return false;

            var closestEnemyBuff = HealingBuffs.EnemyBuffs.FirstOrDefault(eb => eb.IsVisible && eb.IsValid && eb.Position.Distance(Heroes.Player.Position) < 800 && (eb.Position.CountEnemiesInRange(600) == 0 || eb.Position.CountEnemiesInRange(600) < eb.Position.CountAlliesInRange(600)));
            var closestAllyBuff = HealingBuffs.AllyBuffs.FirstOrDefault(ab => ab.IsVisible && ab.IsValid);


            //BUFF EXISTANCE CHECKS;
            if ((closestAllyBuff == null && closestEnemyBuff == null)) return false;

            //BECAUSE WE CHECKED THAT BUFFS CAN'T BE BOTH NULL; IF ONE OF THEM IS NULL IT MEANS THE OTHER ISN'T.
            // ReSharper disable once PossibleNullReferenceException
            var buffPos = closestEnemyBuff != null ? closestEnemyBuff.Position.Randomize(0, 15) : closestAllyBuff.Position.Randomize(0,15);

            if (Heroes.Player.Position.Distance(buffPos) <= 800 && (Heroes.Player.CountEnemiesInRange(800) == 0 || Heroes.Player.CountEnemiesInRange(800) < Heroes.Player.CountAlliesInRange(800)))
            {
                Program.Orbwalker.SetOrbwalkingPoint(buffPos);
                return true;
            }

            //stay in fight if you can't instantly gratify yourself and u don't really need the buff
            if (Heroes.Player.HealthPercent >= 45 && Heroes.Player.CountEnemiesInRange(900) <= Heroes.Player.CountAlliesInRange(900) && Heroes.Player.Distance(buffPos) > 1000) return false;

            //IF BUFFPOS IS VECTOR ZERO OR NOT VALID SOMETHING MUST HAVE GONE WRONG
            if (!buffPos.IsValid()) return false;

            //MOVE TO BUFFPOS
            Program.Orbwalker.SetOrbwalkingPoint(buffPos);

            //STOP EVERYTHING ELSE TO DO THIS
            return true;
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
