using System;

using Ladybug.SceneManagement;

namespace xplatform
{
	public static class Program
	{
		[STAThread]
		static void Main()
		{
			using (var sceneManager = new SceneManager())
			{
				sceneManager.LoadScene(new MainScene(sceneManager));
				sceneManager.Run();
			}
		}
	}
}
