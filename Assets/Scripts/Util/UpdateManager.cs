using System;
using UnityEngine;

namespace Utils
{
	public interface IUpdateManager
	{
		event Action OnUpdate;
	}
	
	public class UpdateManager : MonoBehaviour, IUpdateManager
	{
		public event Action OnUpdate;
        
		private void Update()
		{	
			OnUpdate?.Invoke();
		}
	}
	

}
