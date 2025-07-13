using UnityEditor;
using UnityEngine;

public class LightCookieCleaner : EditorWindow
{
    [MenuItem("Tools/Clean Invalid Light Cookies")]
    public static void CleanLightCookies()
    {
        // 에디터 환경에서 모든 라이트 찾기 (씬에 비활성화된 오브젝트 포함)
        var lights = Resources.FindObjectsOfTypeAll<Light>();

        int count = 0;

        foreach (var light in lights)
        {
            // 프리팹 에디터나 Project에 있는 오브젝트는 제외
            if (EditorUtility.IsPersistent(light.gameObject))
                continue;

            // Spot, Directional Light가 아니고 Cookie가 있다면 제거
            if (light.cookie != null &&
                light.type != LightType.Spot &&
                light.type != LightType.Directional)
            {
                Debug.LogWarning($"[Fixed] Light '{light.name}' ({light.type}) had a cookie. Removing.");
                light.cookie = null;
                EditorUtility.SetDirty(light); // 변경사항 저장
                count++;
            }
        }

        Debug.Log($"Cookie 제거 완료: {count}개의 잘못된 라이트가 수정되었습니다.");
    }
}
