using UnityEngine;

public class SpriteFromPrefab : MonoBehaviour
{
    public Camera renderCamera;
    public GameObject prefabToRender;
    public string outputSpriteName = "RenderedSprite";

    private void Start()
    {
        // Instantiate the prefab
        GameObject instantiatedPrefab = Instantiate(prefabToRender);

        instantiatedPrefab.layer = LayerMask.NameToLayer("Sprites");
        // Adjust the camera position and viewport
        AdjustCameraViewport(instantiatedPrefab);

        // Create a Render Texture and assign it to the camera
        RenderTexture renderTexture = new RenderTexture(512, 512, 24);
        renderCamera.targetTexture = renderTexture;

        // Render the model to the Render Texture
        renderCamera.Render();

        // Create a new Texture2D and read the pixels from the Render Texture
        Texture2D texture = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGBA32, false);
        RenderTexture.active = renderTexture;
        texture.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        texture.Apply();

        // Create a sprite from the texture
        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one * 0.5f);

        // Save the sprite as an asset
        string outputFilePath = $"Assets/{outputSpriteName}.png";
        System.IO.File.WriteAllBytes(outputFilePath, texture.EncodeToPNG());
        UnityEditor.AssetDatabase.Refresh();
        UnityEditor.AssetDatabase.ImportAsset(outputFilePath, UnityEditor.ImportAssetOptions.ForceSynchronousImport);
        UnityEditor.TextureImporter textureImporter = UnityEditor.AssetImporter.GetAtPath(outputFilePath) as UnityEditor.TextureImporter;
        textureImporter.textureType = UnityEditor.TextureImporterType.Sprite;
        textureImporter.spriteImportMode = UnityEditor.SpriteImportMode.Single;
        textureImporter.spritePixelsPerUnit = 100; // Adjust the pixels per unit as needed
        textureImporter.textureCompression = UnityEditor.TextureImporterCompression.Uncompressed;
        UnityEditor.AssetDatabase.ImportAsset(outputFilePath, UnityEditor.ImportAssetOptions.ForceSynchronousImport);

        // Clean up

        Destroy(instantiatedPrefab);
        Destroy(renderTexture);
        Destroy(texture);
    }

    private void AdjustCameraViewport(GameObject targetObject)
    {
        Renderer targetRenderer = targetObject.GetComponent<Renderer>();
        Bounds targetBounds = targetRenderer.bounds;

        // Calculate the desired camera position
        Vector3 cameraPosition = targetBounds.center - renderCamera.transform.forward * (targetBounds.size.magnitude + 1f);

        // Move the camera to the desired position
        renderCamera.transform.position = cameraPosition;
        renderCamera.transform.LookAt(targetBounds.center);

        // Adjust the camera's viewport based on the target object's size
        float targetSize = Mathf.Max(targetBounds.size.x, targetBounds.size.y, targetBounds.size.z);
        renderCamera.orthographicSize = targetSize / 2f;
    }
}