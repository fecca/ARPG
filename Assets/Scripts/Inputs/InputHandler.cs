using ARPG.Zenject;
using UnityEngine;
using Zenject;

namespace ARPG.Inputs
{
	public class InputHandler : MonoBehaviour
	{
		private ISignalBusAdapter _signalBusAdapter;

		[Inject]
		public void Construct(ISignalBusAdapter signalBusAdapter)
		{
			_signalBusAdapter = signalBusAdapter;
		}

		private void Update()
		{
			if (Input.GetMouseButtonDown(0))
			{
				_signalBusAdapter.Fire(new MouseButtonClickedSignal(Input.mousePosition));
			}
		}
	}
}