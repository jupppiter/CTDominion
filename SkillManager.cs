using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeagueSharp;
using LeagueSharp.Common;
using SharpDX;
using CTDominion;

namespace CTDominion
{
    class SkillManager : Helper
    {
        public static int[] Spells = new int[] { 1, 2, 3, 1, 1, 4, 1, 2, 1, 2, 4, 3, 2, 2, 3, 4, 3, 3 };
        public static void AutoLevelUp()
        {
            int qL = Player.Spellbook.GetSpell(SpellSlot.Q).Level;
            int wL = Player.Spellbook.GetSpell(SpellSlot.W).Level;
            int eL = Player.Spellbook.GetSpell(SpellSlot.E).Level;
            int rL = Player.Spellbook.GetSpell(SpellSlot.R).Level;
            if (qL + wL + eL + rL < ObjectManager.Player.Level)
            {
                int[] level = { 0, 0, 0, 0 };
                for (int i = 0; i < ObjectManager.Player.Level; i++)
                {
                    level[Spells[i] - 1] = level[Spells[i] - 1] + 1;
                }
                if (qL < level[0]) ObjectManager.Player.Spellbook.LevelSpell(SpellSlot.Q);
                if (wL < level[1]) ObjectManager.Player.Spellbook.LevelSpell(SpellSlot.W);
                if (eL < level[2]) ObjectManager.Player.Spellbook.LevelSpell(SpellSlot.E);
                if (rL < level[3]) ObjectManager.Player.Spellbook.LevelSpell(SpellSlot.R);

            }
        }// AutoLevelUp

    }
}
