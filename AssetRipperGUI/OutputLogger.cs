using AssetRipper.Logging;
using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

namespace AssetRipperGUI
{
	public sealed class OutputLogger : ILogger
	{
		private readonly TextBox m_textBox;

		public OutputLogger(TextBox textBox)
		{
			if (textBox == null)
			{
				throw new ArgumentNullException(nameof(textBox));
			}
			m_textBox = textBox;
		}

		public void Log(LogType type, LogCategory category, string message)
		{
			if (Application.Current == null)
			{
				return;
			}
			if (Thread.CurrentThread == Application.Current.Dispatcher.Thread)
			{
				LogInner(type, category, message);
			}
			else
			{
				Application.Current.Dispatcher.Invoke(() => LogInner(type, category, message));
			}
		}

		private void LogInner(LogType type, LogCategory category, string message)
		{
#if !DEBUG
			if (type == LogType.Debug)
			{
				return;
			}
#endif

			m_textBox.AppendText(category.ToString());
			switch (type)
			{
				case LogType.Warning:
				case LogType.Error:
					m_textBox.AppendText(" [");
					m_textBox.AppendText(type.ToString());
					m_textBox.AppendText("]");
					break;
			}
			m_textBox.AppendText(": ");
			m_textBox.AppendText(message);
			m_textBox.AppendText(Environment.NewLine);
		}

		public void BlankLine(int numLines)
		{
			for(int i = 0; i < numLines; i++)
				m_textBox.AppendText(Environment.NewLine);
		}
	}
}
