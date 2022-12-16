using UnityEngine;
using UnityEngine.XR.MagicLeap;
using MagicLeap.Core;

namespace MagicLeap {
    public class ModelVisualizer : MonoBehaviour
    {

        #pragma warning disable 414
        [SerializeField, Tooltip("The MLImageTrackerBehavior that will be subscribed to.")]
        private MLImageTrackerBehavior _imageTracker = null;
        #pragma warning restore 414

        [SerializeField, Tooltip("Position offset of the explorer's target relative to this transform.")]
        private Vector3 _positionOffset = Vector3.zero;

        [SerializeField, Tooltip("Prefab of the Model.")]
        private GameObject _model = null;

        private void Awake()
        {
            if(_model == null)
            {
                Debug.LogError("Error: ModelVisualizer._model is not set, disabling script.");
                enabled = false;
                return;
            }

            #if PLATFORM_LUMIN
            _imageTracker.OnTargetFound += (MLImageTracker.Target target, MLImageTracker.Target.Result result) => { gameObject.SetActive(true); };
            _imageTracker.OnTargetLost += (MLImageTracker.Target target, MLImageTracker.Target.Result result) => { gameObject.SetActive(false); };
            #endif

            gameObject.SetActive(false);
        }

        void OnEnable()
        {
            //_model.SetActive(true);

        }

        void OnDisable()
        {
            //_model.SetActive(false);
        }

        void Update()
        {
            
        }

    }


}

