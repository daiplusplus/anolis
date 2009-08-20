using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Anolis.Core.Utility;
using System.Globalization;

namespace Anolis.Packager {
	
	public partial class ProgressForm : Form {
		
		private RateCalculator _calculator;
		
		public ProgressForm() {
			InitializeComponent();
			
			_calculator = new RateCalculator();
			
			this.__timer.Tick += new EventHandler(__timer_Tick);

			this.VisibleChanged += new EventHandler(ProgressForm_VisibleChanged);
		}
		
		private void ProgressForm_VisibleChanged(object sender, EventArgs e) {
			
			__timer.Enabled = Visible;
		}
		
		// when the form is closed the __timer is disposed and stops ticking
		
		private void __timer_Tick(Object sender, EventArgs e) {
			
			SetTimeRemainingString();
		}
		
		private void SetTimeRemainingString() {
			
			Int64 rate    = _calculator.GetRate();
			TimeSpan time = _calculator.GetTimeRemaining();
			
			String timeS = time == TimeSpan.MaxValue ? "Indeterminate" : time.ToString() + " remaining";
			
			__time.Text = String.Format( CultureInfo.InvariantCulture, "{0}% - {1} - {2}KB/{3}KB at {4}KB/s", __progress.Value, timeS, _calculator.LastValue / 1024, _calculator.Total / 1024, rate / 1024 );
		}
		
		public String Status {
			get { return __status.Text; }
			set { __status.Text = value; }
		}
		
		public void SetProgress(Int32 canonicalPercentage, Int64 complete, Int64 total) {
			
			if( canonicalPercentage == 0 ) _calculator.Reset( total );
			
			if( canonicalPercentage == -1 ) {
				
				__progress.Value = canonicalPercentage;
				__progress.Style = ProgressBarStyle.Marquee;
				return;
			}
			
			__progress.Style = ProgressBarStyle.Continuous;
			__progress.Value = canonicalPercentage;
			
			_calculator.Add( complete );
		}
		
	}
}
