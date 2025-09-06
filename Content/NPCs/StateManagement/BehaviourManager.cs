using OmoriMod.Content.NPCs.Abstract;
using OmoriMod.Content.NPCs.StateManagement.NPCBehaviours;
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
        public NPCBehaviour SelectedBehaviour { get => selectedBehaviour; }
        private int selectedBehaviourIndex;
        private readonly Random random;

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

        private static bool PerformBehaviour(NPCBehaviour selectedBehaviour, OmoriNPC npc, BehaviourInfo info)
        {
            if (selectedBehaviour == null) { return true; }
            selectedBehaviour.PerformAI(npc, info);
            return selectedBehaviour.IsDone;
        }

        private void RandomSelector(bool init, OmoriNPC npc)
        {
            if(init) { selectedBehaviour ??= behaviourList[random.Next(behaviourList.Count)]; }
            else
            {
                selectedBehaviour.Reset();
                selectedBehaviour = behaviourList[random.Next(behaviourList.Count)];
            }
			selectedBehaviour.SetNPC(npc);
		}

        private void InOrderSelector(bool init, OmoriNPC npc)
        {
            if (init) { selectedBehaviour ??= behaviourList[selectedBehaviourIndex]; }
            else
            {
                selectedBehaviour.Reset();
                selectedBehaviourIndex = (selectedBehaviourIndex + 1) % behaviourList.Count;
                selectedBehaviour = behaviourList[selectedBehaviourIndex];
            }
			selectedBehaviour.SetNPC(npc);
		}

        private void ExitStatusSelector(bool init, OmoriNPC npc)
        {
            if (init) { selectedBehaviour ??= behaviourList[selectedBehaviourIndex]; }
            else
            {
                int exitStatus = selectedBehaviour.ExitStatus;
                selectedBehaviour.Reset();
                selectedBehaviour = behaviourList[exitStatus];
            }
			selectedBehaviour.SetNPC(npc);
		}


        private void PerformAI(OmoriNPC npc, Action<bool, OmoriNPC> selector, NPCBehaviour idleBehaviour)
        {
            selector?.Invoke(true, npc);
            BehaviourInfo info = selectedBehaviour.BehaviourInfo;

            if (PerformBehaviour(selectedBehaviour, npc, info) && !selectedBehaviour.JustCompleted)
            {
                if (timeBetweenBehaviours.TotalTicks > 0)
                {
                    timeBetweenBehaviours--;
                    PerformBehaviour(idleBehaviour, npc, info);
                }
                else
                {
                    selector?.Invoke(false, npc);
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
		public void PerformAIViaRandomBehaviour(OmoriNPC npc, NPCBehaviour idleBehaviour = null)
        {
            PerformAI(npc, RandomSelector, idleBehaviour);
        }

        public void PerformAIViaInOrderBehaviour(OmoriNPC npc, NPCBehaviour idleBehaviour = null)
        {
            PerformAI(npc, InOrderSelector, idleBehaviour);
        }

        public void PerformAIViaExitStatus(OmoriNPC npc, NPCBehaviour idleBehaviour = null)
        {
            PerformAI(npc, ExitStatusSelector, idleBehaviour);
        }

        public void PerformFindFrame(int frameHeight)
        {
            selectedBehaviour.PerformFindFrame(frameHeight);
		}
    }
}
