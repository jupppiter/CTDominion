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
            bool AnySpellReady = Q.IsReady() || W.IsReady() || R.IsReady();

            if (!AnySpellReady)
            {
                Player.IssueOrder(GameObjectOrder.MoveTo, TEAM_POS);
                Orb.SetOrbwalkingPoint(TEAM_POS);
            }
            else if (Target != null && Target.IsValidTarget(550))
            {

                if (Q.IsReady() && Target.IsValidTarget(Q.Range))
                {
                    Q.Cast(Target);
                }
                if (W.IsReady())
                {
                    W.Cast();
                }
                if (E.IsReady() && Target.IsValidTarget(W.Range))
                {
                    E.Cast(Target.Position);
                }
                if (R.IsReady() && R.IsInRange(Target))
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
