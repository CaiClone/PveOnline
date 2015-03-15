using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace PvEOnline.Logic
{
    //XNA doesn't seem to like async
    public class TimerHandler :GameComponent
    {
        private static Dictionary<string,uint> currentTimers;
        private static uint time; //Will break if someone leaves their game running for 497 days
        private float sTime;
        public TimerHandler(Game1 game) : base(game) {
            sTime = 0;
            time = 0;
            currentTimers = new Dictionary<string, uint>();
        }
        /// <summary>
        /// Only has preccison up to 10ms
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="ms"></param>
        public static void AddTimer(string Name, uint ms){
            try
            {
                currentTimers.Add(Name, time + (uint)(ms / 10));
            }
            catch (ArgumentException e)
            {
                Console.WriteLine("Timer",Name,"Already initialized");
            }
        }
        public static bool CheckTimer(string Name, bool remove=true)
        {
            uint dTime;
            bool res= currentTimers.TryGetValue(Name, out dTime) && dTime < time;
            if (res&&remove) 
                currentTimers.Remove(Name);
            return res;
        }
        public override void  Update(GameTime gameTime)
        {
            sTime += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            while (sTime > 10f)
            {
                sTime -= 10f;
                time++;
            }
            base.Update(gameTime);
        }

        public static void RemoveTimer(string name)
        {
            currentTimers.Remove(name);
        }
    }
}
