using System.ComponentModel;
using System.Runtime.CompilerServices;
using InternalAssets.Scripts.Characters.Hero;
using InternalAssets.Scripts.Data;
using InternalAssets.Scripts.Infrastructure.Factories;
using InternalAssets.Scripts.Infrastructure.Services.PersistentProgress;
using InternalAssets.Scripts.Infrastructure.Services.SaveLoad;
using InternalAssets.Scripts.Map.Grids;
using JetBrains.Annotations;
using UnityEngine;


namespace InternalAssets.Scripts.Cheats
{
    public class CheatsThroughDI : INotifyPropertyChanged
    {
        private readonly IPersistentProgressService _progressService;
		private readonly ISaveLoadService _saveLoadService;
		private readonly IGameFactory _gameFactory;
		public static CheatsThroughDI Instance;

		private HeroMove _heroMove;
		private HeroMove HeroMove => _heroMove ?? (_heroMove = _gameFactory.HeroGameObject.GetComponent<HeroMove>());
		private float _nextNormalizeDirectionX;
		private float _nextNormalizeDirectionY;
		private bool _isMoving;

        public CheatsThroughDI(IPersistentProgressService progressService, ISaveLoadService saveLoadService, IGameFactory gameFactory)
		{
			_progressService = progressService;
			_saveLoadService = saveLoadService;
			_gameFactory = gameFactory;
			Instance = this;
		}

		#region HeroMove

		[Category("Hero Move"), DisplayName("NextNormalizeDirectionX")]
		public float NextNormalizeDirectionX
		{
			get => _nextNormalizeDirectionX;
			set
			{
				_nextNormalizeDirectionX = value;
				OnPropertyChanged(nameof(NextNormalizeDirectionX));
			}
		}

		[Category("Hero Move"), DisplayName("NextNormalizeDirectionY")]
		public float NextNormalizeDirectionY
		{
			get => _nextNormalizeDirectionY;
			set
			{
				_nextNormalizeDirectionY = value;
				OnPropertyChanged(nameof(NextNormalizeDirectionY));
			}
		}

		[Category("Hero Move"), DisplayName("IsMoving")]
		public bool IsMoving
		{
			get => _isMoving;
			set
			{
				_isMoving = value;
				OnPropertyChanged(nameof(IsMoving));
			}
		} 

		#endregion
		
			

		[Category("LOOT"), DisplayName("Add 100 Gold")]
        public void AddGold()
        {
            _progressService.Progress.WorldData.LootData.Collect(new Loot(){Value = 100});
			_saveLoadService.SaveProgress();
        }

		[Category("GamePlay"), DisplayName("Fog of war")]
		public bool IsFogOfWar
		{
			get
			{
				GridsManager gridsManager = Object.FindObjectOfType<GridsManager>();
				return gridsManager.IsActiveFogOfWar;
			}
			set
			{
				GridsManager gridsManager = Object.FindObjectOfType<GridsManager>();
				gridsManager.SetActiveFogOfWar(value);
			}
		}

		#region NeedForUpdateAlwase

		public event SROptionsPropertyChanged PropertyChanged;

		[NotifyPropertyChangedInvocator]
		public void OnPropertyChanged(string propertyName)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, propertyName);
			}

			if (InterfacePropertyChangedEventHandler != null)
			{
				InterfacePropertyChangedEventHandler(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		private event PropertyChangedEventHandler InterfacePropertyChangedEventHandler;

		event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged
		{
			add { InterfacePropertyChangedEventHandler += value; }
			remove { InterfacePropertyChangedEventHandler -= value; }
		}

		#endregion
		
	}
}