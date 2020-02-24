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

        public TeleportationComponent teleportationComponent;
        public Transform worldAnchor;
        public LineRenderer ZoneGuardian;

        private Vector3 playerPos;
        private Quaternion playerRot;
        private bool playerHasMooved;
        private Mesh viewMesh;
        public MeshFilter MeshFilter;

        private Vector3[] GuardianZone;

        private void Start()
        {
            playerPos = teleportationComponent.transform.position;
            playerRot = teleportationComponent.transform.rotation;
            viewMesh = new Mesh();
            MeshFilter.mesh = viewMesh;
        }

        private void LateUpdate()
        {
            if (OVRInput.GetDown(OVRInput.RawButton.B))
            {
                TranslateAndRotateVirtualSpaceRelativeToRealSpace();
            }
           if (OVRInput.GetDown(OVRInput.RawButton.Y))
            {
                if (GuardianZone.Length > 4)
                {
                    GenerateMesh(GuardianZone);
                    DebugHelper.Instance.Log("Generating mesh with " + GuardianZone.Length.ToString() + " points");
                }      
            }
            
        }

        
        void TranslateAndRotateVirtualSpaceRelativeToRealSpace()
        {
            if (OVRManager.boundary.GetConfigured())
            {
                   Vector3[] boundaryPosition = OVRManager.boundary.GetGeometry(OVRBoundary.BoundaryType.OuterBoundary);
                   GuardianZone = boundaryPosition;
                   DebugHelper.Instance.Log(boundaryPosition.Length.ToString());
                   if (boundaryPosition.Length >= 4)
                   {
                       // set the world on the new poses
                       int boundaryCornerIndex = boundaryPosition.Length / 4*3;
                       
                       worldAnchor.position = boundaryPosition[0];
                       worldAnchor.LookAt(boundaryPosition[boundaryCornerIndex]);
                       
                       for (int i = 0; i < boundaryPosition.Length; i++)
                       {
                           boundaryPosition[i].y = 0.5f;
                       }
                       ZoneGuardian.positionCount = boundaryPosition.Length;
                       ZoneGuardian.SetPositions(boundaryPosition);
                       
                       //GenerateMesh(boundaryPosition);
                   }
            }
       
        }

        public void GenerateMesh(Vector3[] viewPoints)
        {
            for (int i = 0; i < viewPoints.Length; i++)
            {
                viewPoints[i].y = 0.05f;
            }
            int vertexCount = viewPoints.Length + 1;
            Vector3[] vertices = new Vector3[vertexCount];
            int[] triangles = new int[(vertexCount - 2) * 3];

            vertices[0] = Vector3.zero;
            for (int i = 0; i < vertexCount - 1; i++)
            {
                vertices[i + 1] = transform.InverseTransformPoint(viewPoints[i]) + Vector3.forward * 0.1f;

                if (i < vertexCount - 2)
                {
                    triangles[i * 3] = 0;
                    triangles[i * 3 + 1] = i + 1;
                    triangles[i * 3 + 2] = i + 2;
                }
            }
            
            viewMesh.Clear();
            viewMesh.vertices = vertices;
            viewMesh.triangles = triangles;
            viewMesh.RecalculateNormals();
        }
/*
        void MovePlayerToVirtualSpaceRelativeToTrackingSpace()
        {
            var Tracked = OVRManager.boundary.TestPoint(teleportationComponent.transform.position, OVRBoundary.BoundaryType.OuterBoundary);
            Vector3 closest = Tracked.ClosestPoint;
            //DebugHelper.Instance.Log(Tracked.position.ToString());
            DebugHelper.Instance.Log(closest.ToString());
            teleportationComponent.Teleport(playerPos, new Vector3(), playerRot);
        }

        */
        RectFromPointsArray GetRectFromArray(Vector3[] positions, Vector3 Init)
        {
            RectFromPointsArray newRect = new RectFromPointsArray();
            newRect.A = Init;
            
            // Computing farest from A, his biggest diagonal 
            float diagonaleDist = 0;
            int atIndex = 0;
            for (int i = 0; i < positions.Length; i++)
            {
                float dist = Vector3.Distance(Init, positions[i]);
                if (dist > diagonaleDist)
                {
                    diagonaleDist = dist;
                    atIndex = i;
                }
            }
            newRect.C = positions[atIndex];

            // Computing farest in X from C
            for (int i = 0; i < positions.Length; i++)
            {
                
            }
            
            
            //Computing farest in Y from C
            
            return newRect;
        }
        
        
    }

    public struct RectFromPointsArray
    {
        public Vector3 A;
        public Vector3 B;
        public Vector3 C;
        public Vector3 D;
    }
   
}