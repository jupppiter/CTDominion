using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeagueSharp;
using LeagueSharp.Common;

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
            E = new Spell(SpellSlot.E, 165);
            R = new Spell(SpellSlot.R, 400);
        }

        public static void FightHard()
        {
            // Initialize spells
            var Target = TargetSelector.GetTarget(600, TargetSelector.DamageType.Magical);


            if (Target != null && Target.IsValid)
            {
                if (Q.IsReady() && Q.IsInRange(Target))
                {
                    Q.Cast(Target);
                }
                if (W.IsReady() && Player.Health < 150 && Player.CountEnemiesInRange(700) > 0)
                {
                    W.Cast();
                }
                if (E.IsReady() && Player.Distance(Target.Position) < 300)
                {
                    E.Cast();
                }
                if (R.IsReady())
                {
                    R.Cast();
                }
            }

            // Chase if not in range
            if (Target != null && Target.IsValid && 
                Player.Distance(Target.Position) > (Player.AttackRange + 50))
            {
                //Player.IssueOrder(GameObjectOrder.MoveTo, Target.Position);
                //Orb.SetOrbwalkingPoint(Target.Position);
                Orbwalking.Orbwalk(Target, Target.Position);
            }
        }

    }
}
