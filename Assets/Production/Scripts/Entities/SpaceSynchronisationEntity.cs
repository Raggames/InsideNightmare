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
        public OVRManager OvrManager;
        public Transform CenterPoint;
        public TextMeshProUGUI position;
        public TextMeshProUGUI trackerPos;
        private Mesh viewMesh;
        public MeshFilter viewMeshFilter;


        public GameObject Cube;
      
        private void Start()
        {
            viewMesh = new Mesh();
            viewMeshFilter.mesh = viewMesh;
            
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
            OVRPlugin.SetBoundaryVisible(true);
           // var poses = OVRPlugin.GetBoundaryGeometry(OVRPlugin.BoundaryType.OuterBoundary);
            Debug.Log(OVRPlugin.version);
           Test();
           
           if (OVRManager.boundary.GetConfigured())
           {
              
               Vector3[] boundaryPosition = OVRManager.boundary.GetGeometry(OVRBoundary.BoundaryType.OuterBoundary);
               int index = 0;
               foreach (var v in boundaryPosition)
               {
                   Instantiate(Cube, boundaryPosition[index], Quaternion.identity);
                   index++;
               }
               trackerPos.text = boundaryPosition.Length.ToString();
           }
            
          
            //Vector3 guardianDirection = boundaryPosition[0];
            //float Angle = Vector3.Angle(worldAnchor.position, guardianDirection);
           // position.text = "GuardianPose 0 => " +poses.Points[0].FromVector3f()+ "  GuardianDir => " + guardianDirection + " Angle => " + Angle;
            //worldAnchor.position = poses.Points[0].FromVector3f();
            //worldAnchor.Rotate(Vector3.up, Angle);
           
           // GenerateMesh(boundaryPosition);

        }

        public void GenerateMesh(Vector3[] points)
        {
            Vector3[] vertices = new Vector3[points.Length];
            int[] triangles = new int[(points.Length - 2) * 3];

            vertices[0] = Vector3.zero;
            for (int i = 0; i < points.Length - 1; i++)
            {
                vertices[i + 1] = transform.InverseTransformPoint(points[i]) + Vector3.forward * 0.1f;

                if (i < points.Length - 2)
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

        public List<Vector3> TryGetGeometryBoundary(OVRBoundary.BoundaryType m_boundaryType)
        {
            List<Vector3> positions = new List<Vector3>();
            Vector3[] boundaryPoints = OVRManager.boundary.GetGeometry(m_boundaryType);
            Vector3 v;
            for(int i=0; i<boundaryPoints.Length; ++i)
            {
                    v = boundaryPoints[i];
                    v.y = 0.0f;
                    positions.Add(v);
            }
                v = boundaryPoints[0];
                v.y = 0.0f;
            return positions;
        }

        void Test()
        {
            XRInputSubsystem system = new XRInputSubsystem();
            system.TryGetBoundaryPoints(OVRManager.boundary.GetGeometry(OVRBoundary.BoundaryType.OuterBoundary).ToList());
            Debug.Log(system.TryGetBoundaryPoints(OVRManager.boundary.GetGeometry(OVRBoundary.BoundaryType.OuterBoundary).ToList()));
        }
    }

   
}