namespace Mep01Web.Type
{
    public class Duration
    {
        private int _hh { get; set; }
        private int _mm { get; set; }
        private int _ss { get; set; }
        private int _seconds { get; set; }
        private string _hms { get; set; }
        private DateTime _datetime { get; set; }

        public Duration(DateTime dt)
        {
            _hh = dt.Hour;
            _mm = dt.Minute;
            _ss = dt.Second;
            _seconds = _hh * 3600 + _mm * 60 + _ss;
            _hms = _hh.ToString("00") + ":" + _mm.ToString("00") + ":" + _ss.ToString("00");
			_datetime = dt;
		}
        public Duration(decimal sss)
        {
            _seconds = (int)sss;
            _hh = _seconds / 3600;
            _mm = (_seconds - _hh * 3600) / 60;
            _ss = _seconds - _hh * 3600 - _mm * 60;
            _hms = _hh.ToString("00") + ":" + _mm.ToString("00") + ":" + _ss.ToString("00");
            try { 
			   _datetime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, _hh, _mm, _ss);
            } catch
            {
                _datetime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
            }

        }
        public int GetSeconds()
        {
            return _seconds;
        }
        public string GetDuration()
        {
            return _hms;
        }
		public DateTime GetDatetime()
		{
			return _datetime;
		}
	}
}
