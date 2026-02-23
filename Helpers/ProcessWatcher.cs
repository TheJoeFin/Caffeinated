using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace Caffeinated.Helpers;

/// <summary>
/// Periodically checks whether any of the configured process names are running.
/// Fires <see cref="StateChanged"/> when the running state transitions.
/// </summary>
internal sealed class ProcessWatcher : IDisposable
{
    private readonly Timer _timer;
    private bool _wasAnyRunning;
    private List<string> _processNames = [];

    public event EventHandler<ProcessWatchStateChangedEventArgs>? StateChanged;

    public List<string> ProcessNames
    {
        get => _processNames;
        set
        {
            _processNames = value ?? [];

            // Re-evaluate immediately when the list changes
            CheckProcesses();
        }
    }

    public bool IsAnyWatchedProcessRunning => _wasAnyRunning;

    public ProcessWatcher(int intervalMs = 5000)
    {
        _timer = new Timer { Interval = intervalMs };
        _timer.Tick += Timer_Tick;
    }

    public void Start() => _timer.Start();

    public void Stop()
    {
        _timer.Stop();
        _wasAnyRunning = false;
    }

    private void Timer_Tick(object? sender, EventArgs e) => CheckProcesses();

    private void CheckProcesses()
    {
        if (_processNames.Count == 0)
        {
            if (_wasAnyRunning)
            {
                _wasAnyRunning = false;
                StateChanged?.Invoke(this, new ProcessWatchStateChangedEventArgs(false, null));
            }

            return;
        }

        string? matchedName = null;

        foreach (string name in _processNames)
        {
            if (string.IsNullOrWhiteSpace(name))
                continue;

            try
            {
                Process[] procs = Process.GetProcessesByName(name);
                bool found = procs.Length > 0;

                foreach (Process p in procs)
                    p.Dispose();

                if (found)
                {
                    matchedName = name;

                    break;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"ProcessWatcher: Error checking '{name}': {ex.Message}");
            }
        }

        bool isAnyRunning = matchedName is not null;

        if (isAnyRunning != _wasAnyRunning)
        {
            _wasAnyRunning = isAnyRunning;
            StateChanged?.Invoke(this, new ProcessWatchStateChangedEventArgs(isAnyRunning, matchedName));
        }
    }

    public void Dispose()
    {
        _timer.Stop();
        _timer.Dispose();
    }
}

internal sealed class ProcessWatchStateChangedEventArgs : EventArgs
{
    public bool IsAnyRunning { get; }
    public string? MatchedProcessName { get; }

    public ProcessWatchStateChangedEventArgs(bool isAnyRunning, string? matchedProcessName)
    {
        IsAnyRunning = isAnyRunning;
        MatchedProcessName = matchedProcessName;
    }
}
