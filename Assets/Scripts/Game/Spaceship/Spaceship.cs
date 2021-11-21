using UnityEngine;

namespace Asteroids.Game
{
	public class Spaceship : MonoBehaviour
	{
		[SerializeField] private SpaceshipSettings _settings;

		private ISpaceshipGun _spaceshipGun;
		private SpaceshipEngene _spaceshipEngene;

		private void Start()
		{
			_spaceshipGun = GetComponent<SpaceshipGun>();
			_spaceshipEngene = new SpaceshipEngene();
			_spaceshipEngene.Init(transform, _settings);
		}

		public void Move(bool moving)
		{
			_spaceshipEngene.Move(moving);
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
	}
}