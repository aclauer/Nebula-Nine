/*
 * Copyright (c) Meta Platforms, Inc. and affiliates.
 * All rights reserved.
 *
 * Licensed under the Oculus SDK License Agreement (the "License");
 * you may not use the Oculus SDK except in compliance with the License,
 * which is provided at the time of installation or download, or which
 * otherwise accompanies this software in either electronic or hard copy form.
 *
 * You may obtain a copy of the License at
 *
 * https://developer.oculus.com/licenses/oculussdk/
 *
 * Unless required by applicable law or agreed to in writing, the Oculus SDK
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using Oculus.Interaction.Surfaces;
using UnityEngine;

namespace Oculus.Interaction.Locomotion
{
    public struct TeleportHit
    {
        public Transform relativeTo;

        public float SnapRadius
        {
            get;
            set;
        }

        public Vector3 Point
        {
            get
            {
                if (relativeTo == null)
                {
                    return _localPose.position;
                }

                return PoseUtils.Multiply(relativeTo.GetPose(), _localPose).position;
            }
        }

        public Vector3 Normal
        {
            get
            {
                if (relativeTo == null)
                {
                    return _localPose.rotation * Vector3.forward;
                }

                return PoseUtils.Multiply(relativeTo.GetPose(), _localPose).rotation * Vector3.forward;
            }
        }

        private Pose _localPose;

        public TeleportHit(Transform relativeTo, Vector3 position, Vector3 normal, float snapRadius = 0f)
        {
            this.relativeTo = relativeTo;
            Pose worldSpacePose = new Pose(position, Quaternion.LookRotation(normal));
            if (relativeTo == null)
            {
                this._localPose = worldSpacePose;
            }
            else
            {
                this._localPose = PoseUtils.Delta(relativeTo.GetPose(), worldSpacePose);
            }
            SnapRadius = snapRadius;
        }

        public static readonly TeleportHit DEFAULT =
            new TeleportHit()
            {
                relativeTo = null,
                _localPose = Pose.identity
            };
    }

    /// <summary>
    /// Makes an object a teleport hotspot.
    /// </summary>
    public class TeleportInteractable : Interactable<TeleportInteractor, TeleportInteractable>
    {
        [SerializeField]
        [Tooltip("Indicates if the interactable is valid for teleport. Setting it to false can be convenient to block the arc.")]
        private bool _allowTeleport = true;
        /// <summary>
        ///  Indicates if the interactable is valid for teleport. Setting it to false can be convenient to block the arc.
        /// </summary>
        public bool AllowTeleport
        {
            get
            {
                return _allowTeleport;
            }
            set
            {
                _allowTeleport = value;
            }
        }

        [SerializeField, Optional, ConditionalHide("_allowTeleport", true)]
        [Tooltip("An override for the Interactor EqualDistanceThreshold used when comparing the interactable against other interactables that does not allow teleport.")]
        private float _equalDistanceToBlockerOverride;
        /// <summary>
        /// An override for the Interactor EqualDistanceThreshold used when comparing the interactable against other interactables that does not allow teleport.
        /// </summary>
        public float EqualDistanceToBlockerOverride
        {
            get
            {
                return _equalDistanceToBlockerOverride;
            }
            set
            {
                _equalDistanceToBlockerOverride = value;
            }
        }

        [SerializeField, Optional]
        [Tooltip("Establishes the priority when several interactables are hit at the same time (EqualDistanceThreshold) by the arc.")]
        private int _tieBreakerScore;
        /// <summary>
        /// Establishes the priority when several interactables are hit at the same time (EqualDistanceThreshold) by the arc.
        /// </summary>
        public int TieBreakerScore
        {
            get
            {
                return _tieBreakerScore;
            }
            set
            {
                _tieBreakerScore = value;
            }
        }

        [SerializeField, Interface(typeof(ISurface))]
        [Tooltip("Surface against which the interactor will check collision with the arc.")]
        private UnityEngine.Object _surface;
        public ISurface Surface { get; private set; }
        public IBounds SurfaceBounds { get; private set; }

        [Header("Target", order =-1)]
        [SerializeField, Optional]
        [Tooltip("A specific point in space where the player should teleport to.")]
        private Transform _targetPoint;

        [SerializeField, Optional]
        [Tooltip("When true, the player will also face the direction specified by the target point.")]
        private bool _faceTargetDirection;
        /// <summary>
        /// When true, the player will also face the direction specified by the target point.
        /// </summary>
        public bool FaceTargetDirection
        {
            get
            {
                return _faceTargetDirection;
            }
            set
            {
                _faceTargetDirection = value;
            }
        }

        [SerializeField, Optional]
        [Tooltip("When true, instead of aligning the players feet to the TargetPoint it will align the head.")]
        private bool _eyeLevel;
        /// <summary>
        ///  When true, instead of aligning the players feet to the TargetPoint it will align the head.
        /// </summary>
        public bool EyeLevel
        {
            get
            {
                return _eyeLevel;
            }
            set
            {
                _eyeLevel = value;
            }
        }

        protected override void Awake()
        {
            base.Awake();

            Surface = _surface as ISurface;
            SurfaceBounds = _surface as IBounds;
        }

        protected override void Start()
        {
            this.BeginStart(ref _started, () => base.Start());
            this.AssertField(Surface, nameof(Surface));
            this.EndStart(ref _started);
        }

        /// <summary>
        /// Determines if a <cref="TeleportInteractor" /> is close enough to target this interactable.
        /// </summary>
        public bool IsInRange(Pose origin, float maxSqrDistance)
        {
            if (SurfaceBounds == null)
            {
                return true;
            }

            Bounds bounds = SurfaceBounds.Bounds;
            Vector3 dir = Vector3.ProjectOnPlane(origin.forward, Vector3.up).normalized;
            Vector3 point = bounds.center;
            point.y = origin.position.y;
            float maxColliderSqrRadius = bounds.extents.x * bounds.extents.x
                + bounds.extents.z * bounds.extents.z;

            float sqrDistanceToCenter = (point - origin.position).sqrMagnitude;
            if (Mathf.Max(0, sqrDistanceToCenter - maxColliderSqrRadius) > maxSqrDistance)
            {
                return false;
            }

            Vector3 pointToDir = Vector3.Cross(point - origin.position, dir);
            float sqrDistanceToDir = pointToDir.sqrMagnitude;
            if (sqrDistanceToDir <= maxColliderSqrRadius)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Detects a hit from the teleport raycast on the object.
        /// </summary>
        public bool DetectHit(Vector3 from, Vector3 to, out TeleportHit hit)
        {

            //Console.Debug("Trying to detect a hit.\n");

            //Vector3 dir = to - from;
            //Ray ray = new Ray(from, dir);

            //if (Physics.Raycast(ray, out RaycastHit rayHit, dir.magnitude))
            //{
            //    if (rayHit.collider.CompareTag("Door"))
            //    {
            //        Console.Debug("Teleportation detected a door.\n");
            //        _allowTeleport = false;
            //        hit = TeleportHit.DEFAULT;
            //        return false;
            //    }
            //}

            //if (Surface.Raycast(ray, out SurfaceHit surfaceHit, dir.magnitude))
            //{
            //    Console.Debug("This should be a valid teleportation.\n");
            //    hit = new TeleportHit(this.transform, surfaceHit.Point, surfaceHit.Normal,
            //        _equalDistanceToBlockerOverride);
            //    return true;
            //}

            //Console.Debug("This is the default case.\n");
            //hit = TeleportHit.DEFAULT;
            //return false;

            Vector3 dir = to - from;
            Ray ray = new Ray(from, dir);
            if (Surface.Raycast(ray, out SurfaceHit surfaceHit, dir.magnitude))
            {
                Debug.Log("Valid teleport hit.\n");              
                hit = new TeleportHit(this.transform, surfaceHit.Point, surfaceHit.Normal,
                    _equalDistanceToBlockerOverride);
                return true;
            }

            //Debug.Log("Invalid teleport hit.\n");
            hit = TeleportHit.DEFAULT;
            return false;
        }

        /// <summary>
        /// Used when the interactor is looking for a teleport target. If you specify a target point, it'll use that.
        /// Otherwise it uses wherever the teleport arc is pointing.
        /// </summary>
        public Pose TargetPose(Pose hitPose)
        {
            Pose targetPose = hitPose;

            if (_targetPoint != null)
            {
                targetPose.position = _targetPoint.position;
                targetPose.rotation = _targetPoint.rotation;
            }

            return targetPose;
        }

        #region Inject
        /// <summary>
        /// Sets all required values for a <cref="TeleportInteractable" /> for a dynamically instantiated GameObject.
        /// </summary>
        public void InjectAllTeleportInteractable(ISurface surface)
        {
            InjectSurface(surface);
        }

        /// <summary>
        /// Sets an <cref="ISurface" /> for a dynamically instantiated GameObject.
        /// </summary>
        public void InjectSurface(ISurface surface)
        {
            _surface = surface as UnityEngine.Object;
            Surface = surface;
            SurfaceBounds = surface as IBounds;
        }

        /// <summary>
        /// Sets a target point for a dynamically instantiated GameObject.
        /// </summary>
        public void InjectOptionalTargetPoint(Transform targetPoint)
        {
            _targetPoint = targetPoint;
        }
        #endregion
    }
}
