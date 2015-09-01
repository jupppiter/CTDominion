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
    class Annie : Helper
    {
        public static Spell Q { get; private set; }
        public static Spell W { get; private set; }
        public static Spell E { get; private set; }
        public static Spell R { get; private set; }

        public static void InitSpells()
        {
            Q = new Spell(SpellSlot.Q, 625);
            W = new Spell(SpellSlot.W, 550);
            E = new Spell(SpellSlot.E);
            R = new Spell(SpellSlot.R, 600);
            Q.SetTargetted(0.25f, 1400f);
            W.SetSkillshot(0.5f, 250f, float.MaxValue, false, SkillshotType.SkillshotCone);
            R.SetSkillshot(0.2f, 250f, float.MaxValue, false, SkillshotType.SkillshotCircle);
        }

        static float ReEnterRange = 1200;
        static float CurrentRange = 550;
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
                if (E.IsReady())
                {
                    E.Cast();
                }
                if (W.IsReady() && Target.IsValidTarget(W.Range))
                {
                    W.Cast(Target.Position);
                }
                if (R.IsReady() && Target.IsValidTarget(R.Range))
                {
                    R.Cast(Target.Position);
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
