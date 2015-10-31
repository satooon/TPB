
public class Common {

	public enum Scene {
		Title,
		CharacterSelect,
		RoomSelect,
		RoomWait,
		Battle,
	}

}

public static class CommonSceneExtension {

	private static string[] sceneName = new string[]{
		"Title",
		"CharacterSelect",
		"RoomSelect",
		"RoomWait",
		"Battle",
	};

	public static int GetIntValue(this Common.Scene value) {
		return (int)value;
	}

	public static string GetSceneName(this Common.Scene value) {
		return CommonSceneExtension.sceneName[value.GetIntValue()];
	}

}
