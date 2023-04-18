using NavMeshPlus.Components;
using System.Collections;
using UnityEngine;

namespace LevelEdit
{
    public class RebakeNavMesh : MonoBehaviour
    {
        [SerializeField] private NavMeshSurface navMesh;
        private const float _waitTime = 2;
        private bool isBake;
        private bool isCoroutineStart;

        private void Update()
        {
            if (!isBake)
            {
                if (!isCoroutineStart)
                {
                    StartCoroutine(RebakeTime());
                }
                navMesh.BuildNavMesh();
            }
        }

        private IEnumerator RebakeTime()
        {
            isCoroutineStart = true;
            yield return new WaitForSeconds(_waitTime);
            isBake = true;
        }
    }
}
