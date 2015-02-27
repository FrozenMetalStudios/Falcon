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
 * Filename: CombinationalFlagState.cs
 * 
 * Description: Flag State storage.
 * 
 *******************************************************************/
using System.Collections;

namespace Assets.Scripts.Utility.FlagStates
{
    /// <summary>
    /// Base Flag State Storage Class.
    /// </summary>
    public class CombinationalFlagState : AbstractFlagState
    {
        /// <summary>
        /// Allows for easy Clearing and Setting of all bits.
        /// </summary>
        static public ushort CLEAR_ALL = 0xFFFF;

        /// <summary>
        ///  Default Constructor
        /// </summary>
        public CombinationalFlagState() : base()
        {
            // Default Empty Initial State.
        }

        /// <summary>
        /// Initial State Constructor.
        /// </summary>
        /// <param name="initialState">Initial States</param>
        public CombinationalFlagState(ushort initialState) : base(initialState)
        {
            // Sets the initial state.
        }

        /// <summary>
        /// Sets the Bits specified by the State Flag.
        /// </summary>
        /// <param name="stateFlag">The State Flag</param>
        override public void SetFlag(ushort stateFlag)
        {
            SetInternalFlags(stateFlag);
        }

        /// <summary>
        /// Clears the Bits specified by the State Flag.
        /// </summary>
        /// <param name="stateFlag">The State Flag</param>
        override public void ClearFlag(ushort stateFlag)
        {
            ClearInternalFlags(stateFlag);
        }
    }
}