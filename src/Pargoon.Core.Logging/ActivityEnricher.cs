namespace Pargoon.Core.Logging;

using System.Diagnostics;
using global::Serilog.Core;
using global::Serilog.Events;

public class ActivityEnricher : ILogEventEnricher
{
  public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
  {
	var activity = Activity.Current;

	if (activity is null)
	{
	  return;
	}

	logEvent.AddPropertyIfAbsent(new LogEventProperty("SpanId", new ScalarValue(activity.GetSpanId())));
	logEvent.AddPropertyIfAbsent(new LogEventProperty("TraceId", new ScalarValue(activity.GetTraceId())));
	logEvent.AddPropertyIfAbsent(new LogEventProperty("ParentId", new ScalarValue(activity.GetParentId())));
  }
}
