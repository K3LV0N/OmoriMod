using OmoriMod.Content.NPCs.Classes;
using OmoriMod.Content.NPCs.State_Management.Behaviour_Info;
using OmoriMod.Util;
using System;
using System.Collections.Generic;

namespace OmoriMod.Content.NPCs.State_Management
{
    public class BehaviourManager
    {
        private readonly List<NPCBehaviour> behaviourList;
        private readonly OmoriBehaviourNPC npc;
        private readonly BehaviourInfo behaviourInfo;
        private TickTimer timeBetweenBehaviours;

        private int selectedBehaviourIndex;
        private NPCBehaviour selectedBehaviour;
        public NPCBehaviour SelectedBehaviour { get => selectedBehaviour; }
        
        private readonly Random random;

        public BehaviourManager(OmoriBehaviourNPC npc, int totalFrames)
        {
            behaviourList = [];
            selectedBehaviour = null;
            selectedBehaviourIndex = 0;
            random = new Random();
            timeBetweenBehaviours = new TickTimer();
            behaviourInfo = new BehaviourInfo(totalFrames);
            this.npc = npc;
        }

        /// <summary>
        /// Adds a set of frames to <see cref="behaviourInfo"/> as an animation
        /// </summary>
        /// <param name="animationName"></param>
        /// <param name="iter"></param>
        /// <returns></returns>
        public bool AddAnimation(string name, FrameIterator iter)
        {
            return behaviourInfo.AddAnimation(name, iter);
        }

        public bool AddAnimation(string name, int beginIndex, int endIndex)
        {
            return AddAnimation(name, new FrameIterator(beginIndex, endIndex));
        }


        /// <summary>
        /// Selects the <paramref name="animationName"/> to run.
        /// </summary>
        /// <param name="animationName"></param>
        /// <returns></returns>
        public bool SelectAnimation(string animationName)
        {
            return behaviourInfo.SelectAnimation(animationName);
        }

        /// <summary>
        /// Adds a <see cref="NPCBehaviour"/> to this manager.
        /// </summary>
        /// <param name="behaviour"></param>
        public void AddBehaviour(NPCBehaviour behaviour)
        {
            behaviourList.Add(behaviour);
        }

        public void SetTimeBetweenBehaviours(TickTimer tickTimer)
        {
            timeBetweenBehaviours = tickTimer;
        }

        private void SelectNewBehaviour(int behaviourIndex)
        {
            selectedBehaviour.Reset();
            behaviourInfo.ResetExitStatus();
            selectedBehaviour = behaviourList[behaviourIndex];
        }




        private bool PerformBehaviour(NPCBehaviour selectedBehaviour, BehaviourInfo info)
        {
            if (selectedBehaviour == null) { return true; }
            selectedBehaviour.PerformAI(npc, info);
            return selectedBehaviour.IsDone;
        }

        private void RandomSelector(bool init)
        {
            if(init) { selectedBehaviour ??= behaviourList[random.Next(behaviourList.Count)]; }
            else
            {
                SelectNewBehaviour(random.Next(behaviourList.Count));
            }
		}

        private void InOrderSelector(bool init)
        {
            if (init) { selectedBehaviour ??= behaviourList[selectedBehaviourIndex]; }
            else
            {
                selectedBehaviourIndex = (selectedBehaviourIndex + 1) % behaviourList.Count;
                SelectNewBehaviour(selectedBehaviourIndex);
            }
		}

        private void ExitStatusSelector(bool init)
        {
            if (init) { selectedBehaviour ??= behaviourList[selectedBehaviourIndex]; }
            else
            {
                SelectNewBehaviour(behaviourInfo.ExitStatus);
            }
		}


        private void PerformAI(Action<bool> selector, NPCBehaviour backgroundBehaviour, NPCBehaviour idleBehaviour)
        {
            selector?.Invoke(true);

            PerformBehaviour(backgroundBehaviour, behaviourInfo);
            if (PerformBehaviour(selectedBehaviour, behaviourInfo))
            {
                if (timeBetweenBehaviours.TotalTicks > 0 && !selectedBehaviour.JustCompleted)
                {
                    timeBetweenBehaviours--;
                    PerformBehaviour(idleBehaviour, behaviourInfo);
                }
                else if (timeBetweenBehaviours.TotalTicks <= 0)
                {
                    selector?.Invoke(false);
                }
            }
        }

