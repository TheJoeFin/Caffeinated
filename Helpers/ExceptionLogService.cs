using System;
using System.Collections.Generic;
using System.Linq;

namespace Caffeinated.Helpers;

public class ExceptionLogService {
    private static readonly object lockObject = new();
    private static readonly List<ExceptionEntry> exceptions = [];
    private const int MaxExceptions = 10;

    public static void LogException(Exception exception) {
        lock (lockObject) {
            exceptions.Insert(0, new ExceptionEntry {
                Exception = exception,
                Timestamp = DateTime.Now
            });

            if (exceptions.Count > MaxExceptions) {
                exceptions.RemoveAt(exceptions.Count - 1);
            }
        }
    }

    public static List<ExceptionEntry> GetExceptions() {
        lock (lockObject) {
            return [.. exceptions];
        }
    }

    public static void Clear() {
        lock (lockObject) {
            exceptions.Clear();
        }
    }
}

public class ExceptionEntry {
    public required Exception Exception { get; init; }
    public DateTime Timestamp { get; init; }
}
