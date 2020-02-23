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
        public TextMeshProUGUI position;
        public TextMeshProUGUI trackerPos;
       
        public LineRenderer ZoneGuardian;
      
        private void Start()
        {
            //MovePlayerInVirtualSpaceRelativeToRealSpace();
        }

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
                   if (boundaryPosition.Length >= 255)
                   {
                       ZoneGuardian.positionCount = boundaryPosition.Length;
                       ZoneGuardian.SetPositions(boundaryPosition);
                       Vector3 guardianDirection = boundaryPosition[0]-boundaryPosition[192];
                       float Angle = Vector3.Angle(worldAnchor.position+Vector3.forward, guardianDirection);
                       worldAnchor.position = boundaryPosition[0];
                       worldAnchor.Rotate(Vector3.up, -Angle);
                   }
               }
        }
    }

   
}