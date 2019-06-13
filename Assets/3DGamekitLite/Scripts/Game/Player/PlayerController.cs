using System.Collections;
using Gamekit3D.Message;
using UnityEngine;

namespace Gamekit3D
{
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(Animator))]
    public class PlayerController : MonoBehaviour, IMessageReceiver
    {
        // These constants are used to ensure Ellen moves and behaves properly.
        // It is advised you don't change them without fully understanding what they do in code.
        private const float KAirborneTurnSpeedProportion = 5.4f;
        private const float KGroundedRayDistance = 1f;
        private const float KJumpAbortSpeed = 10f;
        private const float KMinEnemyDotCoeff = 0.2f;
        private const float KInverseOneEighty = 1f / 180f;
        private const float KStickingGravityProportion = 0.3f;
        private const float KGroundAcceleration = 20f;
        private const float KGroundDeceleration = 25f;
        protected static PlayerController SInstance;
        private readonly int _mHashAirborne = Animator.StringToHash("Airborne");

        // Parameters

        private readonly int _mHashAirborneVerticalSpeed = Animator.StringToHash("AirborneVerticalSpeed");
        private readonly int _mHashAngleDeltaRad = Animator.StringToHash("AngleDeltaRad");

        // Tags
        private readonly int _mHashBlockInput = Animator.StringToHash("BlockInput");
        private readonly int _mHashDeath = Animator.StringToHash("Death");
        private readonly int _mHashEllenCombo1 = Animator.StringToHash("EllenCombo1");
        private readonly int _mHashEllenCombo2 = Animator.StringToHash("EllenCombo2");
        private readonly int _mHashEllenCombo3 = Animator.StringToHash("EllenCombo3");
        private readonly int _mHashEllenCombo4 = Animator.StringToHash("EllenCombo4");
        private readonly int _mHashEllenDeath = Animator.StringToHash("EllenDeath");
        private readonly int _mHashFootFall = Animator.StringToHash("FootFall");
        private readonly int _mHashForwardSpeed = Animator.StringToHash("ForwardSpeed");
        private readonly int _mHashGrounded = Animator.StringToHash("Grounded");
        private readonly int _mHashHurt = Animator.StringToHash("Hurt");
        private readonly int _mHashHurtFromX = Animator.StringToHash("HurtFromX");
        private readonly int _mHashHurtFromY = Animator.StringToHash("HurtFromY");
        private readonly int _mHashInputDetected = Animator.StringToHash("InputDetected");
        private readonly int _mHashLanding = Animator.StringToHash("Landing"); // Also a parameter.

        // States
        private readonly int _mHashLocomotion = Animator.StringToHash("Locomotion");
        private readonly int _mHashMeleeAttack = Animator.StringToHash("MeleeAttack");
        private readonly int _mHashRespawn = Animator.StringToHash("Respawn");
        private readonly int _mHashStateTime = Animator.StringToHash("StateTime");
        private readonly int _mHashTimeoutToIdle = Animator.StringToHash("TimeoutToIdle");

        public CameraSettings cameraSettings; // Reference used to determine the camera's direction.
        public bool canAttack; // Whether or not Ellen can swing her staff.
        public RandomAudioPlayer emoteAttackPlayer;
        public RandomAudioPlayer emoteDeathPlayer;
        public RandomAudioPlayer emoteJumpPlayer;
        public RandomAudioPlayer emoteLandingPlayer;
        public RandomAudioPlayer footstepPlayer; // Random Audio Players used for various situations.
        public float gravity = 20f; // How fast Ellen accelerates downwards when airborne.
        public RandomAudioPlayer hurtAudioPlayer;
        public float idleTimeout = 5f; // How long before Ellen starts considering random idles.
        public float jumpSpeed = 10f; // How fast Ellen takes off when jumping.
        public RandomAudioPlayer landingPlayer;
        protected float MAngleDiff; // Angle in degrees between Ellen's current rotation and her target rotation.

        protected Animator
            MAnimator; // Reference used to make decisions based on Ellen's current animation and to set parameters.

        public float maxForwardSpeed = 8f; // How fast Ellen can run.
        public float maxTurnSpeed = 1200f; // How fast Ellen turns when stationary.

        protected CharacterController MCharCtrl; // Reference used to actually move Ellen.
        protected Checkpoint MCurrentCheckpoint; // Reference used to reset Ellen to the correct position on respawn.

        protected AnimatorStateInfo MCurrentStateInfo; // Information about the base layer of the animator cached.
        protected Material MCurrentWalkingSurface; // Reference used to make decisions about audio.
        protected Damageable MDamageable; // Reference used to set invulnerablity and health based on respawning.
        protected float MDesiredForwardSpeed; // How fast Ellen aims be going along the ground based on input.
        public MeleeWeapon meleeWeapon; // Reference used to (de)activate the staff when attacking. 
        protected float MForwardSpeed; // How fast Ellen is currently going along the ground.
        protected float MIdleTimer; // Used to count up to Ellen considering a random idle.
        protected bool MInAttack; // Whether Ellen is currently in the middle of a melee attack.
        protected bool MInCombo; // Whether Ellen is currently in the middle of her melee combo.
        protected PlayerInput MInput; // Reference used to determine how Ellen should move.
        public float minTurnSpeed = 400f; // How fast Ellen turns when moving at maximum speed.
        protected bool MIsAnimatorTransitioning;
        protected bool MIsGrounded = true; // Whether or not Ellen is currently standing on the ground.
        protected AnimatorStateInfo MNextStateInfo;
        protected Collider[] MOverlapResult = new Collider[8]; // Used to cache colliders that are near Ellen.

        protected AnimatorStateInfo
            MPreviousCurrentStateInfo; // Information about the base layer of the animator from last frame.

        protected bool MPreviousIsAnimatorTransitioning;
        protected bool MPreviouslyGrounded = true; // Whether or not Ellen was standing on the ground last frame.
        protected AnimatorStateInfo MPreviousNextStateInfo;
        protected bool MReadyToJump; // Whether or not the input state and Ellen are correct to allow jumping.
        protected Renderer[] MRenderers; // References used to make sure Renderers are reset properly. 
        protected bool MRespawning; // Whether Ellen is currently respawning.
        protected Quaternion MTargetRotation; // What rotation Ellen is aiming to have based on input.
        protected float MVerticalSpeed; // How fast Ellen is currently moving up or down.
        public static PlayerController Instance => SInstance;

        public bool Respawning => MRespawning;

        protected bool IsMoveInput => !Mathf.Approximately(MInput.MoveInput.sqrMagnitude, 0f);

        // Called by Ellen's Damageable when she is hurt.
        public void OnReceiveMessage(MessageType type, object sender, object data)
        {
            switch (type)
            {
                case MessageType.DAMAGED:
                {
                    var damageData = (Damageable.DamageMessage) data;
                    Damaged(damageData);
                }
                    break;
                case MessageType.DEAD:
                {
                    var damageData = (Damageable.DamageMessage) data;
                    Die(damageData);
                }
                    break;
            }
        }

        public void SetCanAttack(bool canAttack)
        {
            this.canAttack = canAttack;
        }

        // Called automatically by Unity when the script is first added to a gameobject or is reset from the context menu.
        private void Reset()
        {
            meleeWeapon = GetComponentInChildren<MeleeWeapon>();

            var footStepSource = transform.Find("FootstepSource");
            if (footStepSource != null)
                footstepPlayer = footStepSource.GetComponent<RandomAudioPlayer>();

            var hurtSource = transform.Find("HurtSource");
            if (hurtSource != null)
                hurtAudioPlayer = hurtSource.GetComponent<RandomAudioPlayer>();

            var landingSource = transform.Find("LandingSource");
            if (landingSource != null)
                landingPlayer = landingSource.GetComponent<RandomAudioPlayer>();

            cameraSettings = FindObjectOfType<CameraSettings>();

            if (cameraSettings != null)
            {
                if (cameraSettings.follow == null)
                    cameraSettings.follow = transform;

                if (cameraSettings.lookAt == null)
                    cameraSettings.follow = transform.Find("HeadTarget");
            }
        }

        // Called automatically by Unity when the script first exists in the scene.
        private void Awake()
        {
            MInput = GetComponent<PlayerInput>();
            MAnimator = GetComponent<Animator>();
            MCharCtrl = GetComponent<CharacterController>();

            meleeWeapon.SetOwner(gameObject);

            SInstance = this;
        }

        // Called automatically by Unity after Awake whenever the script is enabled. 
        private void OnEnable()
        {
            SceneLinkedSMB<PlayerController>.Initialise(MAnimator, this);

            MDamageable = GetComponent<Damageable>();
            MDamageable.onDamageMessageReceivers.Add(this);

            MDamageable.isInvulnerable = true;

            EquipMeleeWeapon(false);

            MRenderers = GetComponentsInChildren<Renderer>();
        }

        // Called automatically by Unity whenever the script is disabled.
        private void OnDisable()
        {
            MDamageable.onDamageMessageReceivers.Remove(this);

            for (var i = 0; i < MRenderers.Length; ++i) MRenderers[i].enabled = true;
        }

        // Called automatically by Unity once every Physics step.
        private void FixedUpdate()
        {
            CacheAnimatorState();

            UpdateInputBlocking();

            EquipMeleeWeapon(IsWeaponEquiped());

            MAnimator.SetFloat(_mHashStateTime,
                Mathf.Repeat(MAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime, 1f));
            MAnimator.ResetTrigger(_mHashMeleeAttack);

            if (MInput.Attack && canAttack)
                MAnimator.SetTrigger(_mHashMeleeAttack);

            CalculateForwardMovement();
            CalculateVerticalMovement();

            SetTargetRotation();

            if (IsOrientationUpdated() && IsMoveInput)
                UpdateOrientation();

            PlayAudio();

            TimeoutToIdle();

            MPreviouslyGrounded = MIsGrounded;
        }

        // Called at the start of FixedUpdate to record the current state of the base layer of the animator.
        private void CacheAnimatorState()
        {
            MPreviousCurrentStateInfo = MCurrentStateInfo;
            MPreviousNextStateInfo = MNextStateInfo;
            MPreviousIsAnimatorTransitioning = MIsAnimatorTransitioning;

            MCurrentStateInfo = MAnimator.GetCurrentAnimatorStateInfo(0);
            MNextStateInfo = MAnimator.GetNextAnimatorStateInfo(0);
            MIsAnimatorTransitioning = MAnimator.IsInTransition(0);
        }

        // Called after the animator state has been cached to determine whether this script should block user input.
        private void UpdateInputBlocking()
        {
            var inputBlocked = MCurrentStateInfo.tagHash == _mHashBlockInput && !MIsAnimatorTransitioning;
            inputBlocked |= MNextStateInfo.tagHash == _mHashBlockInput;
            MInput.playerControllerInputBlocked = inputBlocked;
        }

        // Called after the animator state has been cached to determine whether or not the staff should be active or not.
        private bool IsWeaponEquiped()
        {
            var equipped = MNextStateInfo.shortNameHash == _mHashEllenCombo1 ||
                           MCurrentStateInfo.shortNameHash == _mHashEllenCombo1;
            equipped |= MNextStateInfo.shortNameHash == _mHashEllenCombo2 ||
                        MCurrentStateInfo.shortNameHash == _mHashEllenCombo2;
            equipped |= MNextStateInfo.shortNameHash == _mHashEllenCombo3 ||
                        MCurrentStateInfo.shortNameHash == _mHashEllenCombo3;
            equipped |= MNextStateInfo.shortNameHash == _mHashEllenCombo4 ||
                        MCurrentStateInfo.shortNameHash == _mHashEllenCombo4;

            return equipped;
        }

        // Called each physics step with a parameter based on the return value of IsWeaponEquiped.
        private void EquipMeleeWeapon(bool equip)
        {
            meleeWeapon.gameObject.SetActive(equip);
            MInAttack = false;
            MInCombo = equip;

            if (!equip)
                MAnimator.ResetTrigger(_mHashMeleeAttack);
        }

        // Called each physics step.
        private void CalculateForwardMovement()
        {
            // Cache the move input and cap it's magnitude at 1.
            var moveInput = MInput.MoveInput;
            if (moveInput.sqrMagnitude > 1f)
                moveInput.Normalize();

            // Calculate the speed intended by input.
            MDesiredForwardSpeed = moveInput.magnitude * maxForwardSpeed;

            // Determine change to speed based on whether there is currently any move input.
            var acceleration = IsMoveInput ? KGroundAcceleration : KGroundDeceleration;

            // Adjust the forward speed towards the desired speed.
            MForwardSpeed = Mathf.MoveTowards(MForwardSpeed, MDesiredForwardSpeed, acceleration * Time.deltaTime);

            // Set the animator parameter to control what animation is being played.
            MAnimator.SetFloat(_mHashForwardSpeed, MForwardSpeed);
        }

        // Called each physics step.
        private void CalculateVerticalMovement()
        {
            // If jump is not currently held and Ellen is on the ground then she is ready to jump.
            if (!MInput.JumpInput && MIsGrounded)
                MReadyToJump = true;

            if (MIsGrounded)
            {
                // When grounded we apply a slight negative vertical speed to make Ellen "stick" to the ground.
                MVerticalSpeed = -gravity * KStickingGravityProportion;

                // If jump is held, Ellen is ready to jump and not currently in the middle of a melee combo...
                if (MInput.JumpInput && MReadyToJump && !MInCombo)
                {
                    // ... then override the previously set vertical speed and make sure she cannot jump again.
                    MVerticalSpeed = jumpSpeed;
                    MIsGrounded = false;
                    MReadyToJump = false;
                }
            }
            else
            {
                // If Ellen is airborne, the jump button is not held and Ellen is currently moving upwards...
                if (!MInput.JumpInput && MVerticalSpeed > 0.0f) MVerticalSpeed -= KJumpAbortSpeed * Time.deltaTime;

                // If a jump is approximately peaking, make it absolute.
                if (Mathf.Approximately(MVerticalSpeed, 0f)) MVerticalSpeed = 0f;

                // If Ellen is airborne, apply gravity.
                MVerticalSpeed -= gravity * Time.deltaTime;
            }
        }

        // Called each physics step to set the rotation Ellen is aiming to have.
        private void SetTargetRotation()
        {
            // Create three variables, move input local to the player, flattened forward direction of the camera and a local target rotation.
            var moveInput = MInput.MoveInput;
            var localMovementDirection = new Vector3(moveInput.x, 0f, moveInput.y).normalized;

            var forward = Quaternion.Euler(0f, cameraSettings.Current.m_XAxis.Value, 0f) * Vector3.forward;
            forward.y = 0f;
            forward.Normalize();

            Quaternion targetRotation;

            // If the local movement direction is the opposite of forward then the target rotation should be towards the camera.
            if (Mathf.Approximately(Vector3.Dot(localMovementDirection, Vector3.forward), -1.0f))
            {
                targetRotation = Quaternion.LookRotation(-forward);
            }
            else
            {
                // Otherwise the rotation should be the offset of the input from the camera's forward.
                var cameraToInputOffset = Quaternion.FromToRotation(Vector3.forward, localMovementDirection);
                targetRotation = Quaternion.LookRotation(cameraToInputOffset * forward);
            }

            // The desired forward direction of Ellen.
            var resultingForward = targetRotation * Vector3.forward;

            // If attacking try to orient to close enemies.
            if (MInAttack)
            {
                // Find all the enemies in the local area.
                var centre = transform.position + transform.forward * 2.0f + transform.up;
                var halfExtents = new Vector3(3.0f, 1.0f, 2.0f);
                var layerMask = 1 << LayerMask.NameToLayer("Enemy");
                var count = Physics.OverlapBoxNonAlloc(centre, halfExtents, MOverlapResult, targetRotation, layerMask);

                // Go through all the enemies in the local area...
                var closestDot = 0.0f;
                var closestForward = Vector3.zero;
                var closest = -1;

                for (var i = 0; i < count; ++i)
                {
                    // ... and for each get a vector from the player to the enemy.
                    var playerToEnemy = MOverlapResult[i].transform.position - transform.position;
                    playerToEnemy.y = 0;
                    playerToEnemy.Normalize();

                    // Find the dot product between the direction the player wants to go and the direction to the enemy.
                    // This will be larger the closer to Ellen's desired direction the direction to the enemy is.
                    var d = Vector3.Dot(resultingForward, playerToEnemy);

                    // Store the closest enemy.
                    if (d > KMinEnemyDotCoeff && d > closestDot)
                    {
                        closestForward = playerToEnemy;
                        closestDot = d;
                        closest = i;
                    }
                }

                // If there is a close enemy...
                if (closest != -1)
                {
                    // The desired forward is the direction to the closest enemy.
                    resultingForward = closestForward;

                    // We also directly set the rotation, as we want snappy fight and orientation isn't updated in the UpdateOrientation function during an atatck.
                    transform.rotation = Quaternion.LookRotation(resultingForward);
                }
            }

            // Find the difference between the current rotation of the player and the desired rotation of the player in radians.
            var angleCurrent = Mathf.Atan2(transform.forward.x, transform.forward.z) * Mathf.Rad2Deg;
            var targetAngle = Mathf.Atan2(resultingForward.x, resultingForward.z) * Mathf.Rad2Deg;

            MAngleDiff = Mathf.DeltaAngle(angleCurrent, targetAngle);
            MTargetRotation = targetRotation;
        }

        // Called each physics step to help determine whether Ellen can turn under player input.
        private bool IsOrientationUpdated()
        {
            var updateOrientationForLocomotion =
                !MIsAnimatorTransitioning && MCurrentStateInfo.shortNameHash == _mHashLocomotion ||
                MNextStateInfo.shortNameHash == _mHashLocomotion;
            var updateOrientationForAirborne =
                !MIsAnimatorTransitioning && MCurrentStateInfo.shortNameHash == _mHashAirborne ||
                MNextStateInfo.shortNameHash == _mHashAirborne;
            var updateOrientationForLanding =
                !MIsAnimatorTransitioning && MCurrentStateInfo.shortNameHash == _mHashLanding ||
                MNextStateInfo.shortNameHash == _mHashLanding;

            return updateOrientationForLocomotion || updateOrientationForAirborne || updateOrientationForLanding ||
                   MInCombo && !MInAttack;
        }

        // Called each physics step after SetTargetRotation if there is move input and Ellen is in the correct animator state according to IsOrientationUpdated.
        private void UpdateOrientation()
        {
            MAnimator.SetFloat(_mHashAngleDeltaRad, MAngleDiff * Mathf.Deg2Rad);

            var localInput = new Vector3(MInput.MoveInput.x, 0f, MInput.MoveInput.y);
            var groundedTurnSpeed = Mathf.Lerp(maxTurnSpeed, minTurnSpeed, MForwardSpeed / MDesiredForwardSpeed);
            var actualTurnSpeed = MIsGrounded
                ? groundedTurnSpeed
                : Vector3.Angle(transform.forward, localInput) * KInverseOneEighty * KAirborneTurnSpeedProportion *
                  groundedTurnSpeed;
            MTargetRotation =
                Quaternion.RotateTowards(transform.rotation, MTargetRotation, actualTurnSpeed * Time.deltaTime);

            transform.rotation = MTargetRotation;
        }

        // Called each physics step to check if audio should be played and if so instruct the relevant random audio player to do so.
        private void PlayAudio()
        {
            var footfallCurve = MAnimator.GetFloat(_mHashFootFall);

            if (footfallCurve > 0.01f && !footstepPlayer.playing && footstepPlayer.canPlay)
            {
                footstepPlayer.playing = true;
                footstepPlayer.canPlay = false;
                footstepPlayer.PlayRandomClip(MCurrentWalkingSurface, MForwardSpeed < 4 ? 0 : 1);
            }
            else if (footstepPlayer.playing)
            {
                footstepPlayer.playing = false;
            }
            else if (footfallCurve < 0.01f && !footstepPlayer.canPlay)
            {
                footstepPlayer.canPlay = true;
            }

            if (MIsGrounded && !MPreviouslyGrounded)
            {
                landingPlayer.PlayRandomClip(MCurrentWalkingSurface, MForwardSpeed < 4 ? 0 : 1);
                emoteLandingPlayer.PlayRandomClip();
            }

            if (!MIsGrounded && MPreviouslyGrounded && MVerticalSpeed > 0f) emoteJumpPlayer.PlayRandomClip();

            if (MCurrentStateInfo.shortNameHash == _mHashHurt && MPreviousCurrentStateInfo.shortNameHash != _mHashHurt
            ) hurtAudioPlayer.PlayRandomClip();

            if (MCurrentStateInfo.shortNameHash == _mHashEllenDeath &&
                MPreviousCurrentStateInfo.shortNameHash != _mHashEllenDeath) emoteDeathPlayer.PlayRandomClip();

            if (MCurrentStateInfo.shortNameHash == _mHashEllenCombo1 &&
                MPreviousCurrentStateInfo.shortNameHash != _mHashEllenCombo1 ||
                MCurrentStateInfo.shortNameHash == _mHashEllenCombo2 &&
                MPreviousCurrentStateInfo.shortNameHash != _mHashEllenCombo2 ||
                MCurrentStateInfo.shortNameHash == _mHashEllenCombo3 &&
                MPreviousCurrentStateInfo.shortNameHash != _mHashEllenCombo3 ||
                MCurrentStateInfo.shortNameHash == _mHashEllenCombo4 &&
                MPreviousCurrentStateInfo.shortNameHash != _mHashEllenCombo4)
                emoteAttackPlayer.PlayRandomClip();
        }

        // Called each physics step to count up to the point where Ellen considers a random idle.
        private void TimeoutToIdle()
        {
            var inputDetected = IsMoveInput || MInput.Attack || MInput.JumpInput;
            if (MIsGrounded && !inputDetected)
            {
                MIdleTimer += Time.deltaTime;

                if (MIdleTimer >= idleTimeout)
                {
                    MIdleTimer = 0f;
                    MAnimator.SetTrigger(_mHashTimeoutToIdle);
                }
            }
            else
            {
                MIdleTimer = 0f;
                MAnimator.ResetTrigger(_mHashTimeoutToIdle);
            }

            MAnimator.SetBool(_mHashInputDetected, inputDetected);
        }

        // Called each physics step (so long as the Animator component is set to Animate Physics) after FixedUpdate to override root motion.
        private void OnAnimatorMove()
        {
            Vector3 movement;

            // If Ellen is on the ground...
            if (MIsGrounded)
            {
                // ... raycast into the ground...
                RaycastHit hit;
                var ray = new Ray(transform.position + Vector3.up * KGroundedRayDistance * 0.5f, -Vector3.up);
                if (Physics.Raycast(ray, out hit, KGroundedRayDistance, Physics.AllLayers,
                    QueryTriggerInteraction.Ignore))
                {
                    // ... and get the movement of the root motion rotated to lie along the plane of the ground.
                    movement = Vector3.ProjectOnPlane(MAnimator.deltaPosition, hit.normal);

                    // Also store the current walking surface so the correct audio is played.
                    var groundRenderer = hit.collider.GetComponentInChildren<Renderer>();
                    MCurrentWalkingSurface = groundRenderer ? groundRenderer.sharedMaterial : null;
                }
                else
                {
                    // If no ground is hit just get the movement as the root motion.
                    // Theoretically this should rarely happen as when grounded the ray should always hit.
                    movement = MAnimator.deltaPosition;
                    MCurrentWalkingSurface = null;
                }
            }
            else
            {
                // If not grounded the movement is just in the forward direction.
                movement = MForwardSpeed * transform.forward * Time.deltaTime;
            }

            // Rotate the transform of the character controller by the animation's root rotation.
            MCharCtrl.transform.rotation *= MAnimator.deltaRotation;

            // Add to the movement with the calculated vertical speed.
            movement += MVerticalSpeed * Vector3.up * Time.deltaTime;

            // Move the character controller.
            MCharCtrl.Move(movement);

            // After the movement store whether or not the character controller is grounded.
            MIsGrounded = MCharCtrl.isGrounded;

            // If Ellen is not on the ground then send the vertical speed to the animator.
            // This is so the vertical speed is kept when landing so the correct landing animation is played.
            if (!MIsGrounded)
                MAnimator.SetFloat(_mHashAirborneVerticalSpeed, MVerticalSpeed);

            // Send whether or not Ellen is on the ground to the animator.
            MAnimator.SetBool(_mHashGrounded, MIsGrounded);
        }

        // This is called by an animation event when Ellen swings her staff.
        public void MeleeAttackStart(int throwing = 0)
        {
            meleeWeapon.BeginAttack(throwing != 0);
            MInAttack = true;
        }

        // This is called by an animation event when Ellen finishes swinging her staff.
        public void MeleeAttackEnd()
        {
            meleeWeapon.EndAttack();
            MInAttack = false;
        }

        // This is called by Checkpoints to make sure Ellen respawns correctly.
        public void SetCheckpoint(Checkpoint checkpoint)
        {
            if (checkpoint != null)
                MCurrentCheckpoint = checkpoint;
        }

        // This is usually called by a state machine behaviour on the animator controller but can be called from anywhere.
        public void Respawn()
        {
            StartCoroutine(RespawnRoutine());
        }

        protected IEnumerator RespawnRoutine()
        {
            // Wait for the animator to be transitioning from the EllenDeath state.
            while (MCurrentStateInfo.shortNameHash != _mHashEllenDeath || !MIsAnimatorTransitioning)
                yield return null;

            // Wait for the screen to fade out.
            yield return StartCoroutine(ScreenFader.FadeSceneOut());
            while (ScreenFader.IsFading) yield return null;

            // Enable spawning.
            var spawn = GetComponentInChildren<EllenSpawn>();
            spawn.enabled = true;

            // If there is a checkpoint, move Ellen to it.
            if (MCurrentCheckpoint != null)
            {
                transform.position = MCurrentCheckpoint.transform.position;
                transform.rotation = MCurrentCheckpoint.transform.rotation;
            }
            else
            {
                Debug.LogError(
                    "There is no Checkpoint set, there should always be a checkpoint set. Did you add a checkpoint at the spawn?");
            }

            // Set the Respawn parameter of the animator.
            MAnimator.SetTrigger(_mHashRespawn);

            // Start the respawn graphic effects.
            spawn.StartEffect();

            // Wait for the screen to fade in.
            // Currently it is not important to yield here but should some changes occur that require waiting until a respawn has finished this will be required.
            yield return StartCoroutine(ScreenFader.FadeSceneIn());

            MDamageable.ResetDamage();
        }

        // Called by a state machine behaviour on Ellen's animator controller.
        public void RespawnFinished()
        {
            MRespawning = false;

            //we set the damageable invincible so we can't get hurt just after being respawned (feel like a double punitive)
            MDamageable.isInvulnerable = false;
        }

        // Called by OnReceiveMessage.
        private void Damaged(Damageable.DamageMessage damageMessage)
        {
            // Set the Hurt parameter of the animator.
            MAnimator.SetTrigger(_mHashHurt);

            // Find the direction of the damage.
            var forward = damageMessage.damageSource - transform.position;
            forward.y = 0f;

            var localHurt = transform.InverseTransformDirection(forward);

            // Set the HurtFromX and HurtFromY parameters of the animator based on the direction of the damage.
            MAnimator.SetFloat(_mHashHurtFromX, localHurt.x);
            MAnimator.SetFloat(_mHashHurtFromY, localHurt.z);

            // Shake the camera.
            CameraShake.Shake(CameraShake.k_PlayerHitShakeAmount, CameraShake.k_PlayerHitShakeTime);

            // Play an audio clip of being hurt.
            if (hurtAudioPlayer != null) hurtAudioPlayer.PlayRandomClip();
        }

        // Called by OnReceiveMessage and by DeathVolumes in the scene.
        public void Die(Damageable.DamageMessage damageMessage)
        {
            MAnimator.SetTrigger(_mHashDeath);
            MForwardSpeed = 0f;
            MVerticalSpeed = 0f;
            MRespawning = true;
            MDamageable.isInvulnerable = true;
        }
    }
}