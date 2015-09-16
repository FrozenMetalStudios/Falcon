/*******************************************************************
 * 
 * Copyright (C) 2015 Frozen Metal Studios - All Rights Reserved
 * 
 * NOTICE:  All information contained herein is, and remains
 * the property of Frozen Metal Studios. The intellectual and 
 * technical concepts contained herein are proprietary to 
 * Frozen Metal Studios are protected by copyright law.
 * Dissemination of this information or reproduction of this material
 * is strictly forbidden unless prior written permission is obtained
 * from Frozen Metal Studios.
 * 
 * *****************************************************************
 * 
 * Filename: PartitionedFlagState.cs
 * 
 * Description: Default Flag State storage.
 * 
 *******************************************************************/
using System.Collections;

namespace Assets.Scripts.Utility.FlagStates
{
    /// <summary>
    /// Base Class for Flag State Storage.
    /// </summary>
    abstract public class AbstractFlagState
    {
        /// <summary>
        /// Current State Storage.
        /// </summary>
        private ushort internalState = 0x0;

        /// <summary>
        /// Default Constructor.
        /// </summary>
        protected AbstractFlagState()
        {
            internalState = 0x0;
        }

        /// <summary>
        /// Initial State Constructor.
        /// </summary>
        /// <param name="initialState">Initial State</param>
        protected AbstractFlagState(ushort initialState)
        {
            // Sets the state to the initial values.
            internalState = initialState;
        }

        /// <summary>
        /// Returns the Internal State.
        /// </summary>
        public ushort GetState()
        {
            return internalState;
        }

        /// <summary>
        /// Sets a combination of Flags.
        /// </summary>
        /// <param name="stateFlag">State Flag Mask</param>
        protected void SetInternalFlags(ushort stateFlag)
        {
            // Sets the Flags specified by the Mask
            internalState |= stateFlag;
        }

        /// <summary>
        /// Clears a combination of Flags.
        /// </summary>
        /// <param name="stateFlag">State Flag Mask</param>
        protected void ClearInternalFlags(ushort stateFlag)
        {
            // Clears the Flags specified by the Mask
            internalState &= (ushort) ~(stateFlag);
        }

        /// <summary>
        /// Sets a new State.
        /// </summary>
        /// <param name="newState">New State</param>
        protected void SetInternalState(ushort newState)
        {
            internalState = newState;
        }

        /// <summary>
        /// Zeros the state storage.
        /// </summary>
        protected void ClearInternalState()
        {
            internalState = 0x0;
        }

        /// <summary>
        /// Allows Generic Clearing of a State.
        /// </summary>
        public void SetToClear()
        {
            ClearInternalState();
        }

        /// <summary>
        /// Needs to Set the State value.
        /// </summary>
        /// <param name="stateFlag">State Flag</param>
        abstract public void SetFlag(ushort stateFlag);

        /// <summary>
        /// Needs to Clear the State value.
        /// </summary>
        /// <param name="stateFlag">State Flag</param>
        abstract public void ClearFlag(ushort stateFlag = 0x0);
    }
}
