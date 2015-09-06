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
    class Garen : Helper
    {
        public static Spell Q { get; private set; }
        public static Spell W { get; private set; }
        public static Spell E { get; private set; }
        public static Spell R { get; private set; }

        public static void InitSpells()
        {
            Q = new Spell(SpellSlot.Q);
            W = new Spell(SpellSlot.W);
            E = new Spell(SpellSlot.E, 325);
            R = new Spell(SpellSlot.R, 400);
        }

        public static void FightHard()
        {
            // Initialize spells
            var Target = TargetSelector.GetTarget(600, TargetSelector.DamageType.Magical);

            // Chase if not in range
            if (Target != null && Target.IsValid && Player.Distance(Target.Position) < 400)
            {

                if (Q.IsReady() && Target.IsValidTarget(Q.Range))
                {
                    Q.Cast(Target);
                }
                if (W.IsReady())
                {
                    W.Cast();
                }
                if (E.IsReady() && Target.IsValidTarget(E.Range))
                {
                    E.Cast(Target.Position);
                }
                if (R.IsReady() && Target.IsValidTarget(R.Range))
                {
                    R.Cast(Target);
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
