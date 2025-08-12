/*
 * Created by SharpDevelop.
 * User: ZBX
 * Date: 2020/9/5
 * Time: 14:49
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Text;
using System.IO;

namespace DayNightFix
{
	/// <summary>
	/// Description of SyntaxPatch.
	/// </summary>
	public static class SyntaxPatch
	{
		
		public static int PatchFile(string fileName, bool notDryRun = false) {
			var sb = new StringBuilder();
			var lines = File.ReadAllLines(fileName, TextEncoding.GetSystemEncodingFromFile(fileName));
			var isB3d = fileName.EndsWith(".b3d", StringComparison.OrdinalIgnoreCase);
			int count = 0;
			foreach (var line in lines)
			{
				string command;
				var tokens = line.Split(new char[]{','}, StringSplitOptions.RemoveEmptyEntries);
				if (isB3d && tokens.Length > 0) {
					// b3d command are separated with spaces
					var cmdIdx = line.IndexOf(' ');
					if (cmdIdx != -1) {
						command = tokens[0].Substring(0, cmdIdx).Trim().ToLower();
						tokens[0] = tokens[0].Substring(cmdIdx + 1).Trim();
					} else {
						command = null;
					}
				} else if(tokens.Length > 0) {
					command = tokens[0].Trim().ToLower();
					for (int i = 0; i < tokens.Length-1; i++) {
						tokens[i] = tokens[i+1].Trim();
					}
					Array.Resize(ref tokens, tokens.Length - 1);
				} else {
					command = null;
				}

				if (command != null && tokens.Length > 1) {
					var t1 = tokens[0].ToLower();
					var t2 = tokens[1].ToLower();
				    if ((command == "load" || command == "loadtexture") && (t1 != "" && t2 != "")) {
						if (t1 == t2) {
							if (isB3d) {
								sb.AppendLine("Load " + tokens[0]);
								sb.AppendLine("EmissiveColor 255, 255, 255");
							} else {
								sb.AppendLine("LoadTexture, " + tokens[0]);
								sb.AppendLine("SetEmissiveColor, 255, 255, 255");
							}
						} else {
							if (isB3d) {
								sb.AppendLine("Load " + tokens[0] + ", " + tokens[1]);
								sb.AppendLine("EmissiveColor 255, 255, 255");
							} else {
								sb.AppendLine("LoadTexture, " + tokens[0] + ", " + tokens[1]);
								sb.AppendLine("SetEmissiveColor, 255, 255, 255");
							}
						}
						count++;
					} else {
						sb.AppendLine(line);
					}
				} else {
					sb.AppendLine(line);
				}
			}
			if (notDryRun && count > 0) File.WriteAllText(fileName, sb.ToString(), Encoding.UTF8);
			return count;
		}
		
		public static int PatchAllFiles(string[] fileNames, Action<string, int, bool> callback, bool notDryRun = false) {
			int count = 0;
			foreach (var fileName in fileNames) {
				callback(fileName, 1, notDryRun);
				int result = PatchFile(fileName, notDryRun);
				if (result > 0){
					count++;
					callback(fileName, 10 + result, notDryRun);
				} else {
					callback(fileName, 2, notDryRun);
				}
			}
			return count;
		}
	}
}
