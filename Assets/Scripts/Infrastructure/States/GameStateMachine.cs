using Infrastructure.Data;
using Infrastructure.Factory;
using Infrastructure.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Infrastructure.States
{
    public class GameStateMachine : IGameStateMachine
    {
        private SceneLoader sceneLoader;
        private readonly Dictionary<Type, IState> _states;

        public GameStateMachine(ICoroutineRunner coroutineRunner ,LoadingCurtain loadingCurtain)
        {
            sceneLoader = new SceneLoader(coroutineRunner, this, loadingCurtain);

            GameData gameData = new GameData();
            LootGeneratorService lootGeneratorService = new LootGeneratorService();
            SaveLoadService saveLoadService = new SaveLoadService();

            InputController inputController = new InputController();
            inputController.Enable();
            InputServicePlayer1 inputPlayer1 = new InputServicePlayer1(inputController);
            InputServicePlayer2 inputPlayer2 = new InputServicePlayer2(inputController);

            ItemsService itemsService = new ItemsService();
            UiFactory uiFactory = new UiFactory(inputPlayer1, this, saveLoadService);
            GameObjectsFactory gameObjectsFactory = new GameObjectsFactory(inputPlayer1, inputPlayer2, itemsService, lootGeneratorService);

            _states = new Dictionary<Type, IState>
            {
                [typeof(InitLevelState)] = new InitLevelState(this, uiFactory, gameObjectsFactory, itemsService, gameData, saveLoadService),
                [typeof(PauseResumeState)] = new PauseResumeState(),
            };
        }

        public void Enter<TState>() where TState : class, IState
        {
            IState state = GetState<TState>();
            state.Enter();
        }

        public void EnterScene(string sceneName)
        {
            sceneLoader.Enter(sceneName);
        }

        public void EnterScene(int indexScene)
        {
            sceneLoader.Enter(indexScene);
        }

        private TState GetState<TState>() where TState : class
        {
            return _states[typeof(TState)] as TState;
        }
    }
}

