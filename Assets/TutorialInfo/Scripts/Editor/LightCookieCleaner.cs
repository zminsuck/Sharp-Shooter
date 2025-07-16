using UnityEditor;
using UnityEngine;

public class LightCookieCleaner : EditorWindow
{
    [MenuItem("Tools/Clean Invalid Light Cookies")]
    public static void CleanLightCookies()
    {
        // ������ ȯ�濡�� ��� ����Ʈ ã�� (���� ��Ȱ��ȭ�� ������Ʈ ����)
        var lights = Resources.FindObjectsOfTypeAll<Light>();

        int count = 0;

        foreach (var light in lights)
        {
            // ������ �����ͳ� Project�� �ִ� ������Ʈ�� ����
            if (EditorUtility.IsPersistent(light.gameObject))
                continue;

            // Spot, Directional Light�� �ƴϰ� Cookie�� �ִٸ� ����
            if (light.cookie != null &&
                light.type != LightType.Spot &&
                light.type != LightType.Directional)
            {
                Debug.LogWarning($"[Fixed] Light '{light.name}' ({light.type}) had a cookie. Removing.");
                light.cookie = null;
                EditorUtility.SetDirty(light); // ������� ����
                count++;
            }
        }

        Debug.Log($"Cookie ���� �Ϸ�: {count}���� �߸��� ����Ʈ�� �����Ǿ����ϴ�.");
    }
}
