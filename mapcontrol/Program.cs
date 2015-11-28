using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using Ensage;
using SharpDX;

namespace Antiward
{
        internal class Program
    {
        private static float skalaX;
        private static float skalaY;

        private static ObjectCache cache = MemoryCache.Default;
        private static CacheItemPolicy policy = new CacheItemPolicy();


        private static void Main(String[] args)



        {
            skalaX = ((float)Drawing.Width / 1366);
            skalaY = ((float)Drawing.Height / 768);

            Game.OnFireEvent += Game_OnFireEvent;
        }

        private static void Game_OnFireEvent(FireEventEventArgs args)
        {
            var mereka = ObjectMgr.GetEntities<Hero>().Where(x => x.Team == ObjectMgr.LocalHero.Team && x.IsAlive).ToList();
            if(args.GameEvent.Name=="dota_inventory_changed")
            {
                foreach (var dia in mereka)
                {   
                    var obs = dia.Inventory.Items.Where(x => x.Name == "item_ward_observer").ToList();
                    var sentry = dia.Inventory.Items.Where(x => x.Name == "item_ward_sentry").ToList();
                    var obsentry = dia.Inventory.Items.Where(x => x.Name == "item_ward_dispenser").ToList();
  
                    if (obs.Any())
                    {
                        foreach (var ob in obs)
                        {
                            if (ob.AbilityState == AbilityState.OnCooldown)
                            {
                                Game.ExecuteCommand("say_team " + dia.Name.Replace("npc_dota_hero_", "") + " barusan masang observer");
                                dia.AddParticleEffect(@"particles\ui_mouseactions\range_display.vpcf").SetControlPoint(1, new Vector3(128, 0, 0));

                            }
                        }
                    }
                    if (sentry.Any())
                    {
                        foreach (var sent in sentry)
                        {
                            if (sent.AbilityState == AbilityState.OnCooldown)
                            {
                                Game.ExecuteCommand("say_team " + dia.Name.Replace("npc_dota_hero_", "") + " barusan masang sentry");
                                dia.AddParticleEffect(@"particles\ui_mouseactions\range_display.vpcf").SetControlPoint(1, new Vector3(100, 0, 0));
                            }
                        }
                    }
                    if (obsentry.Any())
                    {
                        foreach (var obse in obsentry)
                        {
                            if (obse.AbilityState == AbilityState.OnCooldown)
                            {
                                Game.ExecuteCommand("say_team " + dia.Name.Replace("npc_dota_hero_", "") + " barusan masang obs/sentry");
                                dia.AddParticleEffect(@"particles\ui_mouseactions\range_display.vpcf").SetControlPoint(1, new Vector3(128, 0, 0));
                            }
                        }
                    }

                }
            }
        }
    }
} 