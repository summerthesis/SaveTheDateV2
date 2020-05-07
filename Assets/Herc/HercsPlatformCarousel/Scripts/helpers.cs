/**************************************************************
 * Time Interaction Helpers File
 * Author: Hercules (HErC) Dias Campos
 * Created on: May 5, 2020
 * Last Modified: May 5, 2020
 * 
 * Namespace TimeManipulation
 * 
 * Encapsulates time interactivity functionality. For
 * simplicity, include the namespace with the using directive
 * whenever you want to use these properties
 * 
 * This file contains the basics for time manipulation mechanics:
 *      -> An enum to indicate time states
 *      -> An interface to implement time-related behaviours
 *      -> A struct that returns speeds based on the object's
 *          current state
 * 
 * Such an approach offers a bit more flexibility and code
 * reuse possibilities to the time mechanics
 * 
 *************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;

namespace TimeManipuation {
    public enum TimeStates { Normal, Slowed, Stopped}
    public interface ITimeInteractable {
        void Slow();
        void Stop();
        void Restore();
    }

    public struct TimeVar : ITimeInteractable {
        #region Time State mechanics
        [SerializeField, Tooltip("Serialized for test purposes only")]
        private TimeStates m_CurrentState;
        [SerializeField, Tooltip("How long the object will remain under the time manipulation's effects")]
        private float m_EffectTime;
        public float CurrentEffectTime { get; private set; }
        #endregion

        #region Speeds
        [SerializeField] private float m_OriginalSpeed;
        [SerializeField] private float m_HalfSpeed;
        [SerializeField] private float m_StoppedSpeed;
        public float CurrentSpeed { get; private set; }
        #endregion
        public void Slow() {
            m_CurrentState = TimeStates.Slowed;
            CurrentSpeed = m_HalfSpeed;
        }
        public void Stop() {
            m_CurrentState = TimeStates.Stopped;
            CurrentSpeed = 0;
        }
        public void Reset() { /*reset object's timer*/ }
        public void Restore() { 
            m_CurrentState = TimeStates.Normal;
            CurrentSpeed = m_OriginalSpeed;
            Reset();
        }
    }
}