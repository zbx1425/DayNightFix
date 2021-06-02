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
			int count = 0;
			foreach (var line in lines){
				var tokens = line.Split(new char[]{',', ' '}, StringSplitOptions.RemoveEmptyEntries);
				if (tokens.Length > 2) {
					var t0 = tokens[0].Trim().ToLower();
					var t1 = tokens[1].Trim().ToLower();
					var t2 = tokens[2].Trim().ToLower();
				    if ((t0 == "load" || t0 == "loadtexture") && (t1 != "" && t2 != "")) {
						if (t1 == t2) {
							if (t0 == "load") {
								sb.AppendLine("Load " + tokens[1]);
								sb.AppendLine("EmissiveColor 255, 255, 255");
							} else {
								sb.AppendLine("LoadTexture, " + tokens[1]);
								sb.AppendLine("SetEmissiveColor, 255, 255, 255");
							}
						} else {
							if (t0 == "load") {
								sb.AppendLine("Load " + tokens[1] + ", " + tokens[2]);
								sb.AppendLine("EmissiveColor 255, 255, 255");
							} else {
								sb.AppendLine("LoadTexture, " + tokens[1] + ", " + tokens[2]);
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
