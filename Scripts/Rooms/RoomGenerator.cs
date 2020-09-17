﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MazeGeneration.Rooms
{
    public abstract class RoomGenerator : ScriptableObject
    {
        #region Variables
        #region Fields
        [SerializeField]
        private float m_routineDelay = 0.1f;
        #endregion

        #region Properties
        public float RoutineDelay
        {
            get => m_routineDelay;
            set => m_routineDelay = value;
        }

        protected abstract bool ShouldContinue { get; }
        #endregion
        #endregion

        #region RoomGenerator
        public IEnumerator GenerateRoomsRoutine(Maze aMaze, Vector2Int aSize)
        {
            InitialiseGenerator(aMaze, aSize);

            do
            {
                StepGenerator(aMaze, aSize);

                if (RoutineDelay > 0)
                {
                    yield return new WaitForSeconds(RoutineDelay);
                }
            } while (ShouldContinue);
        }


        public void GenerateRooms(Maze aMaze, Vector2Int aSize)
        {
            InitialiseGenerator(aMaze, aSize);

            do
            {
                StepGenerator(aMaze, aSize);
            } while (ShouldContinue);
        }

        protected abstract void InitialiseGenerator(Maze aMaze, Vector2Int aSize);
        protected abstract void StepGenerator(Maze aMaze, Vector2Int aSize);
        #endregion
    }
}