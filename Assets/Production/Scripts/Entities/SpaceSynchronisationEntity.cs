using System;
using System.Collections.Generic;
using System.Linq;
using Production.Scripts.Components;
using TMPro;
using UnityEngine;
using UnityEngine.VR;
using UnityEngine.XR;
using InputTracking = UnityEngine.XR.InputTracking;

namespace Production.Scripts.Entities
{
    public class SpaceSynchronisationEntity : MonoBehaviour
    {

        public Transform worldAnchor;
        public LineRenderer ZoneGuardian;
      
        private void LateUpdate()
        {
            if (OVRInput.GetDown(OVRInput.RawButton.A))
            {
                MovePlayerInVirtualSpaceRelativeToRealSpace();
            }
        }

        
        void MovePlayerInVirtualSpaceRelativeToRealSpace()
        {
            if (OVRManager.boundary.GetConfigured())
               {
                   Vector3[] boundaryPosition = OVRManager.boundary.GetGeometry(OVRBoundary.BoundaryType.OuterBoundary);
                   DebugHelper.Instance.Log(boundaryPosition.Length.ToString());
                   if (boundaryPosition.Length >= 4)
                   {
                       ZoneGuardian.positionCount = boundaryPosition.Length;
                       ZoneGuardian.SetPositions(boundaryPosition);
                       int boundaryCornerIndex = boundaryPosition.Length / 4*3;
                       worldAnchor.position = boundaryPosition[0];
                       worldAnchor.LookAt(boundaryPosition[boundaryCornerIndex]);
                   }
               }
        }
    }

   
}