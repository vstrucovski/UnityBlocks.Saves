using UnityEngine;

namespace UnityBlocks.SaveSystem.Data
{
    [CreateAssetMenu(menuName = Constants.CONFIG_MENU_PATH + "/Save System Config")]
    public class SaveServiceConfig : ScriptableObject
    {
        [field: SerializeField] public bool logRead { get; private set; } = true;
        [field: SerializeField] public bool logWrite { get; private set; } = true;
        [field: SerializeField] public bool useEncryption { get; private set; } = false;
    }
}