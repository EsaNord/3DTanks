using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tanks3D
{
    public class CollectableSpawner : MonoBehaviour
    {
        [SerializeField, Tooltip("Collectable pool size")]
        private int _poolSize = 20;
        [SerializeField, Tooltip("Collectable Spawn Delay")]
        private float _spawnDelay = 2f;
        [SerializeField, Tooltip("Collectable Prefab")]
        private Collectable _collectablePrefab;        
        [SerializeField, Tooltip("Collectable Y position")]
        private float _posY = 1f;
        [SerializeField]
        public Vector3 Position1 = new Vector3(5, 0, 5);
        [SerializeField]
        public Vector3 Position2 = new Vector3(-5, 0, -5);

        private List<Vector3> _points = new List<Vector3>();
        private List<float> _positionListX = new List<float>();
        private List<float> _positionListZ = new List<float>();
        private Pool<Collectable> _collectables;

        // Max and min values.
        private float _maxX;
        private float _minX;
        private float _maxZ;
        private float _minZ;
        private float _timer;

        /// <summary>
        /// Awake method for starting initialization of
        /// collectable spawner.
        /// </summary>
        private void Awake()
        {
            Debug.Log("Collectable spawner Init");
            _collectables = new Pool<Collectable>(_poolSize, _collectablePrefab, false, InitCollectable);
            Init();
        }

        /// <summary>
        /// Initialization of the collectable spawner
        /// where spawn areas minimum and maximum X & Z coordinates
        /// are set.
        /// </summary>
        private void Init()
        {
            _points.Add(Position1);
            _points.Add(Position2);

            for (int i = 0; i < _points.Count; i++)
            {
                _positionListX.Add(_points[i].x);
                _positionListZ.Add(_points[i].z);
            }

            GetSpawnRange();

            Debug.Log("maxX: " + _maxX + " minX: " + _minX + " maxZ: " + _maxZ + " minZ: " + _minZ);
        }

        /// <summary>
        /// Initializes collectable.
        /// </summary>
        /// <param name="collectable">Collectable object</param>
        private void InitCollectable(Collectable collectable)
        {
            collectable.Init(GameManager.Instance.score, CollectableCollected);
        }

        /// <summary>
        /// Sets maximum and minimum X & Z positions.
        /// </summary>
        private void GetSpawnRange()
        {  
            _maxX = GetHighest(_positionListX);
            _maxZ = GetHighest(_positionListZ);
            _minX = GetLowest(_positionListX);
            _minZ = GetLowest(_positionListZ);            
        }

        /// <summary>
        /// Returns highest value from the list.
        /// </summary>
        /// <param name="items">List of items to compare</param>
        /// <returns>Highest value</returns>
        private float GetHighest(List<float> items)
        {
            float result = items[0];

            for (int i = 0; i < items.Count; i++)
            {
                if (items[i] > result)
                    result = items[i];
            }

            return result;
        }

        /// <summary>
        /// Returns lowest value from the list.
        /// </summary>
        /// <param name="items">List of items to compare</param>
        /// <returns>Lowest value</returns>
        private float GetLowest(List<float> items)
        {
            float result = items[0];

            for (int i = 0; i < items.Count; i++)
            {
                if (items[i] < result)
                    result = items[i];
            }

            return result;
        }

        /// <summary>
        /// Sets collectables spawn position.
        /// </summary>
        /// <returns>Spawn position in vector3 form</returns>
        private Vector3 SpawnPosition()
        {
            float x = Random.Range(_minX, _maxX);
            float z = Random.Range(_minZ, _maxZ);

            return new Vector3(x, _posY, z);
        }

        /// <summary>
        /// Spawns Collectable.
        /// </summary>
        private void Spawn()
        {
            Collectable collectable = _collectables.GetPooledObject();
            if (collectable != null)
            {
                collectable.transform.position = SpawnPosition();
            }
        }

        /// <summary>
        /// Timer for collectable spawn.
        /// </summary>
        /// <returns>boolean value</returns>
        private bool SpawnTimer()
        {
            bool result = false;
            _timer += Time.deltaTime;

            if (_timer >= _spawnDelay)
            {
                result = true;
                _timer = 0;
            }

            return result;
        }

        private void Update()
        {
            if (SpawnTimer())
            {
                Spawn();
            }
        }

        /// <summary>
        /// Draws lines between spawn areas corners.
        /// </summary>
        protected void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(Position1, new Vector3(Position2.x, 0, Position1.z));
            Gizmos.DrawLine(Position1, new Vector3(Position1.x, 0, Position2.z));
            Gizmos.DrawLine(Position2, new Vector3(Position2.x, 0, Position1.z));
            Gizmos.DrawLine(Position2, new Vector3(Position1.x, 0, Position2.z));
        }

        /// <summary>
        /// Event that is triggered when collectable is collected.
        /// </summary>
        /// <param name="collectable">Collected collected</param>
        private void CollectableCollected(Collectable collectable)
        {
            if (!_collectables.ReturnObject(collectable))
            {
                Debug.LogError("ERROR: Could Not Return Collectable Back To The Pool");
            }
        }
    }
}