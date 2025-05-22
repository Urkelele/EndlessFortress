using UnityEngine;
using UnityEditor;
using System.IO;
using NUnit.Framework.Interfaces;

public class EditorToolSpritesItems : EditorWindow
{
    private string spritesFolder = "Assets/Props/Items_Icons/Passive_Items_Icons";
    private string itemDataFolder = "Assets/Items/Passive_Items";
    [MenuItem("Tools/Assign Random Item Icons")]
    public static void ShowWindow()
    {
        GetWindow<EditorToolSpritesItems>("Random Icon Assigner");
    }

    private void OnGUI()
    {
        GUILayout.Label("Assign Item Icons to Random ScriptableObjects", EditorStyles.boldLabel);

        spritesFolder = EditorGUILayout.TextField("Sprites Folder", spritesFolder);
        itemDataFolder = EditorGUILayout.TextField("ScriptableObjects Folder", itemDataFolder);

        if (GUILayout.Button("Assign Icons"))
        {
            AssignIconsRandomly();
        }
    }
    [MenuItem("Tools/Clear All Item Icons")]
    public static void ClearIcons()
    {
        string[] itemGuids = AssetDatabase.FindAssets("t:ItemData", new[] { "Assets/Items/Passive_Items" });

        foreach (string guid in itemGuids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            ItemBaseScript item = AssetDatabase.LoadAssetAtPath<ItemBaseScript>(path);

            if (item != null)
            {
                item.m_SpriteItem = null;
                EditorUtility.SetDirty(item);
            }
        }

        AssetDatabase.SaveAssets();
        Debug.Log("✅ All item icons cleared.");
    }

    private void AssignIconsRandomly()
    {
        string[] spriteGuids = AssetDatabase.FindAssets("t:Sprite", new[] { spritesFolder });
        string[] itemGuids = AssetDatabase.FindAssets("t:ItemBaseScript", new[] { itemDataFolder });

        int assigned = 0;

        foreach (string spriteGuid in spriteGuids)
        {
            string spritePath = AssetDatabase.GUIDToAssetPath(spriteGuid);
            Sprite sprite = AssetDatabase.LoadAssetAtPath<Sprite>(spritePath);

            string cleanName = sprite.name.Replace("_Icon", "");

            // Find the first unused item
            ItemBaseScript targetItem = null;
            string targetPath = null;

            foreach (string itemGuid in itemGuids)
            {
                string itemPath = AssetDatabase.GUIDToAssetPath(itemGuid);
                ItemBaseScript item = AssetDatabase.LoadAssetAtPath<ItemBaseScript>(itemPath);

                if (item.m_SpriteItem == null) // Use only unused ones
                {
                    targetItem = item;
                    targetPath = itemPath;
                    break;
                }
            }

            if (targetItem != null)
            {
                targetItem.m_SpriteItem = sprite;
                targetItem.m_ItemName = cleanName;

                // Rename the ScriptableObject asset
                AssetDatabase.RenameAsset(targetPath, cleanName);

                EditorUtility.SetDirty(targetItem);
                assigned++;

                Debug.Log($"✔ Assigned {sprite.name} to ScriptableObject: {cleanName}");
            }
            else
            {
                Debug.LogWarning($"⚠ No available ScriptableObjects left for sprite: {sprite.name}");
            }
        }

        AssetDatabase.SaveAssets();
        Debug.Log($"✅ Assigned {assigned} icons to item ScriptableObjects.");
    }
}
