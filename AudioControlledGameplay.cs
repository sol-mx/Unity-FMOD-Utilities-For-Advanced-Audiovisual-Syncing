using FMOD.Studio;
using System.Collections.Generic;
using UnityEngine;

namespace FMODUnity.Custom
{
    /// <summary>
    /// This class is used to control gameplay events based on FMOD parameters. Create a subclass for each set of gameplay events (i.e. title screen).
    /// Use command instruments in FMOD to change the value of <see cref="ProgressParameterName"/> to trigger events.
    /// </summary>
    public abstract class AudioControlledGameplay : MonoBehaviour
    {
        [field: SerializeField] private bool autoPlay;

        [field: Header("Settings")]
        [field: SerializeField] public EventReference EventReference { get; private set; }
        [field: SerializeField] public string ProgressParameterName { get; private set; }

        [field: Header("Monitor")]
        [field: SerializeField] public int ProgressParameterValue { get; private set; }
        public Cue CurrentCue => Cues[ProgressParameterValue];

        // References
        protected EventInstance EventInstance { get; private set; }
        protected List<Cue> Cues { get; private set; } = new();

        public class Cue
        {
            public delegate void EventDelegate();
            public event EventDelegate Event;

            private bool invoked;

            public Cue(EventDelegate @event)
            {
                Event = @event;
            }

            public void Invoke()
            {
                if (invoked)
                {
                    return;
                }

                Event?.Invoke();
                invoked = true;
            }
        }

        // ============================================================================================================ SETUP

        /// <summary> Enter events in descending order. </summary>
        protected void BuildCueList(params Cue.EventDelegate[] events)
        {
            foreach (var @event in events)
            {
                Cues.Add(new Cue(@event));
            }
        }

        /// <summary> Enter cues in descending order. </summary>
        protected void BuildCueList(params Cue[] cues)
        {
            foreach (var cue in cues)
            {
                Cues.Add(cue);
            }
        }

        // ============================================================================================================ LOGIC

        protected virtual void Start()
        {
            if (EventInstance.isValid())
            {
                EventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                EventInstance.release();
            }

            EventInstance = RuntimeManager.CreateInstance(EventReference);

            if (autoPlay)
            {
                EventInstance.start();
            }
        }

        protected virtual void Update()
        {
            EventInstance.getParameterByName(ProgressParameterName, out _, out float progressValue);
            ProgressParameterValue = (int)progressValue - 1;

            InvokeCurrentCue();
        }

        private void InvokeCurrentCue()
        {
            if (Cues == null || ProgressParameterValue < 0 || ProgressParameterValue >= Cues.Count) return;

            CurrentCue?.Invoke();
        }
    }
}
