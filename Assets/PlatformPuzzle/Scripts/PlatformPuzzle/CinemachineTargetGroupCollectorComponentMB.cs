using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using UnityAtoms.BaseAtoms;
using UnityEngine;

namespace PlatformPuzzle
{
    public class CinemachineTargetGroupCollectorComponentMB : MonoBehaviour
    {
        [SerializeField]
        private CinemachineTargetGroup _targetGroup;

        [SerializeField]
        private FloatReference _weight;

        [SerializeField]
        private FloatReference _radius;

        public void CollectChildren()
        {
            int childCount = transform.childCount;

            List<Transform> targets = _targetGroup.m_Targets
                .Select(x => x.target)
                .ToList();

            for (int i = 0; i < childCount; i++)
            {

                Transform child = transform.GetChild(i);
                if (targets.Contains(child))
                {
                    continue;
                }

                _targetGroup.AddMember(child, _weight.Value, _radius.Value);
            }
        }
    }
}

