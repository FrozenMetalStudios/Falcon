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
 * Filename: AbstractMovement.cs
 * 
 * Description: Default Movement structure and methods.
 * 
 *******************************************************************/
using System.Collections;
using System;
using Assets.Scripts.Utility.FlagStates;

namespace Assets.Scripts.Player.Movement
{
    /// <summary>
    /// Simple Flag State Storage.
    /// </summary>
    public class MovementState
    {
        enum State : int { Movement = 0,
                           Rotation };

        [Flags]
        public enum MovementFlag : ushort { Idle = 0x0,
                                            Walking = 0x1,
                                            Running = 0x2 };

        [Flags]
        public enum RotationFlag : ushort { Idle = 0x0,
                                            Turning = 0x1 };

        /// <summary>
        /// 
        /// </summary>
        private AbstractFlagState[] states;

        /// <summary>
        /// 
        /// </summary>
        public MovementState()
        {
            // Configure the State Storage.
            states = new AbstractFlagState[2];
            states[(int)State.Movement] = new UniqueFlagState();
            states[(int)State.Rotation] = new UniqueFlagState();
        }

        public void SetMovement(MovementFlag mask)
        {
            states[(int)State.Movement].SetFlag((ushort)mask);
        }

        public void ClearMovement()
        {
            states[(int)State.Movement].ClearFlag();
        }

        public ushort GetMovement()
        {
            return states[(int)State.Movement].GetState();
        }

        public void SetRotation(RotationFlag mask)
        {
            states[(int)State.Rotation].SetFlag((ushort)mask);
        }

        public void ClearRotation()
        {
            states[(int)State.Rotation].ClearFlag();
        }

        public ushort GetRotation()
        {
            return states[(int)State.Rotation].GetState();
        }

        public void Reset()
        {
            foreach (AbstractFlagState state in states)
            {
                state.SetToClear();
            }
        }
    }
}
