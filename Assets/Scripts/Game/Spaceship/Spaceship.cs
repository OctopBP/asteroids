using Asteroids.Data;
using UnityEngine;

namespace Asteroids.Game
{
	public class Spaceship : MonoBehaviour, ICollisionRactive
	{
		[SerializeField] private SpaceshipSettings _settings;
		[SerializeField] private SpaceshipEngene _spaceshipEngene;
		[SerializeField] private SpriteRenderer _spriteRenderer;
		[SerializeField] private ParticleSystem _destroyPS;

		private ISpaceshipGun _spaceshipGun;
		private ISpaceshipGun _spaceshipLaser;
		
		private GameState _gameState;
		private GameData _gameData;
		private bool _isAlive;

		private void Awake()
		{
			_spaceshipGun = GetComponent<SpaceshipGun>();
			_spaceshipLaser = GetComponent<SpaceshipLaser>();
		}

		public void Init(GameState gameState, GameData gameData)
		{
			_gameState = gameState;
			_gameData = gameData;

			transform.position = Vector3.zero;
			transform.rotation = Quaternion.identity;

			_spaceshipEngene.Init(transform, _settings, gameData);
			_spaceshipGun.Init(gameData);
			_spaceshipLaser.Init(gameData);

			_destroyPS.gameObject.SetActive(false);
			_spriteRenderer.enabled = true;

			_isAlive = true;
		}

		public void Move(bool moving)
		{
			_spaceshipEngene.Move(moving);
			_gameData.SpaceshipData.SetCoords(transform.position, transform.eulerAngles.z);
		}

		public void ClampPosition(Vector2 screenSize)
		{
			_spaceshipEngene.ClampPosition(screenSize);
		}

		public void Turn(float turn)
		{
			_spaceshipEngene.Turn(turn);
		}

		public void Fire(bool isFire)
		{
			if (isFire) _spaceshipGun.Fire();
		}
		
		public void Laser(bool isLaser)
		{
			if (isLaser) _spaceshipLaser.Fire();
		}

		public void OnCollision(Vector3 from)
		{
			if (!_isAlive) return;
			_isAlive = false;

			DestroyShip(from);
			_gameState.Lose();
		}

		private void DestroyShip(Vector3 from)
		{
			PlayPS(from);
			_spriteRenderer.enabled = false;
			_spaceshipEngene.Stop();
		}

		private void PlayPS(Vector3 from)
		{
			Vector3 direction = (from + _spaceshipEngene.Speed).normalized;
			_destroyPS.transform.up = direction;
			_destroyPS.gameObject.SetActive(true);
			_destroyPS.Play();
		}
	}
}
