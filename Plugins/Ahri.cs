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
    class Ahri : Helper
    {
        public static Spell Q { get; private set; }
        public static Spell W { get; private set; }
        public static Spell E { get; private set; }
        public static Spell R { get; private set; }

        public static void InitSpells()
        {
            Q = new Spell(SpellSlot.Q, 600);
            W = new Spell(SpellSlot.W);
            E = new Spell(SpellSlot.E, 600);
            R = new Spell(SpellSlot.R);
        }

        public static void FightHard()
        {
            // Initialize spells
            var Target = TargetSelector.GetTarget(600, TargetSelector.DamageType.Magical);

            // Chase if not in range
            if (Target != null && Target.IsValid && Player.Distance(Target.Position) < 600)
            {
                var pr = Q.GetPrediction(Target);
                if (Q.IsReady() && Q.IsInRange(Target))
                {
                    Q.Cast(Target.Position);
                }

                if (E.IsReady() && E.IsInRange(Target))
                {
                    E.Cast(Target.Position);
                }
            }
            else if (Target != null && Target.IsValid && Player.Distance(Target.Position) > 620 && Player.Distance(Target.Position) < 2000)
            {
                Player.IssueOrder(GameObjectOrder.MoveTo, Target.Position);
                Orb.SetOrbwalkingPoint(Target.Position);
            }

        } //Fight

    }
}
