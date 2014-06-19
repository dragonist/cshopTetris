using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsApplication7
{
	public class Member
	{
		public string name;
		public int score;
		public Member next;

		public Member(string name, int score)
		{
			this.name = name;
			this.score = score;
		}
	}

	public class Lists
	{
		public Member first;

		public Lists()
		{
			first = null;
		}

		public void Add(Member new_mem)
		{
			if (first == null)
			{
				first = new_mem;
				first.next = null;
			}
			else if (first.score < new_mem.score)
			{
				new_mem.next = first;
				first = new_mem;
			}
			else
			{
				for (Member t = first; t != null; t = t.next)
				{

					if (t.next == null)
					{
						t.next = new_mem;
						new_mem.next = null;

					}
					else if (t.score == new_mem.score || t.next.score < new_mem.score)
					{


						new_mem.next = t.next;

						t.next = new_mem;
						break;
					}
				}
			}

		}

	}
}
