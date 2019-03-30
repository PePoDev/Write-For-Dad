using RotaryHeart.Lib.SerializableDictionary;
using PePo.Dev;
using UnityEngine;

public class ObjectsReference : MonoBehaviour
{
    public ObjectsInScene objectsInScene;
}

namespace PePo.Dev
{
    [System.Serializable]
    public class ObjectsInScene : SerializableDictionaryBase<string, GameObject>
    {

    }
}