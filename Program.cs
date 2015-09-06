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

 public static class HeadQuarters
    {
        public static Obj_HQ AllyHQ
        {
            get { return ObjectManager.Get<Obj_HQ>().FirstOrDefault(hq => hq.IsAlly); }
        }
        public static Obj_HQ EnemyHQ
        {
            get { return ObjectManager.Get<Obj_HQ>().FirstOrDefault(hq => hq.IsEnemy); }
        }
    }

public static class Heroes
    {
        private static List<Obj_AI_Hero> _heroes;

        public static Obj_AI_Hero Player = ObjectManager.Player;

        /// <summary>
        /// Ally Heroes, excluding the player
        /// </summary>
        public static List<Obj_AI_Hero> AllyHeroes
        {
            get { return _heroes.FindAll(h => h.IsValid<Obj_AI_Hero>() && h.IsAlly); }
        }

        public static List<Obj_AI_Hero> EnemyHeroes
        {
            get { return _heroes.FindAll(h => h.IsValid<Obj_AI_Hero>() && h.IsEnemy); }
        }

        public static void Load()
        {
            Player = ObjectManager.Player;
            _heroes = ObjectManager.Get<Obj_AI_Hero>().Where(h=>!h.IsMe).ToList();
        }
    }

//jupppiter.


        static int Range = 900;
        static int MaxRange = 1200;
        public static void MoveBase()

        {
//            if (Player.Distance(TEAM_POS) > 100 && Player.CountEnemiesInRange(1500) < 1 && Minions.EnemyMinions.Any(m => m.Distance(Player) < 100))
//            if (Player.Distance(TEAM_POS) > 100 && Player.CountEnemiesInRange(1500) == 0)
		// recall if no enemy near and minions
//            {
//           	Player.Spellbook.CastSpell(SpellSlot.Recall);
//            }
//            else
//            {

//                Player.IssueOrder(GameObjectOrder.MoveTo, TEAM_POS);
//                Orb.SetOrbwalkingPoint(TEAM_POS);

//if (Heroes.Player.Position.Distance(buffPos) <= 800 && (Heroes.Player.CountEnemiesInRange(800) == 0 || Heroes.Player.CountEnemiesInRange(800) < Heroes.Player.CountAlliesInRange(800)))
//            {

 var closestEnemyBuff = HealingBuffs.EnemyBuffs.FirstOrDefault(eb => eb.IsVisible && eb.IsValid && eb.Position.Distance(Heroes.Player.Position) < 800 && (eb.Position.CountEnemiesInRange(600) == 0 || eb.Position.CountEnemiesInRange(600) < eb.Position.CountAlliesInRange(600)));
 var closestAllyBuff = HealingBuffs.AllyBuffs.FirstOrDefault(ab => ab.IsVisible && ab.IsValid);
                Orb.SetOrbwalkingPoint(buffPos);
//                return true;
//            }
//            }

        } //MoveBase


    }

}
