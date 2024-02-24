using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Text;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace Sabanishi.ZundaManufacture.Editor
{
    public class TagNameCreator
    {
        private static readonly string[] InvalidChars =
        {
            " ", "!", "\"", "#", "$",
            "%", "&", "\'", "(", ")",
            "-", "=", "^", "~", "\\",
            "|", "[", "{", "@", "`",
            "]", "}", ":", "*", ";",
            "+", "/", "?", ".", ">",
            ",", "<"
        };

        private const string PathName = "Assets/Scripts/Common/TagName.cs";
        private static readonly string FileName = Path.GetFileName(PathName);
        private static readonly string FileNameWithoutExtension = Path.GetFileNameWithoutExtension(PathName);

        [MenuItem("Tools/Create/TagName")]
        private static void Open()
        {
            if (!CanCreate())
            {
                //警告ポップアップを表示
                EditorUtility.DisplayDialog("TagNameCreator[失敗]", "現在作成できません(Playモード/コンパイル中)", "OK");
                return;
            }
            
            Create();
            
            //完了ポップアップを表示
            EditorUtility.DisplayDialog("TagNameCreator[成功]", "作成が完了しました", "OK");
        }

        private static bool CanCreate()
        {
            return !EditorApplication.isPlaying && !Application.isPlaying && !EditorApplication.isCompiling;
        }

        private static void Create()
        {
            var builder = new StringBuilder();
            
            AppendLine(0,"namespace Sabanishi.ZundaManufacture.Common", builder);
            AppendLine(0,"{", builder);
            
            AppendLine(1,"/// <summary>", builder);
            AppendLine(1,"/// タグ名を定数で管理するクラス", builder);
            AppendLine(1,"/// </summary>", builder);
            
            AppendLine(1, "public static class TagName", builder);
            AppendLine(1, "{", builder);

            foreach (var n in InternalEditorUtility.tags.Select(c => new { left = RemoveInvalidChars(c), right = c }))
            {
                AppendLine(2, $"public const string {n.left} = \"{n.right}\";", builder);
            }
            
            AppendLine(1, "}", builder);
            AppendLine(0,"}", builder);
            
            var directorName = Path.GetDirectoryName(PathName);
            if (!Directory.Exists(directorName))
            {
                Directory.CreateDirectory(directorName);
            }
            
            File.WriteAllText(PathName, builder.ToString(), Encoding.UTF8);
            AssetDatabase.Refresh(ImportAssetOptions.ImportRecursive);
        }
        
        private static void AppendLine(int indent, string value, StringBuilder builder)
        {
            for (var i = 0; i < indent; i++)
            {
                builder.Append("\t");
            }
            builder.AppendLine(value);
        }
        
        /// <summary>
        /// 無効な文字列の削除を行う
        /// </summary>
        private static string RemoveInvalidChars(string str)
        {
            return InvalidChars.Aggregate(str, (current, c) => current.Replace(c, string.Empty));
        }
    }
}