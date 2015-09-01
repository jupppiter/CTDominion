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
    }
}
