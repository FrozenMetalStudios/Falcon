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
 * Filename: UniqueFlagState.cs
 * 
 * Description: Flag State storage.
 * 
 *******************************************************************/
using UnityEngine;
using System.Collections;

namespace Assets.Scripts.Utility.FlagStates
{
    /// <summary>
    /// Stores Unique Flag States where a 1 bit can only be set in
    /// a series of bits.
    /// </summary>
    class UniqueFlagState : AbstractFlagState
    {
        /// <summary>
        /// Sets the Flag to an Invalid State
        /// </summary>
        static public ushort INVALID_STATE = 0x8000;

        /// <summary>
        /// Default Constructor.
        /// </summary>
        public UniqueFlagState() : base()
        {
            // Sets to Default zero state.
        }

        /// <summary>
        /// Initial Unique State Constructor.
        /// </summary>
        /// <param name="initialState"></param>
        public UniqueFlagState(ushort initialState)
        {
            // The state must represent a Unique value
            if (EnsureUniqueState(initialState))
            {
                SetInternalState(initialState);
            }
            else
            {
                Debug.LogError("Unique Flag given non-unique value");
                SetInternalState(INVALID_STATE);
            }
        }

        /// <summary>
        /// Replaces the Existing Flag with the new Flag.
        /// </summary>
        /// <param name="flagState">New Flag State</param>
        override public void SetFlag(ushort flagState)
        {
            // The new state must be a Unique State
            if (EnsureUniqueState(flagState))
            {
                SetInternalState(flagState);
            }
            else
            {
                SetInternalState(INVALID_STATE);
            }
        }

        /// <summary>
        /// Removes the Last Flag and replaces with the Zero Flag.
        /// </summary>
        /// <param name="flagState">Unused Parameter</param>
        override public void ClearFlag(ushort flagState = 0x0)
        {
            ClearInternalState();
        }

        /// <summary>
        /// Ensure the Flag represents a Unique state.
        /// </summary>
        /// <param name="state">The State Flag to validate</param>
        /// <returns>True if the Flag only has a Single 1, false otherwise</returns>
        private bool EnsureUniqueState(ushort state)
        {
            // Store the 1's count
            int oneCount = 0 ;

            // Shift out each bit and check the value
            for (int i = 0; i < 16; i++)
            {
                // Determine what shifts out
                bool shiftOut = (state % 2) == 1;

                // Convert state to uint to ensure 0s are shifted in.
                state = (ushort) (((uint) state) >> 1);

                // Count the 1's in the state
                if (shiftOut) oneCount++;
                if (oneCount >= 2)
                {
                    // Non-unique state flag found
                    return false;
                }
            }

            // If we looped through bit size of the state, without > 2 ones
            return true;
        }
    }
}
