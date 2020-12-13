using System;
using Zenject;

namespace ARPG.Zenject
{
	public class SignalBusAdapter : ISignalBusAdapter
	{
		private readonly SignalBus _signalBus;

		public SignalBusAdapter(SignalBus signalBus)
		{
			_signalBus = signalBus;
		}

		public void Subscribe<TSignal>(Action<TSignal> callback)
		{
			_signalBus.Subscribe(callback);
		}

		public void Fire<TSignal>(TSignal signal)
		{
			_signalBus.Fire(signal);
		}
	}
}