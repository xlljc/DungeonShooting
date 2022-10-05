using System.Text;

Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

//备份源代码
var projectPath = new Uri(Environment.CurrentDirectory + "../../../../../DScript_Runtime");
string srcPath = projectPath.AbsolutePath + "/Environment/";
string zipPath = projectPath.AbsolutePath + "/Backups/" + DateTime.Now.Ticks + ".zip";
if (!Directory.Exists(projectPath.AbsolutePath + "/Backups/"))
{
    Directory.CreateDirectory(projectPath.AbsolutePath + "/Backups/");
}
ZipUtils.ZipFile(srcPath, zipPath);

//开始生成代码
GenerateCode.StartGenerate(projectPath);