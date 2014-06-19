using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication7
{
	public partial class Form2 : Form
	{
		public Form2(Lists list)
		{
			InitializeComponent();

			int num = 0;

			for (Member t = list.first; t != null; t = t.next)
			{
				num++;
				
				listBox1.Items.Add(num + "\t"+t.name + "\t" + t.score);
			}

		}
	
	}
}
