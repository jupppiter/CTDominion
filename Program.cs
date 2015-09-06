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

//                if (Player.Health < 300 && Player.CountEnemiesInRange(1200) > 3)
                if (Player.HealthPercent <= 30 || Player.ManaPercent <= 20 && Player.CountEnemiesInRange(1200) > 2)
                {
                    MoveBase();
//                    Game.PrintChat("Home!");
                }
                else
                {
//                    if (Player.CountEnemiesInRange(Range) < 3 && !Turrets.EnemyTurrets.Any(t => t.Distance(Player) < 950))
                    if (Player.CountEnemiesInRange(Range) < 3)
                    {
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



//jupppiter:
 public static class Minions
    {
        
        private static List<Obj_AI_Minion> _minions;

        public static List<Obj_AI_Minion> AllyMinions
        {
            get { return _minions.FindAll(t => t.IsValid<Obj_AI_Minion>() && !t.IsDead && t.IsAlly); }
        }
        public static List<Obj_AI_Minion> EnemyMinions
        {
            get { return _minions.FindAll(t => t.IsValid<Obj_AI_Minion>() && !t.IsDead && t.IsValidTarget()); }
        }

        public static void Load()
        {
            _minions = new List<Obj_AI_Minion>();
            Obj_AI_Minion.OnCreate += OnCreate;
            Obj_AI_Minion.OnDelete += OnDelete;
        }

        private static void OnDelete(GameObject sender, EventArgs args)
        {
            var iList = new List<Obj_AI_Minion>();
            foreach (var minion in _minions)
            {
                if (minion.NetworkId == sender.NetworkId) iList.Add(minion);
            }
            foreach (var i in iList)
            {
                _minions.Remove(i);
            }
        }

        private static void OnCreate(GameObject sender, EventArgs args)
        {
            var name = sender.Name.ToLower();
            if (sender.IsValid<Obj_AI_Minion>() && !name.Contains("sru_") && !name.Contains("ward") && !name.Contains("ttn") && !name.Contains("tt_") && !name.Contains("trinket") && !name.Contains("teemo") && sender.Team != GameObjectTeam.Neutral) _minions.Add((Obj_AI_Minion)sender);
        }

    }

    public static class Turrets
    {
        private static List<Obj_AI_Turret> _turrets;

        public static List<Obj_AI_Turret> AllyTurrets
        {
            get { return _turrets.FindAll(t => t.IsValid<Obj_AI_Turret>() && !t.IsDead && t.IsAlly && !t.Name.ToLower().Contains("shrine")); }
        }

        public static List<Obj_AI_Turret> EnemyTurrets
        {
            get { return _turrets.FindAll(t => t.IsValid<Obj_AI_Turret>() && !t.IsDead && t.IsEnemy && !t.Name.ToLower().Contains("shrine")); }
        }

        public static Obj_AI_Turret ClosestEnemyTurret
        {
            get { return EnemyTurrets.OrderBy(t => t.Distance(Player)).FirstOrDefault(); }
        }

        public static void Load()
        {
            _turrets = ObjectManager.Get<Obj_AI_Turret>().ToList();
            Obj_AI_Turret.OnCreate += OnCreate;
            Obj_AI_Turret.OnDelete += OnDelete;
        }

        private static void OnCreate(GameObject sender, EventArgs args)
        {
            if (sender.IsValid<Obj_AI_Turret>()) _turrets.Add((Obj_AI_Turret)sender);
        }

        private static void OnDelete(GameObject sender, EventArgs args)
        {
            var iList = _turrets.Where(turret => turret.NetworkId == sender.NetworkId);
            foreach (var i in iList)
            {
                _turrets.Remove(i);
            }
        }
    }
//jupppiter.


        static int Range = 900;
        static int MaxRange = 1200;
        public static void MoveBase()

        {
//            if (Player.Distance(TEAM_POS) > 100 && Player.CountEnemiesInRange(1500) < 1 && Minions.EnemyMinions.Any(m => m.Distance(Player) < 100))
            if (Player.Distance(TEAM_POS) > 100 && !Minions.EnemyMinions.Any(m => m.Distance(Player) < 100))
		// recall if no enemy near and minions
		Game.PrintChat("minions...");
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
