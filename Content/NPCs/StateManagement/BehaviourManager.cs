using OmoriMod.Content.NPCs.Abstract;
using OmoriMod.Util;
using System;
using System.Collections.Generic;
using Terraria;

namespace OmoriMod.Content.NPCs.StateManagement
{
    public class BehaviourManager
    {
        private readonly List<NPCBehaviour> behaviourList;
        private TickTimer timeBetweenBehaviours;
        private NPCBehaviour selectedBehaviour;
        private int selectedBehaviourIndex;
        private readonly Random random;

        public String SelectedBehaviour { get => selectedBehaviour.BehaviourName; }

        public BehaviourManager()
        {
            behaviourList = [];
            selectedBehaviour = null;
            selectedBehaviourIndex = 0;
            random = new Random();
            timeBetweenBehaviours = new TickTimer();
        }

        public void AddBehaviour(NPCBehaviour behaviour) {
            behaviourList.Add(behaviour);
        }

        public void SetTickTimer(TickTimer tickTimer)
        {
            timeBetweenBehaviours = tickTimer;
        }

        private static bool PerformBehaviour(NPCBehaviour selectedBehaviour, OmoriNPC npc)
        {
            if (selectedBehaviour == null) { return true; }
            selectedBehaviour.PerformAI(npc);
            return selectedBehaviour.IsDone;
        }

        private void RandomSelector(bool init)
        {
            if(init) { selectedBehaviour ??= behaviourList[random.Next(behaviourList.Count)]; }
            else
            {
                selectedBehaviour.Reset();
                selectedBehaviour = behaviourList[random.Next(behaviourList.Count)];
            } 
        }

        private void InOrderSelector(bool init)
        {
            if (init) { selectedBehaviour ??= behaviourList[selectedBehaviourIndex]; }
            else
            {
                selectedBehaviour.Reset();
                selectedBehaviourIndex = (selectedBehaviourIndex + 1) % behaviourList.Count;
                selectedBehaviour = behaviourList[selectedBehaviourIndex];
            }
        }

        private void ExitStatusSelector(bool init)
        {
            if (init) { selectedBehaviour ??= behaviourList[selectedBehaviourIndex]; }
            else
            {
                int exitStatus = selectedBehaviour.ExitStatus;
                selectedBehaviour.Reset();
                selectedBehaviour = behaviourList[exitStatus];
            }
        }


        private void Perform(OmoriNPC npc, Action<bool> selector, NPCBehaviour idleBehaviour = null)
        {
            selector?.Invoke(true);

            if (PerformBehaviour(selectedBehaviour, npc) && !selectedBehaviour.JustCompleted)
            {
                if (timeBetweenBehaviours.TotalTicks > 0)
                {
                    timeBetweenBehaviours--;
                    PerformBehaviour(idleBehaviour, npc);
                }
                else
                {
                    selector?.Invoke(false);
                }
            }
        }

        

        /// <summary>
        /// Selects one behaviour at random from <see cref="behaviourList"/> and performs it.
        /// Optionally takes in a <see cref="NPCBehaviour"/> to implement
        /// an idle behaviour between behaviours.
        /// </summary>
        /// <param name="timeBetweenBehaviours">The amount of time that should pass between behaviors.</param>
        /// <param name="idleBehaviour">The idle behvaiour chosen during <paramref name="timeBetweenBehaviours"/></param>
        public void PerformViaRandomBehaviour(OmoriNPC npc, NPCBehaviour idleBehaviour = null)
        {
            Perform(npc, RandomSelector, idleBehaviour);
        }

        public void PerformViaInOrderBehaviour(OmoriNPC npc, NPCBehaviour idleBehaviour = null)
        {
            Perform(npc, InOrderSelector, idleBehaviour);
        }

        public void PerformViaExitStatus(OmoriNPC npc, NPCBehaviour idleBehaviour = null)
        {
            Perform(npc, ExitStatusSelector, idleBehaviour);
        }
    }
}
