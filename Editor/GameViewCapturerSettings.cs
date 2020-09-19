using UnityEngine;

namespace Kogane.Internal
{
	internal sealed class GameViewCapturerSettings : ScriptableObject
	{
		[SerializeField] private string m_dateTimeFormat = GameViewCapturer.DEFAULT_DATE_TIME_FORMAT;
		[SerializeField] private string m_filenameFormat = GameViewCapturer.DEFAULT_FILENAME_FORMAT;

		public string DateTimeFormat => m_dateTimeFormat;
		public string FilenameFormat => m_filenameFormat;
	}
}