        /// <summary>
        /// Starts the behaviour manager. Randomly selects behaviours from <see cref="behaviourList"/> to run.
        /// If passed in, <paramref name="backgroundBehaviour"/> will be run each tick, and <paramref name="idleBehaviour"/> will run
        /// when the <see cref="selectedBehaviour"/> is on
        /// </summary>
        /// <param name="backgroundBehaviour">This <see cref="NPCBehaviour"/> will be run every tick, regardless of 
        /// whatever <see cref="NPCBehaviour"/> is selected Useful for buffs or for things that don't mess with 
        /// the <paramref name="npc"/> position or velocity.</param>
        /// <param name="idleBehaviour">This <see cref="NPCBehaviour"/> will be run whenever the 
        /// <see cref="timeBetweenBehaviours"/> hasn't run out. AKA, the time inbetween selected behaviours.</param>
		public void PerformAIViaRandomBehaviour(NPCBehaviour backgroundBehaviour = null, NPCBehaviour idleBehaviour = null)
        {
            PerformAI(RandomSelector, backgroundBehaviour, idleBehaviour);
        }

        /// <summary>
        /// Starts the behaviour manager. Selects behaviours from <see cref="behaviourList"/> in order to run.
        /// If passed in, <paramref name="backgroundBehaviour"/> will be run each tick, and <paramref name="idleBehaviour"/> will run
        /// when the <see cref="selectedBehaviour"/> is on
        /// </summary>
        /// <param name="backgroundBehaviour">This <see cref="NPCBehaviour"/> will be run every tick, regardless of 
        /// whatever <see cref="NPCBehaviour"/> is selected Useful for buffs or for things that don't mess with 
        /// the <paramref name="npc"/> position or velocity.</param>
        /// <param name="idleBehaviour">This <see cref="NPCBehaviour"/> will be run whenever the 
        /// <see cref="timeBetweenBehaviours"/> hasn't run out. AKA, the time inbetween selected behaviours.</param>
        public void PerformAIViaInOrderBehaviour(NPCBehaviour backgroundBehaviour = null, NPCBehaviour idleBehaviour = null)
        {
            PerformAI(InOrderSelector, backgroundBehaviour, idleBehaviour);
        }

        /// <summary>
        /// Starts the behaviour manager. Selects behaviours from <see cref="behaviourList"/> 
        /// based on the previous <see cref="SelectedBehaviour"/> exit status to run.
        /// If passed in, <paramref name="backgroundBehaviour"/> will be run each tick, and <paramref name="idleBehaviour"/> will run
        /// when the <see cref="selectedBehaviour"/> is on
        /// </summary>
        /// <param name="backgroundBehaviour">This <see cref="NPCBehaviour"/> will be run every tick, regardless of 
        /// whatever <see cref="NPCBehaviour"/> is selected Useful for buffs or for things that don't mess with 
        /// the <paramref name="npc"/> position or velocity.</param>
        /// <param name="idleBehaviour">This <see cref="NPCBehaviour"/> will be run whenever the 
        /// <see cref="timeBetweenBehaviours"/> hasn't run out. AKA, the time inbetween selected behaviours.</param>
        public void PerformAIViaExitStatus(NPCBehaviour backgroundBehaviour = null, NPCBehaviour idleBehaviour = null)
        {
            PerformAI(ExitStatusSelector, backgroundBehaviour, idleBehaviour);
        }

        /// <summary>
        /// Used to find what frame should be rendered for animation
        /// </summary>
        /// <param name="frameHeight"></param>
        public void PerformFindFrame(int frameHeight)
        {
            selectedBehaviour.PerformFindFrame(npc, behaviourInfo, frameHeight);
		}
    }
}
