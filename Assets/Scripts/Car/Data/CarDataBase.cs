using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
namespace Car.Data
{
    [CreateAssetMenu(fileName = "CarDataBase", menuName = "Scriptable Objects/CarDataBase")]
    public class CarDataBase : ScriptableObject
    {
        [SerializeField] private List<CarConfig> _carConfigs = new List<CarConfig>();

        public CarConfig GetCarConfigs(string Name) { return _carConfigs.Find(car => car.Name == Name); }
    }
}