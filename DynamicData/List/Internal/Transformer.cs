using System;

namespace DynamicData.Internal
{
	internal class Transformer<TSource, TDestination>
	{
		private readonly Func<TSource, TDestination> _factory;
		private readonly ChangeAwareCollection<TDestination> _filtered = new ChangeAwareCollection<TDestination>();

		public Transformer(Func<TSource, TDestination> factory)
		{
			if (factory == null) throw new ArgumentNullException("factory");
			_factory = factory;
		}
		
		public IChangeSet<TDestination> Process(IChangeSet<TSource> changes)
		{
			_filtered.Transform(changes, _factory);
			return _filtered.CaptureChanges();
		}
	}
}