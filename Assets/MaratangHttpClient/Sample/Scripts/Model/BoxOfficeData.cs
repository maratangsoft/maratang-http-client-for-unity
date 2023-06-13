using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MaratangHttp
{
	[Serializable]
	public class BoxOfficeData
	{
		private Boxofs boxofs;

		public Boxofs Boxofs { get => boxofs; }

		public override string ToString()
		{
			return JsonUtility.ToJson(this, true);
		}
	}

	[Serializable]
	public class Boxofs
	{
		private string basedate;
		private List<BoxOffice> boxof;

		public string Basedate { get => basedate; }
		public List<BoxOffice> Boxof { get => boxof; }
	}

	[Serializable]
	public class BoxOffice
	{
		private string cate; 
		private string rnum; 
		private string prfnm; 
		private string prfpd; 
		private string prfplcnm; 
		private string seatcnt; 
		private string prfdtcnt; 
		private string area; 
		private string poster; 
		private string mt20id;

		public string Cate { get => cate; }
		public string Rnum { get => rnum; }
		public string Prfnm { get => prfnm; }
		public string Prfpd { get => prfpd; }
		public string Prfplcnm { get => prfplcnm; }
		public string Seatcnt { get => seatcnt; }
		public string Prfdtcnt { get => prfdtcnt; }
		public string Area { get => area; }
		public string Poster { get => poster; }
		public string Mt20id { get => mt20id; }
	}
}

