using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ImportantScripts.Core.Singleton;

namespace Cloth
{
    public enum ClothType
    {
        SPEED,
        STRONG,
        JUMP

    }

    public class ClothManager : Singleton<ClothManager>
    {
        public List<ClothSetup> clothSetups;

        public ClothSetup GetSetupByType(ClothType clothType)
        {
            return clothSetups.Find(i => i.clothType == clothType);
        }
    }
    
    [System.Serializable]
    public class ClothSetup
    {
        public ClothType clothType;
        public Texture2D texture;
    }
}
