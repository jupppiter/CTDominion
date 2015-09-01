using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeagueSharp;
using LeagueSharp.Common;
using SharpDX;

namespace CTDominion.Plugins
{
    class Warwick : Helper
    {
        public static Spell Q { get; private set; }
        public static Spell W { get; private set; }
        public static Spell E { get; private set; }
        public static Spell R { get; private set; }

        public static void InitSpells()
        {
            Q = new Spell(SpellSlot.Q, 500);
            W = new Spell(SpellSlot.W, 600);
            E = new Spell(SpellSlot.E);
            R = new Spell(SpellSlot.R, 600);
        }

        public static void FightHard()
        {
            // Initialize spells
            var Target = TargetSelector.GetTarget(600, TargetSelector.DamageType.Magical);

            // Chase if not in range
            if (Target != null && Target.IsValid)
            {

                if (Target != null && W.IsReady())
                {
                    W.Cast();
                }

                if (Target != null && R.IsReady() && R.IsInRange(Target))
                {
                    R.Cast(Target);
                }

                if (Target != null && Q.IsReady() && Q.IsInRange(Target))
                {
                    Q.Cast(Target);
                }
            }
            else
            {
                Player.IssueOrder(GameObjectOrder.MoveTo, Target.Position);
                Orb.SetOrbwalkingPoint(Target.Position);
            }

        } //Fight

    }
}
