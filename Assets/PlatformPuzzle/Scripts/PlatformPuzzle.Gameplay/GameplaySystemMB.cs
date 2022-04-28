using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace PlatformPuzzle.Gameplay
{
    internal class GameplaySystemMB : MiddleMast.GameplayFramework.System
    {
        [SerializeField]
        private UnityEvent _onLost;

        [SerializeField]
        private UnityEvent _onWinned;

        private LevelData _levelData;
        private PlayerFSMMB _playerFsm;

        public override void Setup()
        {
            ILevelGenerator levelGenerator = GetComponentInChildren<ILevelGenerator>();
            levelGenerator.Setup();

            IProgressSaver progressSaver = GetComponentInChildren<IProgressSaver>();
            int currentLevel = progressSaver.Load();

            LevelGeneratorData generatorData = new LevelGeneratorData(currentLevel);
            _levelData = levelGenerator.Generate(generatorData);

            ILevelMatchItemsRandomizer matchItemsRandomizer =
                GetComponentInChildren<ILevelMatchItemsRandomizer>();

            matchItemsRandomizer.Randomize(_levelData);

            _playerFsm = GetComponentInChildren<PlayerFSMMB>();
            _playerFsm.Setup();
        }

        public void OnPlatformRotationStarted()
        {
            _playerFsm.BlockInput();
        }

        public void OnPlatformRotationEnded()
        {
            if (CheckLossCondition())
            {
                _onLost?.Invoke();
                return;
            }

            if (CheckWinCondition())
            {
                _onWinned?.Invoke();
                return;
            }

            _playerFsm.AllowInput();
        }

        private bool CheckWinCondition()
        {
            foreach (PlatformMB platform in _levelData.Platforms)
            {
                if (platform.GetEnabledMatchItemsCount() > 0)
                {
                    return false;
                }
            }

            return true;
        }

        private bool CheckLossCondition()
        {
            PlatformMB[] platformsWithMatchItems = _levelData.Platforms
                .Where(x => x.CheckHasEnabledMatchItem())
                .ToArray();

            foreach (PlatformMB platform in platformsWithMatchItems)
            {
                if (!CheckIfPlatformIsSolvable(platform))
                {
                    return true;
                }
            }

            return false;
        }

        private bool CheckIfPlatformIsSolvable(PlatformMB platform)
        {
            List<MatchItemMB> matchItems = platform.GetEnabledMatchItems().ToList();

            IEnumerable<MatchItemMB> otherMatchItems = platform
                .GetAttachedPlatforms()
                .SelectMany(x => x.GetEnabledMatchItems());

            foreach (MatchItemMB matchItem in matchItems)
            {
                bool foundMatch = false;
                foreach (MatchItemMB otherMatchItem in otherMatchItems)
                {
                    if (matchItem.CompareWith(otherMatchItem))
                    {
                        foundMatch = true;
                        break;
                    }
                }

                if (!foundMatch)
                {
                    return false;
                }
            }

            return true;
        }
    }
}

