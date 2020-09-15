﻿using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace MazeGeneration
{
    using Paths;
    using Rooms;
    using Building;

    /// <summary>
    /// Generates a map using a 2D grid of cells
    /// </summary>
    public class MazeGenerator : MonoBehaviour
    {
        #region Variables
        #region Fields
        [Header("Size")]
        [SerializeField] private bool m_uniformCells;
        [SerializeField]private Vector2Int m_gridSize = new Vector2Int(10, 10);

        [Header("Rooms")]
        [SerializeField] private RoomGenerator m_roomGenerator;

        [Header("Path Generation")]
        [SerializeField] private PathAlgorithm m_generationAlgorithm;

        private Maze m_maze;

        [Header("Building")]
        [SerializeField] private MazeBuilderBase m_builder;
        #endregion

        #region Properties
        #endregion
        #endregion

        #region Mono
        protected void Start()
        {
            m_generationAlgorithm.OnCellChanged += (a) => m_builder?.UpdateConstruction(m_maze, a);
            Generate();
        }
        #endregion

        #region MazeGenerator
        public void RegisterBuilder(MazeBuilderBase aBuilder)
        {
            m_builder = aBuilder;
        }

        public void Generate()
        {
            m_maze = new Maze(m_gridSize, m_uniformCells);

            m_builder?.BuildMaze(m_maze);

            StartCoroutine(Routine());

            IEnumerator Routine()
            {
                if (m_roomGenerator) yield return StartCoroutine(m_roomGenerator?.GenerateRooms(m_maze));
                if (m_generationAlgorithm) yield return StartCoroutine(m_generationAlgorithm?.GeneratePath(m_maze, m_gridSize));

                m_builder?.BuildMaze(m_maze);
            }
        }
        #endregion
    }
}
