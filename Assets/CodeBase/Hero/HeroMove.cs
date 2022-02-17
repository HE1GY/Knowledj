using System;
using CodeBase.CameraLogic;
using CodeBase.Data;
using CodeBase.Infrastructure;
using CodeBase.Infrastructure.Services;
using CodeBase.Services.Input;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Hero
{
    public class HeroMove : MonoBehaviour,ISavedProgress
    {
        public CharacterController CharacterController;
        public float MovementSpeed = 4.0f;
        
        private IInputService _inputService;
        private Camera _camera;
        private HeroAnimator _heroAnimator;

        private void Awake()
        {
            _inputService = AllServices.Container.Single<IInputService>();
            _camera=Camera.main;
            _heroAnimator = GetComponent<HeroAnimator>();
        }
        

        private void Update()
        {
            Vector3 movementVector = Vector3.zero;

            if (_inputService.Axis.sqrMagnitude > Constants.Epsilon)
            {
                //Трансформируем экранныые координаты вектора в мировые
                movementVector = _camera.transform.TransformDirection(_inputService.Axis);
                movementVector.y = 0;
                movementVector.Normalize();

                transform.forward = movementVector;
            }

            movementVector += Physics.gravity;
            
            CharacterController.Move(MovementSpeed * movementVector * Time.deltaTime);
        }

        public void LoadProgress(PlayerProgress progress)
        {
            if (CurrentLevel() == progress.WorldData.PositionOnLevel.Level)
            {
                Vector3Data savePosition = progress.WorldData.PositionOnLevel.Position;
                if (savePosition != null)
                {
                    Warp(savePosition);
                }
            }
        }

        private void Warp(Vector3Data savePosition)
        {
            CharacterController.enabled = false;
            transform.position = savePosition.AsUnityVector();
            CharacterController.enabled = true; 
        }

        private static string CurrentLevel  () => SceneManager.GetActiveScene().name;

        public void UpdateProgress(PlayerProgress progress)
        {
            progress.WorldData.PositionOnLevel =new PositionOnLevel(transform.position.AsVectorData(),CurrentLevel());
        }
    }
}