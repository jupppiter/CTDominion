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
    class Helper
    {
        public static Obj_AI_Hero Player { get { return ObjectManager.Player; } }
        public static Orbwalking.Orbwalker Orb = null;
        private static Vector3 BLUE_POS = new Vector3(560, 4133, -35);
        private static Vector3 RED_POS = new Vector3(13323, 4163, -37);

        public static Vector3 TEAM_POS
        {
            get
            {
                if (Player.Team == LeagueSharp.GameObjectTeam.Order)
                    return BLUE_POS;
                else
                    return RED_POS;
            }
        }

        public static double GetDistance(Vector3 P1, Vector3 P2)
        {
            return 10.0;
        }

        public static void WritePosition()
        {
            Console.WriteLine(Player.Position.X + " / " + Player.Position.Y + " / " + Player.Position.Z);
        }

        public static int GetAliveEnemiesClose(float Range)
        {
            List<Obj_AI_Hero> Temp = Player.GetEnemiesInRange(Range);
            int Count = 0;
            foreach (Obj_AI_Hero Hero in Temp)
            {
                if (!Hero.IsDead && Hero.IsVisible && Player.Distance(Hero.Position) < Range)
                {
                    Count++;
                }
            }
            return (Count);
        } //GetAliveEnemiesClose

        public static int GetAliveAlliesClose(float Range)
        {
            List<Obj_AI_Hero> Temp = Player.GetAlliesInRange(Range);
            int Count = 0;
            foreach (Obj_AI_Hero Hero in Temp)
            {
                if (!Hero.IsDead)
                    Count++;
            }
            return (Count);
        } //GetAliveAlliesClose

        public static Obj_AI_Hero GetClosestAliveEnemy()
        {
            List<Obj_AI_Hero> Enemies = Player.GetEnemiesInRange(1500);

            Obj_AI_Hero Temp = null;
            if (Enemies.Count > 0)
            {
                float Dist = Player.Distance(Enemies[0].Position);
                foreach (Obj_AI_Hero Enemy in Enemies)
                {
                    if (!Enemy.IsDead && 
                        Enemy.IsVisible &&
                        Dist > Player.Distance(Enemy.Position))
                    {
                        Dist = Player.Distance(Enemy.Position);
                        Temp = Enemy;
                    }
                }
            }
            return (Temp);
        } //GetClosestAliveEnemy

    }
}
