using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Extension;

public class ResourceSystem : BaseSystem<ResourceSystem>
{
    public enum AssetType
    {
        AnimationClip,
        AudioClip,
        AudioMixer,
        ComputeShader,
        Font,
        GUISkin,
        Material,
        Mesh,
        Model,
        PhysicsMaterial,
        Prefab,
        Scene,
        Shader,
        Sprite,
        Texture,
        VideoClip,
    }

    List<Transform> hierarchyTransforms =new List<Transform>();

    [HideInInspector]
    public ResourceLoaderResources ResourceLoaderResources;

    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();

        ResourceLoaderResources = gameObject.GetComponentForce<ResourceLoaderResources>();

        foreach(AssetType itr in System.Enum.GetValues(typeof(AssetType)))
        {
            GameObject go = new GameObject(itr.ToString());
            go.transform.parent = gameObject.transform;
            hierarchyTransforms.Add(go.transform);
        }
    }

    protected override void Start()
    {
        base.Start();
    }

    public override void Clear()
    {
        base.Clear();
    }

    public Transform GetTransform(AssetType assetType)
    {
        return hierarchyTransforms[(int)assetType];
    }
}
