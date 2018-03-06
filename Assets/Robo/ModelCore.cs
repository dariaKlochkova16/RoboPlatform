using Assets.General;
using UnityEngine;

namespace Assets.Robo
{
    class ModelCore : MonoBehaviour
    {
        //private IMapProvider mapProvider;
        //private IMotionController motionController;
        //private ICamera camera;
        //private IModelServer model;

        public MapProviderEmulator mapProvider;
        public MotionControllerEmulator motionController;
        public CameraEmulator camera;
        public RoboModelServer model;

        public void Start()
        {
            model.MovementEvent += Move;
        }

        private void Move(object sender, MotionEventArgs e)
        {
            motionController.Move(e.motionType, e.distance);
        }

        public void Update()
        {
            model.SetMap(mapProvider.GetMap());
            model.SetVideoImage(camera.GetVideoImage());
        }
    }
}
