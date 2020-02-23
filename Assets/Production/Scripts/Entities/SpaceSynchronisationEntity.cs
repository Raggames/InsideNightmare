using Production.Scripts.Components;
using TMPro;
using UnityEngine;

namespace Production.Scripts.Entities
{
    public class SpaceSynchronisationEntity : MonoBehaviour
    {
       
        
        public TeleportationComponent TeleportationComponent;
        public Transform CenterPoint;
        public TextMeshProUGUI position;
      
        public TextMeshProUGUI trackerPos;
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
            Vector3 calculatedPosition = OVRManager.instance.headPoseRelativeOffsetTranslation;
            trackerPos.text = "Head translation => " + calculatedPosition;
        }

        void MovePlayerInVirtualSpaceRelativeToRealSpace()
        {
            OVRPlugin.SetBoundaryVisible(true);
            OVRPlugin.Posef ovrPose = new OVRPlugin.Posef();
            
            ovrPose = OVRPlugin.GetNodePose(OVRPlugin.Node.TrackerZero, OVRPlugin.Step.Render);
            var a = OVRPlugin.GetNodePose(OVRPlugin.Node.TrackerZero, OVRPlugin.Step.Render);
            var b = OVRPlugin.GetNodePose(OVRPlugin.Node.TrackerOne, OVRPlugin.Step.Render);
            var c = OVRPlugin.GetNodePose(OVRPlugin.Node.TrackerTwo, OVRPlugin.Step.Render);
            var d = OVRPlugin.GetNodePose(OVRPlugin.Node.TrackerThree, OVRPlugin.Step.Render);

            
            
            
            //Vector3 calculatedPosition = a.Position.FromVector3f();
            Vector3 calculatedPosition = OVRManager.instance.headPoseRelativeOffsetTranslation;
            Quaternion calculatedRotation = a.Orientation.FromQuatf();
            
            var poses = OVRPlugin.GetBoundaryGeometry(OVRPlugin.BoundaryType.PlayArea);
            Vector3 CenterPosition = new Vector3();
            if (poses.PointsCount > 0)
            {
               OVRPlugin.Vector3f[] pointsGuardian = poses.Points;
               for (int i = 0; i < poses.PointsCount; i++)
                {
                    var pos = pointsGuardian[i].FromVector3f();
                    CenterPosition += pos;
                }
                
                CenterPosition /= poses.PointsCount;
                CenterPosition.y = 0;
            }

            Vector3 dir = CenterPosition - calculatedPosition;
            position.text = "BoundaryPoint = " + poses.Points[0] + " "+ poses.Points[1] + " "+poses.Points[2]+ " " + poses.Points[3] + "  // Calcultated Position = " + calculatedPosition+ " ,Poses Count = " + poses.PointsCount + " , Center = " + CenterPosition + " ,dir = " + dir
                + " GetNodePoses = " + ovrPose + " " + a.Position + b.Position + c.Position + d.Position.FromVector3f();

            
            TeleportationComponent.Teleport(CenterPoint.position + dir, new Vector3(), calculatedRotation); //GetPose().position, new Vector3(tracker.GetPose().position.x, tracker.GetPose().position.y, tracker.GetPose().position.z +1));
        }
    }
}