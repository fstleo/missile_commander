using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Entities
{
	public class City : MonoBehaviour
	{		
		private IDestroyable _destroyable;
		
		[SerializeField] private GameObject _healthyCity;

		[SerializeField] private GameObject _destroyedCity;

		public void Initialize()
		{
			_destroyable = GetComponent<IDestroyable>();
			_destroyable.OnDestroy += (owner) => Destroy();
		}
		
		public void Destroy()
		{
			_destroyedCity.SetActive(true);
			_healthyCity.SetActive(false);
		}

	}
}