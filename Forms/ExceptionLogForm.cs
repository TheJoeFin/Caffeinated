using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Caffeinated.Helpers;

namespace Caffeinated;

public partial class ExceptionLogForm : BaseForm {
    public ExceptionLogForm() : base() {
        InitializeComponent();
        LoadExceptions();
    }

    protected override void UpdateTheme() {
        base.UpdateTheme();
        
        exceptionListView.BackColor = BackColor;
        exceptionListView.ForeColor = ForeColor;
        detailsTextBox.BackColor = BackColor;
        detailsTextBox.ForeColor = ForeColor;
        
        exceptionListView.Invalidate();
        Refresh();
    }

    private void LoadExceptions() {
        exceptionListView.Items.Clear();
        var exceptions = ExceptionLogService.GetExceptions();

        if (!exceptions.Any()) {
            var item = exceptionListView.Items.Add("No exceptions logged");
            item.Tag = null;
        }
        else {
            foreach (var entry in exceptions) {
                var item = exceptionListView.Items.Add(
                    $"{entry.Timestamp:yyyy-MM-dd HH:mm:ss} - {entry.Exception.GetType().Name}"
                );
                item.Tag = entry;
            }
        }
    }

    private void exceptionListView_SelectedIndexChanged(object? sender, EventArgs e) {
        if (exceptionListView.SelectedItems.Count == 0 || 
            exceptionListView.SelectedItems[0].Tag is not ExceptionEntry entry) {
            detailsTextBox.Text = "";
            return;
        }

        detailsTextBox.Text = $"Exception: {entry.Exception.GetType().FullName}\r\n" +
                             $"Time: {entry.Timestamp:yyyy-MM-dd HH:mm:ss}\r\n" +
                             $"Message: {entry.Exception.Message}\r\n\r\n" +
                             $"Stack Trace:\r\n{entry.Exception.StackTrace}";
    }

    private void clearButton_Click(object? sender, EventArgs e) {
        var result = MessageBox.Show(
            "Clear all exception logs?",
            "Caffeinated",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question
        );

        if (result == DialogResult.Yes) {
            ExceptionLogService.Clear();
            LoadExceptions();
            detailsTextBox.Text = "";
        }
    }

    private void closeButton_Click(object? sender, EventArgs e) {
        Close();
    }

    private void refreshButton_Click(object? sender, EventArgs e) {
        LoadExceptions();
    }
}
