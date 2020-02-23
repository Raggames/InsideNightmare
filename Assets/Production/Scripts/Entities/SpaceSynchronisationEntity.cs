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
            MovePlayerInVirtualSpaceRelativeToRealSpace();
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
           
           // position.text = "Point Counts = " + OVRPlugin.GetBoundaryGeometry(OVRPlugin.BoundaryType.OuterBoundary).PointsCount.ToString()+  "  player Position = " + transform.position;
           /*
            OVRPlugin.Vector3f[] arrayf = OVRPlugin.GetBoundaryGeometry(OVRPlugin.BoundaryType.OuterBoundary).Points;
            Vector3[] positions = new Vector3[arrayf.Length];
            for (int i = 0; i < arrayf.Length; i++)
            {
                positions[i] = arrayf[i].FromVector3f();
                positions[i].y = 1;
            }
            ZoneGuardin.SetPositions(positions);
            
            */
            
            if (OVRManager.boundary.GetConfigured())
               {
                  
                   Vector3[] boundaryPosition = OVRManager.boundary.GetGeometry(OVRBoundary.BoundaryType.OuterBoundary);
                   for (int i = 0; i < boundaryPosition.Length; i++)
                   {
                       boundaryPosition[i].y = 1;
                   }

                   ZoneGuardian.positionCount = boundaryPosition.Length;
                   ZoneGuardian.SetPositions(boundaryPosition);
                   trackerPos.text = boundaryPosition.Length.ToString()  + " " + OVRManager.boundary.GetConfigured() + boundaryPosition[127];
               }
                
          
            //Vector3 guardianDirection = boundaryPosition[0];
            //float Angle = Vector3.Angle(worldAnchor.position, guardianDirection);
           // position.text = "GuardianPose 0 => " +poses.Points[0].FromVector3f()+ "  GuardianDir => " + guardianDirection + " Angle => " + Angle;
            //worldAnchor.position = poses.Points[0].FromVector3f();
            //worldAnchor.Rotate(Vector3.up, Angle);
           
           // GenerateMesh(boundaryPosition);

        }

        
       
    }

   
}