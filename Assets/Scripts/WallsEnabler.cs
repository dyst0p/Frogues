using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FroguesFramework
{
    public class WallsEnabler : MonoBehaviour
    {
        [SerializeField] private CameraController cameraController;
        [SerializeField] private List<Pillar> pillars;
        [SerializeField] private List<AnimatedWall> walls;
        [SerializeField] private float angleDelta = 55f;

        private void Update()
        {
            EnableWalls();
            EnablePillars();
        }

        private void EnableWalls()
        {
            foreach (var wall in walls)
            {
                wall.Showed = Vector3.Angle(Camera.main.transform.forward, wall.transform.forward) > angleDelta;
            }
        }

        private void EnablePillars()
        {
            foreach (var pillar in pillars)
            {
                pillar.itSelf.Showed = pillar.neighborWalls.Any(wall => wall.Showed);
            }
        }
    }

    [Serializable]
    public struct Pillar
    {
        public AnimatedWall itSelf;
        public List<AnimatedWall> neighborWalls;
    }
}