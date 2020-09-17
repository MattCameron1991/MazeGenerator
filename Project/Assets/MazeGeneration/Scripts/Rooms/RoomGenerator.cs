﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MazeGeneration.Rooms
{
    public abstract class RoomGenerator : ScriptableObject
    {
        public abstract IEnumerator GenerateRooms(Maze aMaze);
    }
}