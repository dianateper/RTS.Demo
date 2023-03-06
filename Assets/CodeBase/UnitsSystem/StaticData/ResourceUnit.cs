using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.UnitsSystem.StaticData
{
    [CreateAssetMenu(menuName = "RTS/Create Resource Unit", fileName = "ResourceUnit", order = 0)]
    public class ResourceUnit : Unit
    {
        [SerializeField] private List<ResourceType> _resources;
    }
}