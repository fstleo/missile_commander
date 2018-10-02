using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Util.ObjectPool
{
	public class AutoReturnToPool : PoolItem
	{
		[SerializeField] private float _returnTime;
		
		private float _timer = 0;

		private void Update()
		{
			_timer -= Time.deltaTime;
			if (_timer < 0)
			{
				Return();
			}
		}

		public override void ResetItem()
		{
			base.ResetItem();
			_timer = _returnTime;			
		}
	}
}