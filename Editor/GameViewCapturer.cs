using System;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Kogane.Internal
{
	internal static class GameViewCapturer
	{
		public const string DEFAULT_DATE_TIME_FORMAT = "yyyy-MM-dd_HHmmss";
		public const string DEFAULT_FILENAME_FORMAT  = DATE_TIME_TAG + ".png";

		private const string PACKAGE_NAME  = "UniGameViewCapturer";
		private const string DATE_TIME_TAG = "%DATE_TIME%";
		private const string SETTINGS_PATH = "Assets/UniGameViewCapturerSettings.asset";

		private const string ITEM_NAME_ROOT            = "Edit/UniGameViewCapturer/";
		private const string ITEM_NAME_CAPTURE         = ITEM_NAME_ROOT + "キャプチャ _F12";
		private const string ITEM_NAME_CREATE_SETTINGS = ITEM_NAME_ROOT + "設定ファイル作成";

		[MenuItem( ITEM_NAME_CAPTURE )]
		private static void Capture()
		{
			Debug.Log( $"[{PACKAGE_NAME}] キャプチャ開始" );

			var guid = AssetDatabase
					.FindAssets( $"t:{nameof( GameViewCapturerSettings )}" )
					.FirstOrDefault()
				;

			var assetPath      = AssetDatabase.GUIDToAssetPath( guid );
			var settings       = AssetDatabase.LoadAssetAtPath<GameViewCapturerSettings>( assetPath );
			var dateTimeFormat = settings != null ? settings.DateTimeFormat : DEFAULT_DATE_TIME_FORMAT;
			var filenameFormat = settings != null ? settings.FilenameFormat : DEFAULT_FILENAME_FORMAT;
			var dateTime       = DateTime.Now.ToString( dateTimeFormat );
			var filename       = filenameFormat.Replace( DATE_TIME_TAG, dateTime );

			var dir = Path.GetDirectoryName( filename );

			if ( !string.IsNullOrWhiteSpace( dir ) && !Directory.Exists( dir ) )
			{
				Directory.CreateDirectory( dir );
			}

			ScreenCapture.CaptureScreenshot( filename );

			Debug.Log( $"[{PACKAGE_NAME}] キャプチャ完了：{filename}" );
		}

		[MenuItem( ITEM_NAME_CREATE_SETTINGS )]
		private static void CreateSettings()
		{
			var settings = ScriptableObject.CreateInstance<GameViewCapturerSettings>();
			AssetDatabase.CreateAsset( settings, SETTINGS_PATH );
			EditorGUIUtility.PingObject( settings );
		}

		[MenuItem( ITEM_NAME_CREATE_SETTINGS, true )]
		private static bool CanCreateSettings()
		{
			var guid = AssetDatabase
					.FindAssets( $"t:{nameof( GameViewCapturerSettings )}" )
					.FirstOrDefault()
				;

			return string.IsNullOrWhiteSpace( guid );
		}
	}
}