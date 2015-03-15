using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PvEOnline.Logic.Dungeons;
using PvEOnline.Logic.Units;
using Microsoft.Xna.Framework;
using PvEOnline.Logic;

namespace PvEOnline.AIs.Bosses
{
    public class Elemental : AI
    {
        private Vector2[] sprays; //N,E,S,W
        private EleStates estate;
        public Elemental(Unit u, Dungeon d, UnitManager uM, int seed) :base(u,d,uM,seed)
        {
            //getSprays();
            TimerHandler.AddTimer(unit.name, 100);
        }
        /*private void getSprays()
        {
            string dir ="NESW";
            for (int i = 0; i < 4; i++)
                sprays[i] = dun.MapSpecial.getSpecial("Spray" + dir[i]);
        }*/
        public override void Update()
        {
            //base enrage on the boss 
            if (TimerHandler.CheckTimer(unit.name))
            {
                List<Action> actions = new List<Action>();
                List<int> probs = new List<int>();
                actions.Add(MeleeAttack);
                probs.Add(8);
                switch (estate)
                {
                    case EleStates.Clear:
                        if (unit.hp < unit.cStats.maxhp * 0.8)
                        {
                            actions.Add(ChangeElem);
                            probs.Add(1);
                        }
                        actions.Add(ChangeTarget);
                        probs.Add(2);
                        break;
                    case EleStates.Fire:
                        break;
                    case EleStates.Ice:
                        break;
                    case EleStates.Light:
                        break;
                    case EleStates.Dark:
                        break;
                }
                actions[PickAction(probs, rnd)]();
            }
        }
        private void MeleeAttack()
        {
            Console.WriteLine("Meelee");
            TimerHandler.AddTimer(unit.name, 600);
        }
        private void ChangeElem()
        {
            Console.WriteLine("changeElem");
            unit.setDest(new Vector2(1000,1000));
            TimerHandler.AddTimer(unit.name, 5000);
            //changeSprays();
        }
        private void ChangeTarget()
        {
            //aggro from uMan.getHealer(); UP
            Console.WriteLine("ChangeTargeT");
            TimerHandler.AddTimer(unit.name, 5000);
        }
        private void SpellIce()
        {

        }
        /*private void changeSprays()
        {
            int[] order = {0,1,2,3};
            Elements[] eles = {Elements.Fire,Elements.Water,Elements.Dark,Elements.Light};
            Shuffle<int>(order,rnd);
            for(int i=0;i<order.Length;i++)
                dun.MapSpecial.ActivateSpray(sprays[order[i]], eles[i]);
        }*/
    }
    enum EleStates
    {
        Clear,
        Fire,
        Ice,
        Light,
        Dark
    }
}
