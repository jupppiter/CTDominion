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
    class PathWalker : Helper
    {
        public static List<Vector3> Positions = new List<Vector3>();

        public static void InitPoints()
        {
            Positions.Add(new Vector3(2827, 7775, -141));     // BLUE TOP
            Positions.Add(new Vector3(4561, 2945, -188));     // BLUE BOTTOM
            Positions.Add(new Vector3(9331, 2851, -147));     // RED BOTTOM
            Positions.Add(new Vector3(11095, 7705, -145));    // RED TOP
            Positions.Add(new Vector3(6935, 10641, -147));    // NEUTRAL
            Positions.Add(new Vector3(6935, 6471, -166));     // CENTER
        }

        /// <summary>
        /// Find the nearest Tower where there are enemies, to walk.
        /// </summary>
        public static void WalkAndFight()
        {
            List<Obj_AI_Hero> Enemies = ObjectHandler.Get<Obj_AI_Hero>().Enemies;

            int Index = -1;
            float Dist = 100000;
            for (int i = 0; i < Enemies.Count; i++)
            {
                if (Enemies[i] != null &&
                    !Enemies[i].IsDead &&
                    Enemies[i].IsVisible)
                {
                    float D = Player.Distance(Enemies[i].Position);
                    if (Dist > D)
                    {
                        Index = i;
                        Dist = D;
                    }
                }
            }// For

            if (Index != -1)
            {


                if (Player.Distance(Enemies[Index].Position) < 600)
                {
                    Console.WriteLine("ARRIVED");
                    //Plugins.Warwick.FightHard();
                    Base.GetCombo(Player.ChampionName);
                }
                else
                {
                    Console.WriteLine("WALKING_TO_HIM");
                    Player.IssueOrder(GameObjectOrder.MoveTo, Enemies[Index].Position);
                    Orb.SetOrbwalkingPoint(Enemies[Index].Position);
                }

            }
            else
            {
                Console.WriteLine("ERROR!");
                RandomWalk.Walk();
            }

        } // WalkAndFight()









        internal static bool HealUp()
        {
            if (Heroes.Player.IsDead)
            {
                Program.Orbwalker.ActiveMode = MyOrbwalker.OrbwalkingMode.None;
                return true;
            }

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
}
