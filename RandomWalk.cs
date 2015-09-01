using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeagueSharp;
using LeagueSharp.Common;
using CTDominion;

namespace CTDominion
{
    class RandomWalk : Helper
    {
        // Vars
        static Random R = new Random();
        static int Index = 0;
        static int LastIndex = -1;
        static bool Walking = false;

        // Positions
        static List<Vector3> Points = new List<Vector3>();
        
        /// <summary>
        /// Initit the points to walk arround
        /// </summary>
        public static void Init()
        {
            // General (All) Points to walk
            Points.Add(new Vector3(6949, 6413, -166));
            Points.Add(new Vector3(2721, 7757, -141));
            Points.Add(new Vector3(6865, 11173, -147));
            Points.Add(new Vector3(11147, 7723, -145));
            Points.Add(new Vector3(9427, 2711, -142));
            Points.Add(new Vector3(4475, 2671, -145));
            Points.Add(new Vector3(8529, 7173, -188));
            Points.Add(new Vector3(5670, 5275, -188));
        }

        /// <summary>
        /// Implement the auto-walking to random points
        /// </summary>
        public static void Walk()
        {
            Vector3 Pos = Points[Index];

            if (!Walking)
            {
                Walking = true;
                Index = R.Next(0, Points.Count - 1);

                // Prevent Circular
                if (LastIndex != -1 && LastIndex == Index)
                {
                    if (Index < Points.Count - 1) Index++;
                    else Index--;
                }
                else LastIndex = Index;
            }
            else
            {
                Walking = Player.Distance(Pos) > 100 ? true : false;
                if (Walking)
                {
                    Player.IssueOrder(GameObjectOrder.MoveTo, Pos);
                    Orb.SetOrbwalkingPoint(Pos);
                }
            }

        } //Walk


    }
}
